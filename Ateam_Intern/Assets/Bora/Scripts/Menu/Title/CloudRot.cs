using UnityEngine;
using System.Collections;

public class CloudRot : MonoBehaviour {

	[SerializeField]
	float fRotTime = 1.0f;

	void Update() {
		transform.eulerAngles += new Vector3 (0,0, 360 * (Time.deltaTime / fRotTime));
	}
}
