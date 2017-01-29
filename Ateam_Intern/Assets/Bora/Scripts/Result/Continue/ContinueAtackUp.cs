using UnityEngine;
using System.Collections;

public class ContinueAtackUp : MonoBehaviour {

	public float fAtackAmount = 1.25f;

	void Start() {
		TurnData.Instance.AddContinueAtack (GetComponent<ContinueAtackUp>());
	}

	public float AtackAmount {
		get { return fAtackAmount; }
	}
}
