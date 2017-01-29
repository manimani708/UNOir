using UnityEngine;
using System.Collections;

public class GamePause : MonoBehaviour {

	static public bool bPause { get; private set; }

	void Awake() {
		bPause = false;
	}

	static public void Pause() {
		bPause = true;
		GameController.Instance.SetDelta (0.0f, true);
	}

	static public void UnPause() {
		bPause = false;
		GameController.Instance.SetDelta (1.0f, true);
	}
}
