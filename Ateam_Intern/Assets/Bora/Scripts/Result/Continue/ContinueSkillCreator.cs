using UnityEngine;
using System.Collections;

public class ContinueSkillCreator : MonoBehaviour {

	int nValue = 0;
	public GameObject continueSkillObj = null;

	// Use this for initialization
	void Start () {
		
	}

	public void SetValue(int n) {
		nValue = n;
	}

	public void Set() {

		switch(nValue) {

		case 0:
			continueSkillObj.AddComponent<ContinueAtackUp> ();
			break;
		case 1:
			continueSkillObj.AddComponent<ContinueHeel> ();
			break;
		case 2:
			continueSkillObj.AddComponent<ContinueMaxFever> ();
			break;
		}
	}
}
