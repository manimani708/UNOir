using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_AllFadeOut : MonoBehaviour {

	List<SpriteRenderer> rendererList = new List<SpriteRenderer>();

	public float fTime = 0.5f;
	public bool bEnd = false;

	public bool End { 
		get { return bEnd; } 
		set { bEnd = value; } 
	}

	// Use this for initialization
	void Start () {
		rendererList.Add(GetComponent<SpriteRenderer> ());
		if (transform.childCount > 0) {
			rendererList.Add (transform.GetChild (0).GetComponent<SpriteRenderer> ());
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (bEnd)
			return;

		for (int i = 0; i < rendererList.Count; i++) {
			rendererList [i].color -= new Color (0,0,0, 1.0f * (Time.deltaTime / fTime));

			if (rendererList [i].color.a <= 0.0f) {
				bEnd = true;
			}
		}

		if (bEnd) {

			for (int i = 0; i < rendererList.Count; i++) {
				rendererList [i].color -= new Color (0, 0, 0, 0);
			}
		}
	}
}
