using UnityEngine;
using System.Collections;

public class SolerBeam : EffectBase {

	CharaSkillBase skillBase = null;

	public float fTime = 1.0f;
	Vector3 Distance = Vector3.zero;

	float fInitHeight = 0.0f;
	public bool bUp = true;
	public float fShakeTime = 0.1f;
	float fNowShake = 0.0f;
	float fNowShakeAmount = 0.0f;
	public float fShake = 0.6f;
	[SerializeField] Vector3 TargetPos = Vector3.zero;
	float fInitScale = 0.0f;

	// Use this for initialization
	void Start () {

		transform.position += new Vector3 (0, Random.Range(-0.5f,0.5f), 0);

		//TargetPos = transform.position;
		//TargetPos.x *= -1;
		Distance = TargetPos - transform.position;
		fInitHeight = transform.position.y;
		bUp = true;
		fNowShakeAmount = fNowShake = Random.Range (0.0f, fShake);
		fInitScale = transform.localScale.y;

		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_SOLERBEAM);
	}

	// Update is called once per frame
	void Update () {

		transform.position += Distance * (Time.deltaTime / fTime);
		transform.localScale -= new Vector3 (0, fInitScale, 0) * (Time.deltaTime / fTime);

		if (TargetPos.x >= transform.position.x) {
			bEnd = true;
			skillBase.Run ();
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

	public override void Set(CharaSkillBase skillData) {
		skillBase = skillData;
	}
}
