using UnityEngine;
using System.Collections;

public class AutoDestroyNonChild : MonoBehaviour {
	
	void Update () {
		if (transform.childCount > 0)
			return;
		
		Destroy (this.gameObject);
	}
}
