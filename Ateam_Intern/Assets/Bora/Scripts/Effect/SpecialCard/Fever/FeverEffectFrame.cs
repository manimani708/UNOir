using UnityEngine;
using System.Collections;

public class FeverEffectFrame : MonoBehaviour {

	public float fTime = 1.0f;
	SpriteRenderer render = null;
	bool bAdd = false;

	//Vector3 InitScale = Vector3.zero;

	void Start() {
		render = GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {

		if (FeverEffectManager.Instance.GetFever ()) {

			if (bAdd) {
				render.color += new Color (0,0,0, 1.0f * (Time.deltaTime / fTime));

				if (render.color.a >= 1.0f) {
					bAdd = false;
					render.color = new Color (1,1,1,1);
				}
			} else {
				render.color -= new Color (0,0,0, 1.0f * (Time.deltaTime / fTime));

				if (render.color.a <= 0.0f) {
					bAdd = true;
					render.color = new Color (1,1,1,0);
				}
			}

		} else {
			render.color = new Color (1,1,1,0);
		}
	}
}
