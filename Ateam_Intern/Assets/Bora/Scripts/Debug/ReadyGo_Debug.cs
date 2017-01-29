using UnityEngine;
using System.Collections;

public class ReadyGo_Debug : MonoBehaviour {

	public GameObject Obj = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.P)) {
			GameObject go = (GameObject)Instantiate (Obj, transform.position, transform.rotation);
			go.transform.SetParent (transform);
			go.transform.localScale = new Vector3 (1,1,1);
		}
	}
}
