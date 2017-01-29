using UnityEngine;
using System.Collections;

public class GameOver : ResultBase {

	public Result result = null;
	public Continue continueCs = null;

	public override void Start() {
		
	}

	// Update is called once per frame
	public override void Update () {
		base.Update ();
	}

	public override void Yes() {
		continueCs.bOn = true;
		continueCs.ReInit ();
		bOn = false;
		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_SCENECHANGE);
	}

	public override void No() {

		SceneChanger.Instance.ChangeScene ("MainMenu", 1.0f, false);
		//result.bOn = true;
		//result.SetFlg (false);
		//bOn = false;
	}
}
