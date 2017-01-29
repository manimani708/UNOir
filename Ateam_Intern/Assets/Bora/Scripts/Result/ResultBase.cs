using UnityEngine;
using System.Collections;

public class ResultBase : MonoBehaviour {

	public float fTime = 1.0f; 
	public Vector3 MaxScale = new Vector3 (1,1,0); 
	public bool bOn { get; set; }
	public bool bEnd { get; set; }

	public virtual void Start() {
		transform.localScale = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	public virtual void Update () {

		if (bOn) {
			
			if (transform.localScale.x < MaxScale.x) {
				transform.localScale += MaxScale * (Time.deltaTime / fTime);
				bEnd = false;
			} else {
				transform.localScale = MaxScale;
				bEnd = true;
			}

		} else {

			if (transform.localScale.x > 0.0f) {
				transform.localScale -= MaxScale * (Time.deltaTime / fTime);
				bEnd = false;
			} else {
				transform.localScale = Vector3.zero;
				bEnd = true;
			}
		}
	}

	public virtual void Yes () {

	}

	public virtual void No () {

	}
}
