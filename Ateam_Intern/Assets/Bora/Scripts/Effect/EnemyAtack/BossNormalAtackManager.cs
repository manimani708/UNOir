using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossNormalAtackManager : EffectBase {

	public List<BossNormalAtack> atackList = new List<BossNormalAtack>(); 
	bool bDamageRed = true;

	void Start() {
		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_BOSSNORMALVOICE);
	}

	void Update () {

		int nEnd = 0;
		for (int i = 0; i < atackList.Count; i++) {
			if(atackList[i].bEnd) {
				nEnd++;
			}
		}

		if (!bAtack) {
			bAtack = atackList [0].bAtack;
			if (bAtack && bDamageRed) {
				DamageRed.Instance.Run ();
				SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_BOSSNORMALATACK);
			}
		}

		if (bReverse && !atackList [atackList.Count / 2].bReverse) {
			bReverse = false;
			ReverseEffectManager.Instance.Run ();
		}

		if (nEnd >= atackList.Count) {
			bEnd = true;
			//Destroy (this.gameObject);  // 消す
		}
	}

	public override void Set(bool bFlg) {
		bReverse = bFlg;
		bDamageRed = !bFlg;

		for (int i = 0; i < atackList.Count; i++) {
			atackList [i].Set (bFlg);
		}
	}
}
