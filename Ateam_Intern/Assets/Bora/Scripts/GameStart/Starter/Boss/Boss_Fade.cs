using UnityEngine;
using System.Collections;

public class Boss_Fade : MonoBehaviour {

	public float fTime = 0.25f;
	SpriteRenderer renderer = null;
	bool bIn = true;

	public Boss_TextMove textMove = null;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<SpriteRenderer> ();
		renderer.color = new Color (1,1,1,0);
	}

	// Update is called once per frame
	void Update () {
		if (textMove.bEnd) {
			renderer.color = new Color (1,1,1,0.5f);
			return;
		}

		if (bIn) {
			renderer.color += new Color (0,0,0, 1.0f * (Time.deltaTime/fTime));

			if (renderer.color.a >= 1.0f)
				bIn = false;
		} else {
			renderer.color -= new Color (0,0,0, 1.0f * (Time.deltaTime/fTime));

			if (renderer.color.a <= 0.0f)
				bIn = true;
		}
	}
}
