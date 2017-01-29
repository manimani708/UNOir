using UnityEngine;
using System.Collections;

public class WeakEnemyStart : EffectBase {

	[SerializeField]
	SpriteRenderer render = null;

	[SerializeField]
	float fTime = 0.5f;

	Vector3 MaxScale = Vector3.zero;

	//[SerializeField]
	//bool bStart = true;

	[SerializeField]
	float fNextAlpha = 0.25f;

	void Awake () {
		MaxScale = transform.localScale;
		transform.localScale = Vector3.zero;
	}

	void Update () {
		if (render.color.a <= 0.0f)
			return;

		transform.eulerAngles += new Vector3(0,0, 360 * Time.deltaTime);

		if (render.color.a <= fNextAlpha) {
			bEnd = true;
			render.color -= new Color (0, 0, 0, 0.5f * (Time.deltaTime / (fTime * 2.0f)));
		} else {
			render.color -= new Color (0, 0, 0, 0.5f * (Time.deltaTime / fTime));
		}

		if (transform.localScale.x < MaxScale.x) {
			transform.localScale += MaxScale * (Time.deltaTime / fTime);
		}
	}
}
