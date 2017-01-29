using UnityEngine;
using System.Collections;

public class FeverEffect : MonoBehaviour {

	UnoData unoData = null;
	SpriteRenderer render = null;

	float fAddTime = 0.15f;
	bool bAdd = false; 

	float fChangeTime = 0.15f; 
	Vector3 InitScale = Vector3.zero;

	// Use this for initialization
	void Start () {
		bAdd = true;
		render = GetComponent<SpriteRenderer> ();
		unoData = GetComponentInParent<UnoData> ();
		InitScale = new Vector3(1.2f,1.2f,1.2f);
		render.sortingOrder = -1;
	}

	// Update is called once per frame
	void Update () {
	
		if (FeverEffectManager.Instance.GetFever()) {

			if (InitScale.x > transform.localScale.x) {
				transform.localScale += InitScale * (Time.deltaTime / fChangeTime);
			} else if (bAdd) {
				render.color += new Color (0,0,0, 1.0f * (Time.deltaTime / fAddTime));

				if (render.color.a >= 1.0f) {
					bAdd = false;
					render.color = new Color (1, 1, 1, 1);
				}
			} else {
				render.color -= new Color (0,0,0, 1.0f * (Time.deltaTime / fAddTime));

				if (render.color.a <= 0.0f) {
					bAdd = true;
					render.color = new Color (1, 1, 1, 0);
				}
			}
			render.sortingOrder = unoData.GetOreder () - 1;
		} else {

			if (0.0f < transform.localScale.x) {
				transform.localScale -= InitScale * (Time.deltaTime / fChangeTime);
			} else {
				transform.localScale = Vector3.zero;
			}
		}
	}
}
