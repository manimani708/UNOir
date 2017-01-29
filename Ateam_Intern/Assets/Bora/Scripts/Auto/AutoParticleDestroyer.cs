using UnityEngine;
using System.Collections;

public class AutoParticleDestroyer : MonoBehaviour {

	ParticleSystem particle = null;

	void Start() {
		particle = GetComponent<ParticleSystem> ();
	}

	// Update is called once per frame
	void Update () {
		if (particle.time >= particle.duration) {
			Destroy (this.gameObject);
		}
	}
}
