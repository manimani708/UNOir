using UnityEngine;
using System.Collections;

public class Charactors_Debug : MonoBehaviour {

	// Use this for initialization
	void Start () {
	foreach (Transform child in transform)
        {
            CharactorData data = child.GetComponent<Charactor_Debug>().charactorData;
            CharactorData_Debug.AddData(data);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
