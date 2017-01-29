using UnityEngine;
using System.Collections;

public class CharaDrower : MonoBehaviour {

	[SerializeField]
	SpriteRenderer render = null;

	[SerializeField]
	float fTime = 0.5f;

	[SerializeField]
	Vector3 CenterPos = new Vector3(0,0,0);

	Vector3 StartPos = new Vector3(0,0,0);

	[SerializeField]
	Vector3 EndPos = new Vector3(0,0,0);

	bool bIn = true;
	public bool bRun { get; private set; }

	void Start() {
		StartPos = transform.position;
		render.color = new Color (1,1,1,0);
		bRun = true;
	}

	public void End() {
		bIn = false;
		bRun = true;
	}

	void Update () {
		
		if (bIn) {
			if (Application.platform == RuntimePlatform.Android) {
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
					Vector2 pos = Input.GetTouch (0).position;
					Vector2 Point = Camera.main.ScreenToWorldPoint (pos);
					Collider2D collider2D = Physics2D.OverlapPoint (Point);
					if (collider2D) {
						bIn = false;
						bRun = true;
					}
				}
			} else {
				if (Input.GetMouseButtonDown (0)) {
					Vector2 pos = Input.mousePosition;
					Vector2 Point = Camera.main.ScreenToWorldPoint (pos);
					Collider2D collider2D = Physics2D.OverlapPoint (Point);
					if (collider2D) {
						bIn = false;
						bRun = true;
					}
				}
			}
		}

		if (!bRun) {
			return;
		}

		if (bIn) {
			if (render.color.a < 1.0f) {
				render.color += new Color (0, 0, 0, 1.0f * (Time.deltaTime / fTime));
			}
			transform.position += (CenterPos - transform.position) * (Time.deltaTime / fTime);

			if(transform.position.x <= CenterPos.x + 0.1f) {
			//if (render.color.a >= 1.0f) {
                bRun = false;
                transform.position = CenterPos + new Vector3(0.1f,0,0);
			}

		} else {

			render.color -= new Color (0,0,0, 1.0f * (Time.deltaTime/ fTime));
			transform.position -= (transform.position - EndPos) * (Time.deltaTime / fTime);

			if(transform.position.x <= EndPos.x + 0.1f) {
			//if (render.color.a <= 0.0f) {
				Destroy (this.gameObject);
			}
		}
	}
}
