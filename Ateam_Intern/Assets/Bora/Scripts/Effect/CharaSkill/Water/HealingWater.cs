using UnityEngine;
using System.Collections;

public class HealingWater : EffectBase {

	CharaSkillBase skillBase = null;
	public ParticleSystem particle = null;

	float fNowTime = 0.0f;
	public float fTime = 0.5f;
	public Vector3 TargetPos = new Vector3 (0, 0.5f, 0);

	SpriteRenderer render = null;
	Vector3 Distance = Vector3.zero;
	Vector3 InitScale = Vector3.zero;

	// Use this for initialization
	void Start () {
		particle.Stop ();
		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_HEELWATER);

		render = GetComponent<SpriteRenderer> ();
		Distance = TargetPos - transform.position;
		InitScale = transform.localScale;
		transform.DetachChildren ();
	
		fTime += Random.Range (-0.2f, 0.2f);
	}

	// Update is called once per frame
	void Update () {

		if (bEnd) {
			if (!particle.isPlaying) {
				skillBase.Run ();
				Destroy (this.gameObject);
				Destroy (particle.gameObject);
			}
			return;
		}

		fNowTime += Time.deltaTime;
		transform.position += Distance * (Time.deltaTime / fTime);

		if (fNowTime >= (fTime / 10.0f) * 7.5f) {
			transform.localScale -= InitScale * (Time.deltaTime / (fTime - (fTime / 10.0f) * 7.5f));
		}

		if (!particle.isPlaying) {
			particle.transform.position = transform.position;

			if (fNowTime >= (fTime / 10.0f) * 9.0f) {
				particle.Play ();
			}
		}

		if (transform.position.y <= TargetPos.y) {
			render.color -= new Color (0, 0, 0, 1);
			bEnd = true;
		}
	}

	public override void Set(CharaSkillBase skillData) {
		skillBase = skillData;
	}
}
