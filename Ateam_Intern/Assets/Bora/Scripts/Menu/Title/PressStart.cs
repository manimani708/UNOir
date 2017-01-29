using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PressStart : MonoBehaviour {

	Image image = null;
	public float fTime = 0.75f; 
	bool bIn = true;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();

		SoundManager.Instance.PlayBGM (SoundManager.eBgmValue.BGM_TITLE);
	}
	
	// Update is called once per frame
	void Update () {

		if (bIn) {
			image.color -= new Color (0, 0, 0, 1.0f * (Time.deltaTime / fTime));

			if (image.color.a <= 0.0f) {
				bIn = false;
			}
		} else {
			image.color += new Color (0, 0, 0, 1.0f * (Time.deltaTime / fTime));

			if (image.color.a >= 1.0f) {
				bIn = true;
			}
		}

		if (Application.platform == RuntimePlatform.Android) {
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began && !FadeManager.Instance.GetFadeing()) {
				SceneChanger.Instance.ChangeMainMenu ();
				SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_TOUCHSTART);
			}
		} else {
			if (Input.GetMouseButtonDown (0) && !FadeManager.Instance.GetFadeing()) {
				SceneChanger.Instance.ChangeMainMenu ();
				SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_TOUCHSTART);
			}
		}
	}
}
