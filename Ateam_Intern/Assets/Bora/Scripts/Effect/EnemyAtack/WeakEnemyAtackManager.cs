using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeakEnemyAtackManager : EffectBase {

	[SerializeField]
	EffectBase effect = null;

	[SerializeField]
	ParticleSystem particle = null;

	[SerializeField]
	WeakEnemyStart start = null;

	void Awake() {
		effect.enabled = false;
		particle.Stop ();
	}

	void Update() {

		if (start && start.bEnd) {
			effect.enabled = true;
			particle.Play ();
			start = null;
			SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_WEAKENEMYATACK);
			return;
		}

		if (!bAtack) {
			if (effect.bEnd) {
				particle.loop = false;
				bAtack = true;
			}
		} else if (!particle.isPlaying) {
			bEnd = true;
		}
	}

	public override void Set(Vector3 TargetPos, bool bFlg) {
		transform.position -= new Vector3 (0.4f,0.5f,0.0f);
		effect.Set (TargetPos, bFlg);
	}
}
