using UnityEngine;
using System.Collections;

public class SolerBeamChild : MonoBehaviour {

	public float fTime = 1.0f;
	Vector3 Distance = Vector3.zero;

	float fInitHeight = 0.0f;
	public bool bUp = true;
	public float fShakeTime = 0.1f;
	float fNowShake = 0.0f;
	float fNowShakeAmount = 0.0f;
	public float fShake = 0.6f;
	Vector3 TargetPos = Vector3.zero;
	Vector3 InitScale = Vector3.zero;

	// Use this for initialization
	void Start () {

		TargetPos = transform.position;
		TargetPos.x *= -1;
		Distance = TargetPos - transform.position;
		fInitHeight = transform.position.y;
		bUp = true;
		fNowShakeAmount = fNowShake = Random.Range (0.0f, fShake);
		transform.parent = null;
		InitScale = transform.localScale;
	}

	// Update is called once per frame
	void Update () {

		transform.position += Distance * (Time.deltaTime / fTime);
		transform.localScale -= InitScale * (Time.deltaTime / fTime);

		if (TargetPos.x >= transform.position.x) {
			Destroy (this.gameObject);
		}

		if (bUp) {
			transform.position += new Vector3 (0, fNowShakeAmount * (Time.deltaTime / fShakeTime), 0);

			if (fInitHeight + fNowShake <= transform.position.y) {
				bUp = false;
				fNowShake = -Random.Range (0.0f, fShake);
				fNowShakeAmount = transform.position.y - (fInitHeight + fNowShake);
			}
		} else {
			transform.position -= new Vector3 (0, fNowShakeAmount * (Time.deltaTime / fShakeTime), 0);

			if (fInitHeight + fNowShake >= transform.position.y) {
				bUp = true;
				fNowShake = Random.Range (0.0f, fShake);
				fNowShakeAmount = (fInitHeight + fNowShake) - transform.position.y;
			}
		}
	}
}
