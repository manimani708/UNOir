using UnityEngine;
using System.Collections;

public class Thunder1 : EffectBase {

	CharaSkillBase skillBase = null;
	public ParticleSystem myParticle = null;
	public ParticleSystem particle = null;
	public BlackOut blackOut = null; 

	public float fWait = 0.0f;

	public float fFadeInTime = 1.0f;
	public float fMaxBlack = 0.35f; 

	// Use this for initialization
	void Awake () {
		particle.transform.parent = null;
		blackOut.transform.parent = null;
		particle.Stop ();
	}

	// Update is called once per frame
	void Update () {

		if (myParticle && myParticle.isPlaying && blackOut.Alpha < fMaxBlack) {
			blackOut.Alpha += fMaxBlack * (Time.deltaTime / fFadeInTime);

			if (blackOut.Alpha >= fMaxBlack) {
				SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_THUNDER);
			}
			return;
		}

		if (myParticle && !myParticle.isPlaying) {
			Destroy (myParticle.gameObject);
			myParticle = null;
			particle.Play ();
			skillBase.Run ();
			SoundManager.Instance.PlayBGM (SoundManager.eBgmValue.BGM_THUNDERNOW);
		}

		if (!myParticle) {
			blackOut.Alpha -= fMaxBlack * (Time.deltaTime / fFadeInTime);

			if (blackOut.Alpha <= 0.0f) {
				Destroy (blackOut.gameObject); 
				Destroy (this.gameObject); 
			}
		}
	}

	public override void Set(CharaSkillBase skillData) {
		skillBase = skillData;
	}
}
