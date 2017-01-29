using UnityEngine;
using System.Collections;

public class AutoDeatroyNotIsPlaying : MonoBehaviour {

	ParticleSystem particle = null;

	void Start() {
		particle = GetComponent<ParticleSystem> ();
	}

	// Update is called once per frame
	void Update () {
		if (!particle.isPlaying) {
			Destroy (this.gameObject);
		}
	}
}
