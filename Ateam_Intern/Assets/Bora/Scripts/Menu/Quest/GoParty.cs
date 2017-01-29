using UnityEngine;
using System.Collections;

public class GoParty : MonoBehaviour {

	void Awake() {
		SoundManager.Instance.PlayBGM (SoundManager.eBgmValue.BGM_MENU);
	}

	public void OnClick() {
		SceneChanger.Instance.ChangeScene ("PartyCheck", 1.0f, true);
	}
}
