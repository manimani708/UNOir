using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReturnGame : MonoBehaviour {

	[SerializeField]
	Button button = null;

	[SerializeField]
	GameObject poseWindow = null;

	public void OnClick() {
		GamePause.UnPause ();
		poseWindow.SetActive (false);
		button.enabled = true;
        SoundManager.Instance.PlaySE(SoundManager.eSeValue.SE_OFFWINDOW);
        SoundManager.Instance.PauseBGM(SoundManager.eBgmValue.BGM_ATACKUP, false);
        SoundManager.Instance.PauseBGM(SoundManager.eBgmValue.BGM_THUNDERNOW, false);
	}
}
