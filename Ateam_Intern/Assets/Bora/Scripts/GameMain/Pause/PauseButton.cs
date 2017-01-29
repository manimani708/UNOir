using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour {

	[SerializeField]
	Button button = null;

	[SerializeField]
	GameObject poseWindow = null;

	public void Pause() {
		if (!BattleManager.Instance.GetIsInBattle ())
			return;

		GamePause.Pause ();
		poseWindow.SetActive (true);
		button.enabled = false;
        SoundManager.Instance.PlaySE(SoundManager.eSeValue.SE_ONWINDOW);

        SoundManager.Instance.PauseBGM(SoundManager.eBgmValue.BGM_ATACKUP, true);
        SoundManager.Instance.PauseBGM(SoundManager.eBgmValue.BGM_THUNDERNOW, true);
	}
}
