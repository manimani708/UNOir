using UnityEngine;
using System.Collections;

public class ReverseEffect : MonoBehaviour {

	bool bEnd = false;
	bool bRun = false;
	public PurunManju purun = null;
	SpriteRenderer render = null;

	bool bStart = false;
	float fStartTime = 0.25f;
	float fAlphaTime = 0.5f;
	float fInitAlpha = 0.0f;

	void Start () {
		render = GetComponent<SpriteRenderer> ();
		fInitAlpha = render.color.a;
		Color c = render.color;
		render.color = new Color (c.r, c.g, c.b, 0);
	}

	void Update () {

		// 出る
		if (!bStart) {
			render.color += new Color (0,0,0, fInitAlpha * (Time.deltaTime / fStartTime));

			if (render.color.a >= fInitAlpha) {
				bStart = true;
			}
		}

		if (!bRun)
			return;

		// 消える
		if (!bEnd && purun.Counter <= 0.0f) {
			bEnd = true;
			ReverseEffectManager.Instance.SetNext ();
		}

		if(bEnd) {
			render.color -= new Color (0,0,0, fInitAlpha * (Time.deltaTime / fAlphaTime));

			if (render.color.a <= 0.0f) {
				Destroy(this.gameObject);
			}
		}
	}

	public void Run() {
		bRun = true;
		purun.Play ();
	}
}
