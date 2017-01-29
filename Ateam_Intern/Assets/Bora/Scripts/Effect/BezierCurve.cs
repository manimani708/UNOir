using UnityEngine;
using System.Collections;

public class BezierCurve : MonoBehaviour {

	public struct tBez {
		public float t;				// 時間
		public Vector3 b0,b1,b2;	// 点
	};

	static public Vector3 CulcBez(tBez bez) {

		float x = (1 - bez.t) * (1 - bez.t) * bez.b0.x + 2 * (1 - bez.t) * bez.t * bez.b1.x + bez.t * bez.t * bez.b2.x;
		float y = (1 - bez.t) * (1 - bez.t) * bez.b0.y + 2 * (1 - bez.t) * bez.t * bez.b1.y + bez.t * bez.t * bez.b2.y;
		return new Vector3 (x, y, 0.0f);
	}

	static public Vector3 CulcBez(tBez bez, bool bZ) {

		float x = (1 - bez.t) * (1 - bez.t) * bez.b0.x + 2 * (1 - bez.t) * bez.t * bez.b1.x + bez.t * bez.t * bez.b2.x;
		float y = (1 - bez.t) * (1 - bez.t) * bez.b0.y + 2 * (1 - bez.t) * bez.t * bez.b1.y + bez.t * bez.t * bez.b2.y;
		float z = (1 - bez.t) * (1 - bez.t) * bez.b0.z + 2 * (1 - bez.t) * bez.t * bez.b1.z + bez.t * bez.t * bez.b2.z;
		return new Vector3 (x, y, z);
	}
}
