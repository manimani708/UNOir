using UnityEngine;
using System.Collections;

public class EnemyPos : MonoBehaviour {

	static public Vector3 Pos { get; private set; }

	void Awake() {
		Pos = transform.position;
	}
}
