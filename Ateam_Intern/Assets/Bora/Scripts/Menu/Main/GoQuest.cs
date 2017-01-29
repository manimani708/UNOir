using UnityEngine;
using System.Collections;

public class GoQuest : MonoBehaviour {

	void Awake() {
		SoundManager.Instance.PlayBGM (SoundManager.eBgmValue.BGM_MENU);
	}

	public void OnClick() {
		SceneChanger.Instance.ChangeScene ("QuestSelect", 1.0f, true);
	}
}
