using UnityEngine;
using System.Collections;

public class ReturnMenu : MonoBehaviour {

	public void OnClick() {
		SceneChanger.Instance.ChangeScene ("MainMenu", 1.0f, false);
	}
}
