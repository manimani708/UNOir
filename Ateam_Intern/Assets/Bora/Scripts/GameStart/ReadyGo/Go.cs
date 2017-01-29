using UnityEngine;
using System.Collections;

public class Go : MonoBehaviour {

	public float fTime = 0.25f;
	public Vector3 MaxSize = Vector3.zero;

	public float fFadeTime = 0.25f;

	//Transform transform = null;
	SpriteRenderer renderer = null;
	public bool bEnd { get; private set; } 

	// Use this for initialization
	void Start () {
		//rectTransform = GetComponent<RectTransform> ();
		bEnd = false;
		renderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.localScale.x >= MaxSize.x) {
			renderer.color -= new Color (0,0,0, 1.0f * (Time.deltaTime / fFadeTime));

			if (renderer.color.a <= 0.0f) {
				bEnd = true;
			}

		} 
		transform.localScale += MaxSize * (Time.deltaTime / fTime);
	}
}
