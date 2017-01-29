using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusFrame : MonoBehaviour {

	bool bRun = false;
	bool bOn = false;

	[SerializeField]
	float fTime = 0.5f;

	[SerializeField]
	Button[] OnButton; // このスクリプトがOnの時にOnにするボタンリスト

	[SerializeField]
	Button[] OffButton;  // このスクリプトがOffの時にOnにするボタンリスト

	Vector3 InitScale = Vector3.zero;

	void Awake() {
		InitScale = transform.localScale;
		transform.localScale = Vector3.zero;
	}

	void Update() {

		if (!bRun)
			return;

		if (bOn) {
			transform.localScale += InitScale * (Time.deltaTime / fTime);

			if (transform.localScale.x >= InitScale.x) {
				transform.localScale = InitScale;
				bRun = false;

				for (int i = 0; i < OnButton.Length; i++) {
					OnButton [i].enabled = true;
				}
			}
		} else {
			transform.localScale -= InitScale * (Time.deltaTime / fTime);

			if (transform.localScale.x <= 0.0f) {
				transform.localScale = Vector3.zero;
				bRun = false;

				for (int i = 0; i < OffButton.Length; i++) {
					OffButton [i].enabled = true;
				}
			}
		}
	}

	public void On() {
		if (bRun)
			return;
		
		bOn = true;
		bRun = true;

		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_ONWINDOW);

		for (int i = 0; i < OffButton.Length; i++) {
			OffButton [i].enabled = false;
		}
	}

	public void Off() {
		if (bRun)
			return;

		bOn = false;
		bRun = true;

		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_OFFWINDOW);

		for (int i = 0; i < OnButton.Length; i++) {
			OnButton [i].enabled = false;
		}
	}
}
