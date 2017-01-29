using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchEffect : MonoBehaviour {

	[SerializeField]
	Vector3 MaxSize = new Vector3(1,1,0);

	[SerializeField]
	float fTime = 0.5f;

	SpriteRenderer render = null;

	void Start () {
		render = GetComponent<SpriteRenderer>();
	}

	void Update () {
			
		transform.localScale += MaxSize * (Time.unscaledDeltaTime / fTime);
		render.color -= new Color(0,0,0, 1.0f * (Time.unscaledDeltaTime / fTime));

		if (render.color.a <= 0.0f) {
			Destroy (this.gameObject);
		}
	}
}
