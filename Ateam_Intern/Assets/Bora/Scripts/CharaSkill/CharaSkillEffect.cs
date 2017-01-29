using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharaSkillEffect : MonoBehaviour {

	bool bRun = false;
	UnoStruct.tCard tempCard;
	UnoData unoData = null;
	public SpriteRenderer spriteRenderer = null;
	const float fBlack = 0.5f;

	void Start() {
		unoData = GetComponentInParent<UnoData> ();
	}

	void Update () {
		if (!bRun) {
			return;
		}

		if (!FieldCard.Instance.Judge(tempCard) || unoData.Click || GameController.Instance.bStop) {
			spriteRenderer.color = new Color (1, 1, 1, 1.0f - CharaSkillEffectData.Instance.fAddAlpha);
			transform.localScale = CharaSkillEffectData.Instance.InitScale;
			return;
		}

		//transform.localScale = CharaSkillEffectData.Instance.nowScale;
		spriteRenderer.color = CharaSkillEffectData.Instance.nowColor;

		/*if (FieldCard.Instance.GetTempFlg () && !FieldCard.Instance.Judge(tempCard)) {
			spriteRenderer.color = new Color (fBlack,fBlack,fBlack,spriteRenderer.color.a);
		}*/
	}

	public void RunCheck(UnoStruct.tCard card) {

		bRun = false;
		List<PlayerCharactor> player = GameMainUpperManager.instance.charactorAndFriend;
		for (int i = 0; i < player.Count; i++) {
			if (card.m_Color != player [i].GetTCard ().m_Color ||
			    card.m_Number != player [i].GetTCard ().m_Number) {
				continue;
			}

			bRun = true;
			tempCard = card;
			spriteRenderer.sprite = CharaSkillEffectData.Instance.GetSprite(i);
			break;
		}

		if (!bRun) {
			spriteRenderer.color = new Color (1,1,1, 1.0f - CharaSkillEffectData.Instance.fAddAlpha);
			transform.localScale = CharaSkillEffectData.Instance.InitScale;
		}
	}
}
