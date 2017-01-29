using UnityEngine;
using System.Collections;

public class SandSpeed : MonoBehaviour {

	public float fTime = 1.0f;
	public float fMove = 1.0f;

	void Update() {
		transform.position += new Vector3 (fMove * (Time.deltaTime / fTime), 0, 0);
	}
}
