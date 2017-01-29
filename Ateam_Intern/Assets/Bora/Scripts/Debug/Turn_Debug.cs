using UnityEngine;
using System.Collections;

public class Turn_Debug : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		TurnData.tTurnData data = TurnData.Instance.GetTurnData ();
	}
}
