using UnityEngine;
using System.Collections;

public class LockOnEffect : MonoBehaviour {

	public float fScaleTime = 0.5f; 
	public float fRotTime = 1.0f;

	bool bAdd = true;
	Vector3 InitScale = Vector3.zero;
	public Vector3 AddScale = new Vector3(0.3f,0.3f,0.0f);

	void Start() {
		InitScale = transform.localScale;
	}

	void Update () {

		transform.eulerAngles += new Vector3(0,0, 360 * (Time.deltaTime / fRotTime));

		if (bAdd) {
			transform.localScale += AddScale * (Time.deltaTime / fScaleTime);

			if (transform.localScale.x >= InitScale.x + AddScale.x) {
				bAdd = false;
			}
		} else {
			transform.localScale -= AddScale * (Time.deltaTime / fScaleTime);

			if (transform.localScale.x <= InitScale.x) {
				bAdd = true;
			}
		}
	}
}
