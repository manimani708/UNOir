using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharaSkillManager : EffectBase {

	CharaSkillBase skillBase = null;
	static Sprite[] cutinSprite = new Sprite[5];
	public Color[] effectColor = new Color[5];
	SpriteRenderer render = null;
	int nNumber = 0; // 番号をもとにエフェクトを判断

	Vector3 Distance = Vector3.zero;
	float fNowTime = 0.0f;
	public float fTime = 0.5f;
	bool bAdd = true;

	bool bWait = false;
	public float fWaitTime = 1.0f;

	//const float fDelta = 1.0f / 60.0f;

	public ParticleSystem particle = null;
	public List<GameObject> effectList = new List<GameObject> ();

	// 順番判断材料
	static int nMaxCharaSkill = 0;
	static int nNowCharaSkill = 0;
	int nMyNumber = 0;

	public float fTopPos = 4.35f;
	public float fDownPos = 2.0f;
	float fInterval = 0.0f;

	bool bSe = false;

	// Use this for initialization
	void Awake () {

		if (!cutinSprite [0]) {
			cutinSprite = ResourceHolder.Instance.GetResource (ResourceHolder.eResourceId.ID_CUTIN);
			//cutinSprite = Resources.LoadAll<Sprite> ("Textures/Effect/CharaSkill/CutIn");
		}

		if (bEnd) {
			return;
		}

		render = GetComponent<SpriteRenderer> ();
		render.color = new Color (1,1,1,0);
		Vector3 TargetPos = transform.position;
		TargetPos.x *= -1; // 反転
		Distance = TargetPos - transform.position;
		fInterval = fTopPos - fDownPos;
	}

	void Start() {
		if (nMaxCharaSkill <= 1)
			return;

		float dis = fInterval / (nMaxCharaSkill + 1);
		float rivision = fTopPos - (dis * (nMyNumber + 1));
		Vector3 p = transform.position;
		transform.position = new Vector3(p.x, rivision, p.z);
		GameController.Instance.SetDelta (0.0f, true);
	}
	
	// Update is called once per frame
	void Update () {

		if (GamePause.bPause)
			return;

		if (nMyNumber != nNowCharaSkill) {
			return;
		}

		float fNowSpeed = Time.unscaledDeltaTime;
		/*if (nMaxCharaSkill >= 3) {
			fNowSpeed *= 1.2f; // 同時枚数が多いので早送り
		}*/

		//GameController.Instance.SetDelta (0.0f);
		if (bSe) {
			GameController.Instance.SetDelta (0.0f);
		} else {
            GameController.Instance.SetDelta(0.0f, true);
            SoundManager.Instance.PauseBGM(SoundManager.eBgmValue.BGM_ATACKUP, true);
            SoundManager.Instance.PauseBGM(SoundManager.eBgmValue.BGM_THUNDERNOW, true);
		}

		fNowTime += fNowSpeed;
		if (!bWait) {
			transform.position += (Distance / 10.0f * 4.75f)  * (fNowSpeed / fTime);

			if (bAdd) {
				render.color += new Color (0,0,0, 1.0f * (fNowSpeed / fTime));
			} else {
				render.color -= new Color (0,0,0, 1.0f * (fNowSpeed / fTime));
			}

			if (fNowTime >= fTime && fNowTime < fTime + fWaitTime) {
				bWait = true;

				// 中央でエフェクト再生
				particle.Play();
				Vector3 p = transform.position;
				p.x = 0.0f;
				particle.transform.position = p;
				transform.DetachChildren ();
				bAdd = false;

                // 処理落ちによる位置ズレを解消
                p.x = -(Distance.x / 10.0f * 0.25f);
                transform.position = p;
                fNowTime = fTime;

				if (!bSe) {
                    SoundManager.Instance.StopSE(SoundManager.eSeValue.SE_CUTIN);
                    SoundManager.Instance.PlaySE(SoundManager.eSeValue.SE_CUTIN);

					switch (nNumber) {
					case 0:
						SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_CUTINHARE);
						break;
					case 1:
						SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_CUTINAME);
						break;
					case 2:
						SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_CUTINKAMINARI);
						break;
					case 3:
						SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_CUTINKAZE);
						break;
					case 4:
						SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_CUTINFRIEND);
						break;
                    }
                    //Debug.Log("voice");

					bSe = true;
				}
			} 
			else if (fNowTime >= fTime * 2.0f + fWaitTime) {
				bEnd = true;
                GameController.Instance.SetDelta(1.0f);
                SoundManager.Instance.PauseBGM(SoundManager.eBgmValue.BGM_ATACKUP, false);
                SoundManager.Instance.PauseBGM(SoundManager.eBgmValue.BGM_THUNDERNOW, false);

				nNowCharaSkill++;
				if (nNowCharaSkill >= nMaxCharaSkill) {
					nNowCharaSkill = 0;
					nMaxCharaSkill = 0; // すべて終了した
				}
				// ここで各種エフェクト生成
				GameObject Obj = (GameObject)Instantiate(effectList[nNumber]);
				Obj.GetComponent<EffectBase> ().Set (skillBase);

				Destroy (this.gameObject);
				Destroy (particle.gameObject);
			}
		} else {
			transform.position += (Distance / 10.0f * 0.5f)  * (fNowSpeed / fWaitTime);

			if (fNowTime >= fTime + fWaitTime) {
				bWait = false;
			}
		}

		if (!particle.transform.parent) {
			particle.Simulate (fNowSpeed, true, false);
		}
	}

	public override void Set(CharaSkillBase skillData) {
		skillBase = skillData;

		switch (skillBase.SkillColor) {

		case UnoStruct.eColor.COLOR_RED:
			if (skillBase.SkillType == CharaSkillBase.eSkillType.SKILL_RISE) {
				nNumber = 0;
			} else {
				nNumber = 4;
			}
			break;
		case UnoStruct.eColor.COLOR_BLUE:
			nNumber = 1;
			break;
		case UnoStruct.eColor.COLOR_GREEN:
			nNumber = 2;
			break;
		case UnoStruct.eColor.COLOR_YELLOW:
			nNumber = 3;
			break;
		}

		// Sprite取得
		render.sprite = cutinSprite [nNumber];

		particle.Stop ();
		particle.startColor = effectColor [nNumber];

		// ナンバー保存
		nMyNumber = nMaxCharaSkill;
		nMaxCharaSkill++;
	}
}
