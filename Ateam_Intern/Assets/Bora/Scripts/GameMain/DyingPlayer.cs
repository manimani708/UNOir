using UnityEngine;
using System.Collections;

public class DyingPlayer : MonoBehaviour {

	[SerializeField]
	Player player = null;

	float fNowTime = 0.0f;
	[SerializeField]
	float fTime = 0.5f;

	float fNowAlpha = 0.0f;
	[SerializeField]
	float fMaxAlpha = 0.5f;

	[SerializeField]
	SpriteRenderer render = null;

	bool bAdd = true;

	void Update() {

        if (player.isDead)
        {
            return;
        }

		if (player.hpRemain > player.hpMax * 0.5f) {
            render.color = new Color(1, 1, 1, 0);
			return;
		}

		if (player.hpRemain <= player.hpMax * 0.25f) {
			fNowTime = fTime;
            fNowAlpha = fMaxAlpha;
		} else {
			fNowTime = fTime * 2.0f;
			fNowAlpha = fMaxAlpha / 2.0f;
		}

		if (bAdd) {
			render.color += new Color (0,0,0, fNowAlpha * (Time.deltaTime / fNowTime));

			if (render.color.a >= fNowAlpha) {
				bAdd = false;
			}
		} else {
			render.color -= new Color (0,0,0, fNowAlpha * (Time.deltaTime / fNowTime));

			if (render.color.a <= 0.0f) {
				bAdd = true;
			}
		}
	}
}
