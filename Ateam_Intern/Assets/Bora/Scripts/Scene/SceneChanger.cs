using UnityEngine;
using System.Collections;

// シーン遷移用
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

	//----- Singleton
	private static SceneChanger instance;

	public static SceneChanger Instance {
		get {
			if (instance == null) {
				instance = (SceneChanger)FindObjectOfType(typeof(SceneChanger));

				if (instance == null) {
					Debug.LogError(typeof(SceneChanger) + "is nothing");
				}
			}

			return instance;
		}
	}

	public void Awake() {
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}

		DontDestroyOnLoad (this.gameObject);
	}

	public void ChangeScene(string sceneName, float interval, bool bNext, bool bStopBgm = true) {
		if (!FadeManager.Instance.GetFadeing()) {
			FadeManager.Instance.LoadLevel(sceneName, interval, bStopBgm);

            if (bNext)
            {
                SoundManager.Instance.PlaySE(SoundManager.eSeValue.SE_SCENECHANGE);
            }
            else
            {
                SoundManager.Instance.PlaySE(SoundManager.eSeValue.SE_OFFWINDOW);
            }
		}
	}

	public void ChangeTitle() {
		if (!FadeManager.Instance.GetFadeing()) {
			FadeManager.Instance.LoadLevel("Title", 1.0f, true);
			//SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_TOUCHSTART);
		}
	}
		
	public void ChangeMainMenu() {
		if (!FadeManager.Instance.GetFadeing()) {
			FadeManager.Instance.LoadLevel("MainMenu", 1.0f, true);
			//SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_TOUCHSTART);
		}
	}

	public void ChangeGameMain() {
		if (!FadeManager.Instance.GetFadeing()) {
			FadeManager.Instance.LoadLevel("Test", 1.0f, true);
			//SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_TOUCHSTART);
		}
	}
}
