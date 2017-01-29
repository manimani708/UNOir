using UnityEngine;
using System.Collections;

public class Boss_TextMove : MonoBehaviour {

	public bool bEnd { get; private set;}
	float fNowTime = 0.0f;
	public float fTime = 2.0f;
	Vector3 InitPos = Vector3.zero;
	Vector3 PurposePos = Vector3.zero;

	// Use this for initialization
	void Start () {
		bEnd = false;
		InitPos = PurposePos = transform.position;
		PurposePos.x *= -1; // 逆側に移動するため
	}
	
	// Update is called once per frame
	void Update () {

		if (bEnd)
			return;

		fNowTime += Time.deltaTime;
		transform.position += (PurposePos - InitPos) * (Time.deltaTime / fTime);

		if (fNowTime >= fTime) {
			bEnd = true;
		}
	}
}
