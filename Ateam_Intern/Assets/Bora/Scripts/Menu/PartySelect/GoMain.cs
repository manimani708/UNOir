using UnityEngine;
using System.Collections;

public class GoMain : MonoBehaviour {

	void Start() {
		SoundManager.Instance.PlayBGM (SoundManager.eBgmValue.BGM_PARTY);
	}

	public void OnClick() {
		SceneChanger.Instance.ChangeScene ("Main", 1.0f, true);
	}
}
