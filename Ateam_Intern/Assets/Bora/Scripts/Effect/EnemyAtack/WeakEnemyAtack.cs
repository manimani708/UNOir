using UnityEngine;
using System.Collections;

public class WeakEnemyAtack : EffectBase {

	public float fTime = 0.25f; 
	BezierCurve.tBez bez; 
	bool bDamageRed = true;

	// Update is called once per frame
	void Update () {

		if (bez.t >= 1.0f) {
			bEnd = true;

			if (!bAtack) {
				bAtack = true;
				if (bDamageRed) {
					DamageRed.Instance.Run ();
				}
			}
		} else {
			bez.t += 1.0f * (Time.deltaTime / fTime);
			if(bez.t >= 1.0f) {
				bez.t = 1.0f;
			}
			if (bReverse && bez.t > 0.5f) {
				ReverseEffectManager.Instance.Run ();
				bReverse = false;
			}

			transform.position = BezierCurve.CulcBez (bez, true);
		}
	}

	public void Rivision(float fRiv) {
		bez.b2.x += fRiv;
	}

	public override void Set(Vector3 TargetPos, bool bFlg) {
		bReverse = bFlg;
		bDamageRed = !bFlg;
		bez.b0 = transform.position;
		bez.b0.z = -10.0f;
		bez.b1 = (TargetPos - bez.b0) / 2.0f + bez.b0;
		//bez.b1.x = bez.b0.x;

		if (!bReverse) {
			bez.b2 = TargetPos;
			bez.b2.x += Random.Range (-0.5f,0.5f);
		} else {
			bez.b2 = transform.position;
			bez.b1.y += TargetPos.y - transform.position.y;
		}
		//transform.localScale = Vector3.zero;
	}
}
