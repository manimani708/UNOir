using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Continue : ResultBase {

	ContinueCardBase cardBase = null;
	public List<ContinueCardBase> CardList = new List<ContinueCardBase> ();
	public List<Vector3> PosList = new List<Vector3>();

	float fNowTime = 0.0f;
	public float fDelayTime = 2.0f;

	[SerializeField]
	ContinueMessage message = null;

	[SerializeField]
	ParticleSystem particle = null;

	public void ReInit() {

		for (int i = 0; i < CardList.Count; i++) {
			CardList [i].nValue = i;
		}

		for (int i = 0; i < 10; i++) {
			int nOne = Random.Range (0, CardList.Count);
			int nTwo = Random.Range (0, CardList.Count);
			int nTempValue = CardList [nOne].nValue;
			CardList [nOne].nValue = CardList [nTwo].nValue;
			CardList [nTwo].nValue = nTempValue;
		}

		for (int i = 0; i < CardList.Count; i++) {
			CardList [i].ReInit ();
		}
		fNowTime = 0.0f;
	}

	public override void Start () {
		cardBase = null;
		particle.transform.localScale = Vector3.zero;
		ReInit ();
	}

	public override void Update () {
		base.Update ();

		if (bOn) {

			if (transform.localScale.x < MaxScale.x) {
				particle.transform.localScale += new Vector3(1,1,1) * (Time.deltaTime / fTime);
				bEnd = false;
			} else {
				particle.transform.localScale = new Vector3 (1, 1, 1);
				bEnd = true;
			}

		} else {
			if (transform.localScale.x > 0.0f) {
				particle.transform.localScale -= new Vector3(1,1,1) * (Time.deltaTime / fTime);
				bEnd = false;
				SoundManager.Instance.StopSE ();
			} else {
				particle.transform.localScale = Vector3.zero;
				particle.Stop ();
				bEnd = true;
			}
		}

		if (!cardBase) {
			bool bTouch = false;
			Vector3 pos = Vector3.zero;

			if (Application.platform == RuntimePlatform.Android) {

				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
					pos = Input.GetTouch (0).position;
					bTouch = true;
				}
			} else {

				if (Input.GetMouseButtonDown (0)) {
					pos = Input.mousePosition;
					bTouch = true;
				}
			}

			if (bTouch) {
				Vector2 Point = Camera.main.ScreenToWorldPoint (pos);
				Collider2D collider2D = Physics2D.OverlapPoint (Point);

				if (collider2D && collider2D.tag == "ContinueCard") {
					cardBase = collider2D.GetComponent<ContinueCardBase> ();

					for (int i = 0; i < CardList.Count; i++) {
						if (CardList [i] == cardBase) {
							CardList [i].Run (true);
						} else {
							CardList [i].Run (false);
						}
					}
				}
			}
		} else if (!cardBase.Decision) {
			
			fNowTime += Time.deltaTime;

			if (Application.platform == RuntimePlatform.Android) {
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
					fNowTime = fDelayTime;
				}
			} else {
				if (Input.GetMouseButtonDown (0)) {
					fNowTime = fDelayTime;
				}
			}
		
			if (fNowTime < fDelayTime)
				return;

			if (bOn) {
				bOn = false;
				return;
			}

			if (!bEnd)
				return;
			
			cardBase = null;
			message.End ();	
			ResultManager.Instance.EndResult ();
		}
	}
}
