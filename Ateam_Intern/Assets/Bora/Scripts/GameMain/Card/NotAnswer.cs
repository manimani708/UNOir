using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NotAnswer : MonoBehaviour {

	bool bPlay = false;
	public float fLife = 0.5f;
	float fInitLife = 0.0f;
	SpriteRenderer spriteRenderer = null;
	int nOrderinLayer = 22;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		fInitLife = fLife;
		spriteRenderer.color = new Color (1,1,1,0);
		spriteRenderer.sortingOrder = nOrderinLayer;
	}

	void Update () {
		if (!bPlay)
			return;

		fLife -= Time.deltaTime;
		spriteRenderer.color = new Color (1,1,1, fLife / fInitLife);

		if (fLife <= 0.0f) {
			bPlay = false;
		}
	}

	public void Play() {
		bPlay = true;
		fLife = fInitLife;
		spriteRenderer.color = new Color (1,1,1,1);
	}
}
