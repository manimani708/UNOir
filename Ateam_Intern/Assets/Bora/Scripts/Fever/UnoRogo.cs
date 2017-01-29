using UnityEngine;
using System.Collections;

public class UnoRogo : MonoBehaviour {

	public float fTime = 0.75f;
	Vector3 MaxScale = Vector3.zero;

	public float fFadeTime = 0.5f;
	public Vector3 FadeAddScale = new Vector3(0.2f, 0.2f, 0.0f);

	SpriteRenderer renderer = null;

	// Use this for initialization
	void Start () {
		MaxScale = transform.localScale;
		transform.localScale = Vector3.zero;

		renderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.localScale.x < MaxScale.x) {
			transform.localScale += MaxScale * (Time.deltaTime / fTime);
		} else {
			renderer.color -= new Color(0,0,0, 1.0f * (Time.deltaTime / fFadeTime));
			transform.localScale += FadeAddScale * (Time.deltaTime / fFadeTime);

			if (renderer.color.a <= 0.0f) {
				Destroy (this.gameObject);
			}
		}
	}
}
