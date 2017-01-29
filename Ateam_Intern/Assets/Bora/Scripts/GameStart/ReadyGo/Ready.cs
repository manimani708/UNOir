using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ready : MonoBehaviour {

	float fNowTime = 0.0f;
	public float fTime = 1.0f;
	public GameObject Go = null;

	SpriteRenderer sprite = null;
	public float fFadeTime = 0.25f;

	// Use this for initialization
	void Start () {
		Go.SetActive (false);
		sprite = GetComponent<SpriteRenderer> ();

		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_READY);
	}
	
	// Update is called once per frame
	void Update () {
	
		fNowTime += Time.deltaTime;

		if (fNowTime >= fTime) {
			if (!Go.active) {
				Go.SetActive (true);
				SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_GO);
			}

			if (sprite.color.a > 0.0f) {
				sprite.color -= new Color (0,0,0, 1.0f * (Time.deltaTime / fFadeTime));
			}
		}
	}
}
