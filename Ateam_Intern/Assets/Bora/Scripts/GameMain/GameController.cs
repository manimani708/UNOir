using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	static GameController instance;

	public static GameController Instance {
		get {
			if (instance == null) {
				instance = (GameController)FindObjectOfType(typeof(GameController));

				if (instance == null) {
					Debug.LogError("GameController Instance Error");
				}
			}
			return instance;
		}
	}

	public bool bStop { get; private set; }

	void Awake() {
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}
		bStop = false;
	}

	void Start() {
		SoundManager.Instance.PlayBGM (SoundManager.eBgmValue.BGM_ENEMYBATTLE);
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.O)) {
			SetDelta (0.0f);
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			SetDelta (1.0f);
		}
	}

	public void SetDelta(float fDelta, bool bFlg = false) {

		if (Time.timeScale > 0.0f && fDelta <= 0.0f) {
			bStop = true;
			//SoundManager.Instance.PauseSE (true);
		} else if(Time.timeScale <= 0.0f && fDelta > 0.0f) {
			bStop = false;
			//SoundManager.Instance.PauseSE (false);
		}

		if (bFlg) {
			if (fDelta <= 0.0f) {
				SoundManager.Instance.PauseSE (true);
			} else {
				SoundManager.Instance.PauseSE (false);
			}
		}

		Time.timeScale = fDelta;
	}
}
