using UnityEngine;
using System.Collections;

public class DrowAlpha : MonoBehaviour {

	static DrowAlpha instance;

	public static DrowAlpha Instance {
		get {
			if (instance == null) {
				instance = (DrowAlpha)FindObjectOfType(typeof(DrowAlpha));

				if (instance == null) {
					Debug.LogError("DrowAlpha Instance Error");
				}
			}

			return instance;
		}
	}

	public float a = 0.0f;
	bool bAdd = true; 
	float fAddTime = 0.2f;

	void Awake() {
		if (this != Instance) {
			Destroy(this.gameObject);
			return;
		}
	}

	void Update() {
		if (bAdd) {
			a += 1.0f * (Time.deltaTime / fAddTime);

			if (a >= 1.0f) {
				bAdd = false;
				a = 1.0f;
			}
		} else {
			a -= 1.0f * (Time.deltaTime / fAddTime);

			if (a <= 0.0f) {
				bAdd = true;
				a = 0.0f;
			}
		}
	}
}
