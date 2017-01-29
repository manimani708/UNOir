using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HpNumberEffect : MonoBehaviour {

	public HPNumber hpNumber = null;
	HPNumber.State Oldstate = HPNumber.State.Safe;

	float fNowTime  = 0.0f;
	public float fTime = 0.25f;
	bool bAdd = false;

	public bool bFade = true;
	List<SpriteRenderer> numberList = new List<SpriteRenderer>();
	public float fChangeAmount = 0.5f;

	public bool bShake = true;
	Vector3 InitScale = Vector3.zero;
	public Vector3 MaxAddScale = new Vector3(0.1f,0.1f,0.0f);

	void Start() {
		hpNumber.Init ();

		if (bFade) {
			for (int i = 0; i < transform.childCount; i++) {
				numberList.Add (transform.GetChild (i).GetComponent<SpriteRenderer> ()); 
			}
		}

		if (bShake) {
			InitScale = transform.localScale;
		}
		Oldstate = hpNumber.GetState ();
	}

	void Update() {

		HPNumber.State state = hpNumber.GetState ();

		switch(state) {

		case HPNumber.State.Safe:
			if (Oldstate != state) {
				if (bFade) {
					for (int i = 0; i < numberList.Count; i++) {
						numberList [i].color = new Color (1, 1, 1, 1);
					}
				}
				if (bShake) {
					transform.localScale = InitScale;
				}
			}
			return;

		case HPNumber.State.Caution:
			fNowTime = fTime * 2.0f;
			break;
		
		case HPNumber.State.Pinti:
			fNowTime = fTime;
			break;
		}
		Oldstate = state;

		if (bFade) {

			if (bAdd) {
				for (int i = 0; i < numberList.Count; i++) {
					numberList [i].color += new Color (0,0,0, fChangeAmount * (Time.deltaTime / fNowTime));
				}

				if (numberList [0].color.a >= 1.0f) {
					for (int i = 0; i < numberList.Count; i++) {
						numberList [i].color = new Color (1,1,1,1);
					}
					bAdd = false;
				}
			} else {
				for (int i = 0; i < numberList.Count; i++) {
					numberList [i].color -= new Color (0,0,0, fChangeAmount * (Time.deltaTime / fNowTime));
				}

				if (numberList [0].color.a <= fChangeAmount) {
					for (int i = 0; i < numberList.Count; i++) {
						numberList [i].color = new Color (1,1,1,fChangeAmount);
					}
					bAdd = true;
				}
			}
		}
			
		if (bShake) {

			if (!bAdd) {
				transform.localScale += MaxAddScale * (Time.deltaTime / fNowTime); 

				if (transform.localScale.x >= InitScale.x + MaxAddScale.x) {
					transform.localScale = InitScale + MaxAddScale;
					bAdd = true;
				}
			} else {
				transform.localScale -= MaxAddScale * (Time.deltaTime / fNowTime); 

				if (transform.localScale.x <= InitScale.x) {
					transform.localScale = InitScale;
					bAdd = false;
				}
			}
		}
	}
}
