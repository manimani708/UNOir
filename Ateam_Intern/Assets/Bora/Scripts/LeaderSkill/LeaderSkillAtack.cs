using UnityEngine;
using System.Collections;

public class LeaderSkillAtack : LeaderSkillBase {

	[Range(0.0f,1.0f)] public float fPercentage;

	// Use this for initialization
	void Start () {
		TurnData.Instance.leaderSkillAtack = this.GetComponent<LeaderSkillAtack> ();
	}

	public float fGetPercentage() {
		return fPercentage + 1.0f;
	}
}
