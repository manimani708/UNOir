using UnityEngine;
using System.Collections;

public class Thunder : EffectBase {

	CharaSkillBase skillBase = null;
	public ParticleSystem particle = null;
	public BlackOut blackOut = null; 

	public float fWait = 0.0f;

	float fNowDownTime = 0.0f;
	public float fDownTime = 0.5f;
	Vector3 Distance = Vector3.zero;
	public Vector3 TargetPos = Vector3.zero;

	bool bUp = true;
	public float fShakeTime = 0.1f;
	public float fShake = 0.5f;
	public float fAtten = 0.02f;

	SpriteRenderer render = null;
	bool bFade = false;
	public float fFadeTime = 0.25f;

	public float fFadeInTime = 1.0f;
	public float fMaxBlack = 0.35f; 

	// Use this for initialization
	void Awake () {
		render = GetComponentInChildren<SpriteRenderer>();
		Distance = TargetPos - transform.position;
		particle.transform.parent = null;
		blackOut.transform.parent = null;
		particle.Stop ();

		//fDownTime += Random.Range (-0.2f,0.2f);
	}

	// Update is called once per frame
	void Update () {

		if (!bFade && blackOut.Alpha < fMaxBlack) {
			blackOut.Alpha += fMaxBlack * (Time.deltaTime / fFadeInTime);
			return;
		}
		
		if (fNowDownTime < fDownTime) {
			fNowDownTime += Time.deltaTime;
			transform.position += Distance * (Time.deltaTime / fDownTime);
		} else if (!bFade) {

			if (fShake <= 0.0f) {
				bFade = true;
				skillBase.Run ();
				particle.Play ();
				return;
			}

			if (bUp) {
				transform.position += new Vector3 (0, fShake * (Time.deltaTime / fShakeTime), 0);

				if (transform.position.y >= TargetPos.y + fShake) {
					bUp = false;
					fShake -= fAtten;
				}
			} else {
				transform.position -= new Vector3 (0, fShake * (Time.deltaTime / fShakeTime), 0);

				if (transform.position.y <= TargetPos.y - fShake) {
					bUp = true;
					fShake -= fAtten;
				}
			}
		} else {
			if (render.color.a <= 0.0f) {
				fNowDownTime += Time.deltaTime;

				if (fNowDownTime < fWait + fDownTime) {
					bEnd = true;
					Destroy (blackOut.gameObject);
					Destroy (this.gameObject);
				}
			} else {
				render.color -= new Color (0,0,0, 1.0f * (Time.deltaTime / fFadeTime));
				blackOut.Alpha -= fMaxBlack * (Time.deltaTime / fFadeTime);
			}
		}
	}

	public override void Set(CharaSkillBase skillData) {
		skillBase = skillData;
	}
}
