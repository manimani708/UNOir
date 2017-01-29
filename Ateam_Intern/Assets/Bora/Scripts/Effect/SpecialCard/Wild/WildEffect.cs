using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WildEffect : MonoBehaviour {

	bool bRun = true;
	public bool bEnd { get; set; }
	SpriteRenderer render = null;

	[SerializeField]
	float fTime = 0.5f;

	void Start() {
		bEnd = true;
		render = GetComponent<SpriteRenderer> ();
		render.color = new Color (1,1,1,0);
	}

	void Update () {

		if (bEnd) {
			return;
		}
	
		//	加算
		if (bRun) {
			render.color += new Color (0,0,0, 1.0f * (Time.deltaTime / fTime));

			if (render.color.a >= 1.0f) {
				bEnd = true;
			}
		} 
		//	減衰
		else {
			render.color -= new Color (0,0,0, 1.0f * (Time.deltaTime / fTime));

			if (render.color.a <= 0.0f) { 
				bEnd = true;
			}
		}
	}

	public void Run(bool bFlg) {
		bRun = bFlg;
		bEnd = false;
	}

	public void Stop() {
		bEnd = true;
		render.color = new Color (1,1,1,0);
	}
}
