using UnityEngine;
using System.Collections;

public class NormalEffect : EffectBase {

	public float fTime = 0.25f; 
	BezierCurve.tBez bez; 
	static Sprite[] effectSprite = new Sprite[4];

	// 勝手にいれた
	public float fEndTime = 0.5f; 
	Vector3 MaxAdd = Vector3.zero;
	SpriteRenderer render = null;

	public float fRotTime = 0.25f;

	void Awake() {
		if (!effectSprite [0]) {
			effectSprite = ResourceHolder.Instance.GetResource (ResourceHolder.eResourceId.ID_ATACKEFFECT);
			//effectSprite = Resources.LoadAll<Sprite> ("Textures/Effect/PlayerAtack/atack_effect");
		}
		render = GetComponent<SpriteRenderer> ();
		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_NORMALATACK);
	}
	
	// Update is called once per frame
	void Update () {

		transform.eulerAngles += new Vector3 (0,0, 360 * (Time.deltaTime / fRotTime));

		if (bez.t >= 1.0f) {
			render.color -= new Color (0, 0, 0, 1.0f * (Time.deltaTime / fEndTime));
			transform.localScale += MaxAdd * (Time.deltaTime / fEndTime);

			if (render.color.a <= 0.5f) {
				bAtack = true;
			}

			if (render.color.a <= 0.0f) {
				bEnd = true;
				//Destroy (this.gameObject); // 消す
			}
		} else {
			bez.t += 1.0f * (Time.deltaTime / fTime);
			if(bez.t >= 1.0f) {
				bez.t = 1.0f;
			}
			transform.position = BezierCurve.CulcBez (bez);

			if (transform.localScale.x >= MaxAdd.x)
				return;
			
			transform.localScale += MaxAdd * (Time.deltaTime / (fTime / 2.0f));
		}
	}

	public override void Set(UnoStruct.eColor color, Vector3 TargetPos) {
		bez.b0 = transform.position;
		bez.b2 = TargetPos;
		bez.b1 = (bez.b2 - bez.b0) / 2.0f + bez.b0;
		bez.b1.x = bez.b0.x;

		MaxAdd = transform.localScale;
		transform.localScale = Vector3.zero;

		switch (color) {
		case UnoStruct.eColor.COLOR_RED:
			GetComponent<SpriteRenderer> ().sprite = effectSprite [0];
			break;
		case UnoStruct.eColor.COLOR_BLUE:
			GetComponent<SpriteRenderer> ().sprite = effectSprite [1];
			break;
		case UnoStruct.eColor.COLOR_YELLOW:
			GetComponent<SpriteRenderer> ().sprite = effectSprite [2];
			break;
		case UnoStruct.eColor.COLOR_GREEN:
			GetComponent<SpriteRenderer> ().sprite = effectSprite [3];
			break;
		}
	}
}
