using UnityEngine;
using System.Collections;

public class StrongEffectManager : EffectBase {

	public float fDistance = 1.0f;
	const int nNum = 5;
	GameObject[] Child = new GameObject[nNum];
	StrongEffect[] effectList = new StrongEffect[nNum];

	// Use this for initialization
	void Init () {

		for (int i = 0; i < Child.Length; i++) {
			Child[i] = transform.GetChild (i).gameObject;
			effectList [i] = Child [i].GetComponent<StrongEffect> ();
		}
		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_STRONGATACK);
	}
	
	// Update is called once per frame
	void Update () {

		int nCnt = 0;
		for (int i = 0; i < effectList.Length; i++) {
			if (!effectList [i].bEnd)
				continue;
			
			nCnt++;
		}

		bAtack = effectList [0].bAtack;

		if (nCnt >= effectList.Length) {
			bEnd = true;
			//Destroy (this.gameObject); // 消す
		}
	}

	public override void Set(UnoStruct.eColor color, Vector3 TargetPos) {
		Init ();

		int nAngle = Random.Range (0, 360/nNum);
		float fRadian = 360/(Mathf.PI*2.0f);

		for (int i = 0; i < Child.Length; i++) {
			float x = Mathf.Sin ((float)nAngle / fRadian) * fDistance; 
			float y = Mathf.Cos ((float)nAngle / fRadian) * fDistance; 
			Child [i].transform.position += new Vector3 (x,y,0); 
			nAngle += 360 / nNum;
		}

		for (int i = 0; i < effectList.Length; i++) {
			int nNext = i + 1;
			if (nNext == effectList.Length) {
				nNext = 0;
			}
			Vector3 Center = effectList [nNext].transform.position + (TargetPos - effectList [nNext].transform.position) / 2.0f;
			effectList [i].Set (color, TargetPos, Center);
		}
	}
}
