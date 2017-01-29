using UnityEngine;
using System.Collections;

public class BossNormalAtack : EffectBase {

	BezierCurve.tBez bez;
	float fReverseTime = 0.0f;
	bool bMyReverse = false;

	float fNowTime = 0.0f; 
	public float fWait = 0.5f;  
	public float fTime = 0.25f;
	Vector3 Distance = Vector3.zero;
	public Vector3 TargetPos = new Vector3 (0, 0.45f, 0);
	public Vector3 InitRot = new Vector3 (0, 0, 20);

	SpriteRenderer render = null;
	public float fFadeTime = 0.5f;

	// Use this for initialization
	void Start () {
		render = GetComponent<SpriteRenderer> ();
		transform.eulerAngles = InitRot;
		Distance = TargetPos - transform.position;
	}
	
	// Update is called once per frame
	void Update () {	

		if (bEnd)
			return;

		if (fNowTime <= fWait) {
			fNowTime += Time.deltaTime;
			return;
		}

		// リバース実行
		if (bez.t >= 0.5f) {
			bReverse = false;
			bAtack = true;
		}

		if(bez.t < 1.0f) {
		//if (fNowTime <= fTime + fReverseTime + fWait) {
			fNowTime += Time.deltaTime;

			bez.t += 1.0f * (Time.deltaTime / (fTime + fReverseTime));
			if(bez.t >= 1.0f) {
				bez.t = 1.0f;
			}
			transform.position = BezierCurve.CulcBez (bez);
			transform.eulerAngles -= InitRot * (Time.deltaTime / fTime); 
		} 
		else {
		///if(bez.t >= 0.5f) {
			render.color -= new Color (0,0,0, 1.0f * (Time.deltaTime / fFadeTime));

			if (bMyReverse) {
				transform.position += new Vector3 (bez.b0.x * (Time.deltaTime / fFadeTime), 0, 0);
			} else {
				bez.t += 1.0f * (Time.deltaTime / ((fTime + fReverseTime) * 10.0f));
				transform.position = BezierCurve.CulcBez (bez);
			}

			if (render.color.a <= 0.0f) {
				bEnd = true;
			}
		}
	}

	public override void Set(bool bFlg) {
		bMyReverse = bReverse = bFlg;
		Distance = TargetPos - transform.position;

		if(bReverse) {
			bez.t = 0.0f;
			bez.b0 = transform.position;
			bez.b1 = transform.position + (Distance); //* 2.0f); 
			bez.b1.x = transform.position.x;
			bez.b2 = EnemyPos.Pos;
			fReverseTime = fTime * 1.0f;
		} else {
			bez.t = 0.0f;
			bez.b0 = transform.position;
			bez.b1 = transform.position + (Distance / 2.0f); 
			bez.b2 = TargetPos;
		}
	}
}
