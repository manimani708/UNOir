using UnityEngine;
using System.Collections;

public class TouchEffectCreator : MonoBehaviour {

	[SerializeField]
	GameObject touchEffect = null;

	void Update () {
	
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
				Vector3 pos = Input.GetTouch (0).position;
				pos.z = 10.0f;
				pos = Camera.main.ScreenToWorldPoint (pos);
				Instantiate (touchEffect, pos, Quaternion.identity);
				//SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_TOUCH);
			}
		} else {
			if (Input.GetMouseButtonDown (0)) {
				Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,10.0f));
				Instantiate (touchEffect, pos, Quaternion.identity);
				//SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_TOUCH);
			}
		}
	}
}
