using UnityEngine;
using System.Collections;

public class LeaderSkillRise : LeaderSkillBase {

	public int nAdd = 2; // 倍率

	// Use this for initialization
	void Start () {

		UnoStruct.tCard card;
		card.m_Color = SkillColor;

		for(int i = 0; i <= (int)UnoStruct.eNumber.NUMBER_DROWTWO; i++) {
			card.m_Number = (UnoStruct.eNumber)i;
			UnoCreateManager.Instance.SetDict (card, nAdd, true, i == (int)UnoStruct.eNumber.NUMBER_DROWTWO);
		}
	}
}
