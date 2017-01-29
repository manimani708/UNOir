using UnityEngine;
using System.Collections;

public class CharactorDrowerCreator : MonoBehaviour {

	int nNowChara = 0;

	[SerializeField]
	GameObject charaDrowerObj = null;

	[SerializeField]
	Sprite[] charaSprite;

	// 現在表示しているCharaDrower
	CharaDrower charaDrower = null;

	float fNowChangeTime = 0.0f;
	[SerializeField]
	float fChangeTime = 5.0f;

	// Use this for initialization
	void Start () {
		nNowChara = Random.Range (0,charaSprite.Length);
		Create ();
	}
	
	// Update is called once per frame
	void Update () {

		if (!charaDrower.bRun) {
			fNowChangeTime += Time.deltaTime;
			if (fNowChangeTime >= fChangeTime) {
				charaDrower.End();
				Create ();
				fNowChangeTime = 0.0f;
			}
		}

		if (Application.platform == RuntimePlatform.Android) {
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
				Vector2 pos = Input.GetTouch (0).position;
				Vector2 Point = Camera.main.ScreenToWorldPoint (pos);
				Collider2D collider2D = Physics2D.OverlapPoint (Point);
				if (collider2D) {
					Create ();
				}
			}
		} else {
			if (Input.GetMouseButtonDown (0)) {
				Vector2 pos = Input.mousePosition;
				Vector2 Point = Camera.main.ScreenToWorldPoint (pos);
				Collider2D collider2D = Physics2D.OverlapPoint (Point);
				if (collider2D) {
					Create ();
				}
			}
		}
	}

	void Create() {
		fNowChangeTime = 0.0f;
		GameObject temp = (GameObject)Instantiate (charaDrowerObj);
		temp.GetComponent<SpriteRenderer>().sprite = charaSprite[nNowChara];
		nNowChara = (nNowChara + 1) % charaSprite.Length;
		charaDrower = temp.GetComponent<CharaDrower> ();
		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_CHARACHANGE);
	}
}
