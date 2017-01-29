using UnityEngine;
using System.Collections;

public class BossSpecialAtack : EffectBase {

	BezierCurve.tBez bez;
	Vector3 Distance = new Vector3(0,0,0);
	public Vector3 TargetPos = new Vector3(0,0.5f,0);
	float fNowTime = 0.0f;
	public float fTime = 0.5f;
	public float fRotTime = 0.25f;

	public float fFadeTime = 0.25f;
	SpriteRenderer render = null;

	public float fScaleTime = 0.25f;
	Vector3 MaxScale = Vector3.zero;

	public bool bNext { get; private set; }
	public bool bGo { get; set; }

	[SerializeField]
	Transform child = null;

	// Use this for initialization
	void Awake () {
		bNext = false;
		bGo = false;
		MaxScale = transform.localScale;
		transform.localScale = Vector3.zero;
		render = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (bEnd)
			return;

		transform.eulerAngles += new Vector3 (0,0, 360 * (Time.deltaTime / fRotTime));
		child.transform.eulerAngles -= new Vector3 (0,0, 720 * (Time.deltaTime / fRotTime));

		if (bGo) {
			fNowTime += Time.deltaTime;

			if(bez.t < 1.0f) {
			//if (fNowTime <= fTime) {
				bez.t += 1.0f * (Time.deltaTime / fTime);
				if(bez.t >= 1.0f) {
					bez.t = 1.0f;
					bAtack = true;
				}
				if (bReverse && bez.t >= 0.5f) {
					bReverse = false;
				}

				transform.position = BezierCurve.CulcBez (bez);
				//transform.position += Distance * (Time.deltaTime / fTime);
			} else {
				render.color -= new Color (0, 0, 0, 1.0f * (Time.deltaTime / fFadeTime));
				transform.localScale += (MaxScale / 2.0f) * (Time.deltaTime / fFadeTime);

				if (render.color.a <= 0.0f) {
					bEnd = true;
					transform.localScale = Vector3.zero;
				}
			}

		} else if (!bNext) {
			transform.localScale += MaxScale * (Time.deltaTime / fScaleTime);

			if (transform.localScale.x >= MaxScale.x) {
				bNext = true;
			}
		}
	}

	public override void Set(bool bFlg) {
		bReverse = bFlg;
		Distance = TargetPos - transform.position;

		if(bReverse) {
			bez.t = 0.0f;
			bez.b0 = transform.position;
			bez.b1 = transform.position + (Distance);//* 2.0f); 
			bez.b2 = EnemyPos.Pos;
			fTime *= 3.0f;
		} else {
			bez.t = 0.0f;
			bez.b0 = transform.position;
			bez.b1 = transform.position + (Distance / 2.0f); 
			bez.b2 = TargetPos;
		}
	}
}
