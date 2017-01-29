using UnityEngine;
using System.Collections;

public class ContinueMessage : MonoBehaviour {

	[SerializeField]
	Sprite[] messageList = new Sprite[3];
	SpriteRenderer render = null;
	public int nValue = 0;
	bool bRun = true;

	Vector3 InitScale = Vector3.zero;
	public float fTime = 1.0f;
	public Vector3 MaxScale = Vector3.zero;

	public void SetValue(int i) {
		nValue = i;
		render.sprite = messageList [nValue];
		bRun = true;
		render.enabled = true;
		transform.localScale = Vector3.zero;
	}

	public void End() {
		render.enabled = false;
		transform.localScale = Vector3.zero;
	}

	void Start() {
		render = GetComponent<SpriteRenderer> ();
		render.enabled = false;
		InitScale = transform.localScale;
		transform.localScale = Vector3.zero;
	}

	void Update () {

		if (!bRun)
			return;

		transform.localScale += MaxScale * (Time.deltaTime / fTime);

		if (transform.localScale.x >= MaxScale.x) {
			transform.localScale = MaxScale;
			bRun = false;
		}
	}
}
