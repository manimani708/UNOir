using UnityEngine;
using System.Collections;

public class Retire : MonoBehaviour {

    public void OnClick() {
        GameController.Instance.SetDelta(1.0f);
        GamePause.UnPause();
		SceneChanger.Instance.ChangeScene ("MainMenu", 1.0f, false);
	}
}
