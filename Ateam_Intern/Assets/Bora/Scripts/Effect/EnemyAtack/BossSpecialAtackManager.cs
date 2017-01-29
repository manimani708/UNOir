using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossSpecialAtackManager : EffectBase {

	int nNow = 0;
	List<BossSpecialAtack> atackList = new List<BossSpecialAtack>();
	bool bDamageRed = true;

	bool bGo = false;
	float fNowWait = 0.0f;
	public float fWait = 0.5f;

	[SerializeField]
	BlackOut blackOut = null;

	[SerializeField]
	float fMaxAlpha = 0.5f;

	void Start() {
		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_BOSSSPECIALATACK);
		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_BOSSSPECIALVOICE);
	}

	void Update() {

		if (bEnd)
			return;

		if (nNow == atackList.Count) {

			if (bReverse && !atackList [atackList.Count / 2].bReverse) {
				bReverse = false;
				ReverseEffectManager.Instance.Run ();
			}

			if (bGo) {
				fNowWait += Time.deltaTime;
				if (fNowWait < fWait) {
					return;
				} else {
					bGo = false;
					for (int i = 0; i < atackList.Count; i++) {
						atackList [i].bGo = true;
					}
				}
			}

			blackOut.Alpha -= fMaxAlpha * (Time.deltaTime / (atackList[0].fFadeTime * 3));

			// ダメージ描画フラグ
			if (!bAtack) {
				bAtack = atackList [0].bAtack;
				if (bAtack && bDamageRed) {
					DamageRed.Instance.Run ();
				}
			}

			int nEnd = 0;
			for (int i = 0; i < atackList.Count; i++) {

				if (!atackList [i].bEnd)
					continue;
				
				nEnd++;
			}
			if (nEnd >= atackList.Count) {
				bEnd = true;
				//Destroy (this.gameObject); // 消す
			}
			return;
		}

		blackOut.Alpha += fMaxAlpha * (Time.deltaTime / (atackList[0].fScaleTime * 3));

		if (atackList [nNow].bNext) {
			nNow++;
			if (nNow >= atackList.Count) {
				bGo = true;
				return;
			}
			atackList [nNow].enabled = true;
		}
	}

	public override void Set(bool bFlg) {
		bReverse = bFlg;
		bDamageRed = !bFlg;

		for (int i = 0; i < transform.childCount - 1; i++) {
			BossSpecialAtack atack = transform.GetChild (i).GetComponent<BossSpecialAtack> ();
			atack.enabled = false;
			atack.transform.localScale = Vector3.zero;
			atack.Set (bFlg);
			atackList.Add (atack);
		}
		atackList [nNow].enabled = true;
	}
}
