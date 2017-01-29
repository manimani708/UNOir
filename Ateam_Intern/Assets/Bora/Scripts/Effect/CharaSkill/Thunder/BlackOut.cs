using UnityEngine;
using System.Collections;

public class BlackOut : MonoBehaviour {

	SpriteRenderer render = null;
	float fAlpha = 0.0f;

	void Start() {
		render = GetComponent<SpriteRenderer> ();
	}

	public float Alpha {
		get { return fAlpha; }
		set { fAlpha = value; }
	}

	void Update() {
		render.color = new Color (0,0,0, fAlpha);
	}
		
	/*public void Run(bool bFlg, float fAmount) {

		if(bFlg) {
			render.color += new Color (0,0,0,fAmount);
		} else {
			render.color -= new Color (0,0,0,fAmount);
			Debug.Log ("a");
		}
	}*/
}
