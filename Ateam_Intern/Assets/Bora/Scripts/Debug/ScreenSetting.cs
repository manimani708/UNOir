using UnityEngine;
using System.Collections;

public class ScreenSetting : MonoBehaviour {

	// Use this for initialization
	void Awake() {

		if (Application.platform != RuntimePlatform.Android) {
			Screen.SetResolution (540, 960, false, 60);
		}
		Destroy (this.gameObject);
	}
}
