using UnityEngine;
using System.Collections;

public class BackRot : MonoBehaviour {

	[SerializeField]
	float fTime = 20.0f;

	void Update () {
		transform.eulerAngles += new Vector3 (0,0, 360 * (Time.deltaTime / fTime));
	}
}
