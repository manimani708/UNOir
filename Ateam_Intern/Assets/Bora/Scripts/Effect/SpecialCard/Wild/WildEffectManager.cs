using UnityEngine;
using System.Collections;

public class WildEffectManager : MonoBehaviour {

	int nCnt = 0;

	[SerializeField]
	FieldCard fieldCard = null;

	[SerializeField]
	WildEffect[] effect = new WildEffect[3];

	UnoStruct.eColor oldColor = UnoStruct.eColor.COLOR_MAX;

	SpriteRenderer render = null;

	void Start() {
		render = GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {

		if (fieldCard.m_Card.m_Color != UnoStruct.eColor.COLOR_WILD) {

			if (oldColor == UnoStruct.eColor.COLOR_WILD) {
				for (int i = 0; i < effect.Length; i++) {
					effect [i].Stop();
				}
				render.color = new Color (1, 1, 1, 1);
			}
			oldColor = fieldCard.m_Card.m_Color;
			return;
		}

		render.color = new Color (1, 1, 1, 0.5f);

		if (effect [nCnt].bEnd) {
			effect [nCnt].Run (false);
			nCnt = (nCnt + 1) % effect.Length;
			effect [nCnt].Run (true);
		}
		oldColor = fieldCard.m_Card.m_Color;
	}
}
