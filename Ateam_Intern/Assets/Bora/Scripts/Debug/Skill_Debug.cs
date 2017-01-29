using UnityEngine;
using System.Collections;

public class Skill_Debug : MonoBehaviour {

	CharaSkillBase atack;
	CharaSkillBase hell;
	CharaSkillBase delay;
	CharaSkillBase rise;

	// Use this for initialization
	void Start () {
		atack = GetComponentInChildren<SkillAtack> ();
		hell = GetComponentInChildren<SkillHell> ();
		delay = GetComponentInChildren<SkillDelay> ();
		rise = GetComponentInChildren<SkillRise> ();

		// Debug
/*		UnoStruct.tCard card;
		card.m_Color = UnoStruct.eColor.COLOR_WILD;

		card.m_Number = UnoStruct.eNumber.NUMBER_WILD;
		UnoCreateManager.Instance.SetDict (card, 20, true);

		card.m_Number = UnoStruct.eNumber.NUMBER_DROWFOUR;
		UnoCreateManager.Instance.SetDict (card, 20, true);*/
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Z)) {
			atack.Run ();
		}
		if (Input.GetKeyDown (KeyCode.X)) {
			hell.Run ();
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			delay.Run ();
		}
		if (Input.GetKeyDown (KeyCode.V)) {
			rise.Run ();
		}
	}
}
