using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

	int nNow = 0;
	bool bEnd = false;
	List<Dissolver> DissolverList = new List<Dissolver>();

	float fNowWait = 0.0f;
	public float fWait = 1.0f;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < transform.childCount; i++) {
			Dissolver temp = transform.GetChild (i).GetComponent<Dissolver> ();
			temp.enabled = false;
			DissolverList.Add (temp);
		}
		DissolverList [nNow].enabled = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (End) {
			return;
		}

		if (TouchManager.Instance.TouchCheck ()) {
			for (int i = 0; i < DissolverList.Count; i++) {
				DissolverList [i].enabled = true;
				DissolverList [i].Skip ();
			}
			End = true;
			return;
		}

		if (nNow < DissolverList.Count) {
			if (DissolverList [nNow].End) {
				nNow++;

				if (DissolverList.Count != nNow) {
					DissolverList [nNow].enabled = true;
				}
			}
		} else {

			fNowWait += Time.deltaTime;

			if (fNowWait >= fWait) { 	
				End = true;
			}
		}
	}

	public bool End {
		get { return bEnd; }
		set { bEnd = value; }
	}
}
