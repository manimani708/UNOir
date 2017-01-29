using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;

public class Result : ResultBase {

	// Update is called once per frame
	//public override void Update () {
	//	base.Update ();
	//}

	public override void Yes() {
		// 終了
		SceneChanger.Instance.ChangeScene ("MainMenu", 1.0f, true);
		//SceneManager.LoadScene ("Title");
		//ResultManager.Instance.EndResult();
	}
}
