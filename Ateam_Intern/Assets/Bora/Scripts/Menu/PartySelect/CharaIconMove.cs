using UnityEngine;
using System.Collections;

public class CharaIconMove : MonoBehaviour {

	[SerializeField]
	float fTime = 0.5f;

	[SerializeField]
	Vector3 Move = new Vector3(0,50,0);

	[SerializeField]
	bool bAdd = true;

	bool bWait = false;

	float fNowWaitTime = 0.0f;
	[SerializeField]
	float fWaitTime = 0.5f;

	Vector3 InitPos = Vector3.zero;

	void Start() {
		if (bAdd) {
			InitPos = transform.position;
		} else {
			InitPos = transform.position - Move;
		}
	}

	void Update () {

		if (bWait) {
			fNowWaitTime += Time.deltaTime;
			if(fNowWaitTime >= fWaitTime) {
				fNowWaitTime = 0.0f;
				bWait = false;
			}
			return;
		}
	
		if (bAdd) {
			transform.position += Move * (Time.deltaTime / fTime);

			if (transform.position.y >= InitPos.y + Move.y) {
				bAdd = false;
				bWait = true;
			}

		} else {
			transform.position -= Move * (Time.deltaTime / fTime);

			if (transform.position.y <= InitPos.y) {
				bAdd = true;
				bWait = true;
			}
		}
	}
}
