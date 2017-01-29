using UnityEngine;
using System.Collections;

public class EnemyAtack : EffectBase {

	public float fTime = 0.25f; 
	BezierCurve.tBez bez; 
	bool bDamageRed = true;

	public float fEndTime = 0.5f; 
	Vector3 MaxAdd = Vector3.zero;
	float fMaxParticleSize = 0.0f;
	SpriteRenderer render = null;
	[SerializeField]
	ParticleSystem particle = null;

	public float fRotTime = 0.25f;

	void Awake() {
		render = GetComponent<SpriteRenderer> ();
		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_NORMALENEMYATACK);
	}

	// Update is called once per frame
	void Update () {

		if (fRotTime > 0.0f) {
			transform.eulerAngles += new Vector3 (0, 0, 360 * (Time.deltaTime / fRotTime));
		}

		if (bez.t >= 1.0f) {
			render.color -= new Color (0, 0, 0, 1.0f * (Time.deltaTime / fEndTime));
			transform.localScale -= MaxAdd * (Time.deltaTime / fEndTime);

			if (!bAtack) {
				bAtack = true;
				if (bDamageRed) {
					DamageRed.Instance.Run ();
				}
			}

			if (render.color.a <= 0.0f) {
				bEnd = true;
			}
		} else {
			bez.t += 1.0f * (Time.deltaTime / fTime);
			if(bez.t >= 1.0f) {
				bez.t = 1.0f;
			}
			if (bReverse && bez.t > 0.5f) {
				ReverseEffectManager.Instance.Run ();
				bReverse = false;
			}

			transform.position = BezierCurve.CulcBez (bez);

			particle.startSize += fMaxParticleSize * (Time.deltaTime / fTime);

			if (transform.localScale.x >= MaxAdd.x)
				return;

			transform.localScale += MaxAdd * (Time.deltaTime / (fTime / 2.0f));
		}
	}

	public override void Set(Vector3 TargetPos, bool bFlg) {
		bReverse = bFlg;
		bDamageRed = !bFlg;
		bez.b0 = transform.position;
		bez.b1 = (TargetPos - bez.b0) / 2.0f + bez.b0;
		bez.b1.x = bez.b0.x;

		if (!bReverse) {
			bez.b2 = TargetPos;
			bez.b2.x += Random.Range (-0.5f,0.5f);
		} else {
			bez.b2 = transform.position;
			bez.b1.y += TargetPos.y - transform.position.y;
		}

		MaxAdd = transform.localScale;
		transform.localScale = Vector3.zero;
		fMaxParticleSize = particle.startSize;
		particle.startSize = 0.0f;
	}
}
