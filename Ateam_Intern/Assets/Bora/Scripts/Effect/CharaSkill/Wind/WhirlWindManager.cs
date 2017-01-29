using UnityEngine;
using System.Collections;

public class WhirlWindManager : EffectBase {

	CharaSkillBase skillBase = null;

	int nCnt = 0;
	WhirlWind[] Child = new WhirlWind[4];

	// Use this for initialization
	void Start () {
	
		for (int i = 0; i < transform.childCount; i++) {
			Child [i] = transform.GetChild (i).GetComponent<WhirlWind> ();
			Child [i].fTime += Random.Range (-0.15f, 0.15f);
		}
		Child [nCnt].Run ();
		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_WIND);
	}
	
	// Update is called once per frame
	void Update () {

		if (nCnt >= Child.Length - 1) {
			if (Child [nCnt].bEnd) {
				skillBase.Run ();
				Destroy (this.gameObject);
			}
			return;
		}

		if (Child [nCnt].bEnd) {
			nCnt++;
			if (nCnt >= Child.Length / 2) {
				Child [nCnt].Run ();
				Child [nCnt + 1].Run ();

				nCnt++;
			} else {
				Child [nCnt].Run ();
			}
		}
	}

	public override void Set(CharaSkillBase skillData) {
		skillBase = skillData;
	}
}
