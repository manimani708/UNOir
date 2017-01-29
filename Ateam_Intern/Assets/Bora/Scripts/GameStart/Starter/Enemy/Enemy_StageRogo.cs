using UnityEngine;
using System.Collections;

public class Enemy_StageRogo : MonoBehaviour {

	public bool bEnd { get; private set; }

	public float fFadeTime = 0.5f;

	public float fAddTime = 1.0f;
	Vector3 MaxScale = Vector3.zero;

	public float fDelay = 1.0f;
	float fNowDelay = 0.0f;

	SpriteRenderer renderer = null;

	// Use this for initialization
	void Start () {

		renderer = GetComponent <SpriteRenderer> ();
		renderer.color = new Color (1,1,1,0);

		MaxScale = transform.localScale;
		transform.localScale = Vector3.zero;

		bEnd = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (bEnd)
			return;

		if (renderer.color.a >= 1.0f && transform.localScale.x >= MaxScale.x) {
			fNowDelay += Time.deltaTime;

			if (fNowDelay >= fDelay) {
				bEnd = true;
			}
		}

		if (fNowDelay > 0.0f)
			return;

		if (renderer.color.a < 1.0f) {
			renderer.color += new Color (0,0,0, 1.0f * (Time.deltaTime / fFadeTime));
		}

		if (transform.localScale.x < MaxScale.x) {
			transform.localScale += MaxScale * (Time.deltaTime / fAddTime);
		}
	}
}
