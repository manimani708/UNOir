using UnityEngine;
using System.Collections;

public class ReturnQuest : MonoBehaviour {

	public void OnClick() {
		SceneChanger.Instance.ChangeScene ("QuestSelect", 1.0f, false);
	}
}
