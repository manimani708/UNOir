using UnityEngine;
using System.Collections;

public class ResetCard : MonoBehaviour {

	[SerializeField]
	float fTime = 0.2f;

	[SerializeField]
	float fMaxAlpha = 0.5f;

	[SerializeField]
	SpriteRenderer render = null;

	bool bRun = false;
	bool bAdd = true;

	void Update() {
		if (!bRun)
			return;

		if (bAdd) {
			render.color += new Color (0, 0, 0, fMaxAlpha * (Time.deltaTime / fTime));

			if (render.color.a >= fMaxAlpha) {
				bAdd = false;
				render.color = new Color (1,1,1,fMaxAlpha);
			}
		} else {
			render.color -= new Color (0, 0, 0, fMaxAlpha * (Time.deltaTime / fTime));

			if (render.color.a <= 0.0f) {
				bAdd = true;
				bRun = false;
				render.color = new Color (1,1,1,0);
			}
		}
	}

	public void Run() {
		bRun = true;
	}
}
