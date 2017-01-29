using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

	[SerializeField]
	float fTime = 2.0f;

	[SerializeField]
	float fRange = 0.5f;

	[SerializeField]
	Enemy enemy = null;

	bool bAdd = true;
	Vector3 InitPos = Vector3.zero;
	Transform[] child = new Transform[2];

	void Start () {
		InitPos = transform.position;

		for (int i = 0; i < child.Length; i++) {
			child [i] = transform.GetChild (i);
		}
	}

	void Update () {
		
		if (enemy.isDead || enemy.gaugeSpeed < 1.0f)
			return;

		Vector2[] tempPos = new Vector2[child.Length];
		for (int i = 0; i < child.Length; i++) {
			tempPos[i] = child [i].position;
		}

		if (bAdd) {
			transform.position += new Vector3 (0, fRange * (Time.deltaTime / fTime), 0);

			if (transform.position.y >= InitPos.y + fRange) {
				bAdd = false;
			}
		} else {
			transform.position -= new Vector3 (0, fRange * (Time.deltaTime / fTime), 0);

			if (transform.position.y <= InitPos.y) {
				bAdd = true;
			}
		}
		for (int i = 0; i < child.Length; i++) {
			child [i].position = tempPos[i];
		}
	}
}
