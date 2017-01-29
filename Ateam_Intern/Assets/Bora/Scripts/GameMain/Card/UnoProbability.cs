using UnityEngine;
using System.Collections;

public class UnoProbability : MonoBehaviour {

	public int RedZero = 1;
	public int RedOne = 1;
	public int RedTwo = 1;
	public int RedThree = 1;
	public int RedFour = 1;
	public int RedFive = 1;
	public int RedSix = 1;
	public int RedSeven = 1;
	public int RedEight = 1;
	public int RedNine = 1;
	public int RedReverse = 1;
	public int RedSkip = 1;
	public int RedDrowTwo = 1;

	public int BlueZero = 1;
	public int BlueOne = 1;
	public int BlueTwo = 1;
	public int BlueThree = 1;
	public int BlueFour = 1;
	public int BlueFive = 1;
	public int BlueSix = 1;
	public int BlueSeven = 1;
	public int BlueEight = 1;
	public int BlueNine = 1;
	public int BlueReverse = 1;
	public int BlueSkip = 1;
	public int BlueDrowTwo = 1;

	public int GreenZero = 1;
	public int GreenOne = 1;
	public int GreenTwo = 1;
	public int GreenThree = 1;
	public int GreenFour = 1;
	public int GreenFive = 1;
	public int GreenSix = 1;
	public int GreenSeven = 1;
	public int GreenEight = 1;
	public int GreenNine = 1;
	public int GreenReverse = 1;
	public int GreenSkip = 1;
	public int GreenDrowTwo = 1;

	public int YellowZero = 1;
	public int YellowOne = 1;
	public int YellowTwo = 1;
	public int YellowThree = 1;
	public int YellowFour = 1;
	public int YellowFive = 1;
	public int YellowSix = 1;
	public int YellowSeven = 1;
	public int YellowEight = 1;
	public int YellowNine = 1;
	public int YellowReverse = 1;
	public int YellowSkip = 1;
	public int YellowDrowTwo = 1;

	public int DrowFour = 1;
	public int Wild = 1;

	void Start() {
		//Set();
	}

	void Update() {
		#if DEBUG
		if (Input.GetKeyDown (KeyCode.R)) {
			Set ();
		}
		#endif
	}

	// Use this for initialization
	public void Set () {

		UnoStruct.tCard card;

		card.m_Color = UnoStruct.eColor.COLOR_RED;

		card.m_Number = UnoStruct.eNumber.NUMBER_ZERO;
		UnoCreateManager.Instance.SetDict(card, RedZero, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_ONE;
		UnoCreateManager.Instance.SetDict(card, RedOne, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_TWO;
		UnoCreateManager.Instance.SetDict(card, RedTwo, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_THREE;
		UnoCreateManager.Instance.SetDict(card, RedThree, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_FOUR;
		UnoCreateManager.Instance.SetDict(card, RedFour, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_FIVE;
		UnoCreateManager.Instance.SetDict(card, RedFive, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_SIX;
		UnoCreateManager.Instance.SetDict(card, RedSix, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_SEVEN;
		UnoCreateManager.Instance.SetDict(card, RedSeven, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_EIGHT;
		UnoCreateManager.Instance.SetDict(card, RedEight, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_NINE;
		UnoCreateManager.Instance.SetDict(card, RedNine, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_SKIP;
		UnoCreateManager.Instance.SetDict(card, RedSkip, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_REVERSE;
		UnoCreateManager.Instance.SetDict(card, RedReverse, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_DROWTWO;
		UnoCreateManager.Instance.SetDict(card, RedDrowTwo, true, false);


		card.m_Color = UnoStruct.eColor.COLOR_BLUE;

		card.m_Number = UnoStruct.eNumber.NUMBER_ZERO;
		UnoCreateManager.Instance.SetDict(card, BlueZero, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_ONE;
		UnoCreateManager.Instance.SetDict(card, BlueOne, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_TWO;
		UnoCreateManager.Instance.SetDict(card, BlueTwo, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_THREE;
		UnoCreateManager.Instance.SetDict(card, BlueThree, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_FOUR;
		UnoCreateManager.Instance.SetDict(card, BlueFour, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_FIVE;
		UnoCreateManager.Instance.SetDict(card, BlueFive, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_SIX;
		UnoCreateManager.Instance.SetDict(card, BlueSix, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_SEVEN;
		UnoCreateManager.Instance.SetDict(card, BlueSeven, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_EIGHT;
		UnoCreateManager.Instance.SetDict(card, BlueEight, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_NINE;
		UnoCreateManager.Instance.SetDict(card, BlueNine, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_SKIP;
		UnoCreateManager.Instance.SetDict(card, BlueSkip, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_REVERSE;
		UnoCreateManager.Instance.SetDict(card, BlueReverse, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_DROWTWO;
		UnoCreateManager.Instance.SetDict(card, BlueDrowTwo, true, false);


		card.m_Color = UnoStruct.eColor.COLOR_GREEN;

		card.m_Number = UnoStruct.eNumber.NUMBER_ZERO;
		UnoCreateManager.Instance.SetDict(card, GreenZero, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_ONE;
		UnoCreateManager.Instance.SetDict(card, GreenOne, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_TWO;
		UnoCreateManager.Instance.SetDict(card, GreenTwo, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_THREE;
		UnoCreateManager.Instance.SetDict(card, GreenThree, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_FOUR;
		UnoCreateManager.Instance.SetDict(card, GreenFour, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_FIVE;
		UnoCreateManager.Instance.SetDict(card, GreenFive, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_SIX;
		UnoCreateManager.Instance.SetDict(card, GreenSix, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_SEVEN;
		UnoCreateManager.Instance.SetDict(card, GreenSeven, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_EIGHT;
		UnoCreateManager.Instance.SetDict(card, GreenEight, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_NINE;
		UnoCreateManager.Instance.SetDict(card, GreenNine, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_SKIP;
		UnoCreateManager.Instance.SetDict(card, GreenSkip, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_REVERSE;
		UnoCreateManager.Instance.SetDict(card, GreenReverse, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_DROWTWO;
		UnoCreateManager.Instance.SetDict(card, GreenDrowTwo, true, false);


		card.m_Color = UnoStruct.eColor.COLOR_YELLOW;

		card.m_Number = UnoStruct.eNumber.NUMBER_ZERO;
		UnoCreateManager.Instance.SetDict(card, YellowZero, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_ONE;
		UnoCreateManager.Instance.SetDict(card, YellowOne, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_TWO;
		UnoCreateManager.Instance.SetDict(card, YellowTwo, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_THREE;
		UnoCreateManager.Instance.SetDict(card, YellowThree, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_FOUR;
		UnoCreateManager.Instance.SetDict(card, YellowFour, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_FIVE;
		UnoCreateManager.Instance.SetDict(card, YellowFive, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_SIX;
		UnoCreateManager.Instance.SetDict(card, YellowSix, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_SEVEN;
		UnoCreateManager.Instance.SetDict(card, YellowSeven, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_EIGHT;
		UnoCreateManager.Instance.SetDict(card, YellowEight, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_NINE;
		UnoCreateManager.Instance.SetDict(card, YellowNine, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_SKIP;
		UnoCreateManager.Instance.SetDict(card, YellowSkip, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_REVERSE;
		UnoCreateManager.Instance.SetDict(card, YellowReverse, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_DROWTWO;
		UnoCreateManager.Instance.SetDict(card, YellowDrowTwo, true, false);


		card.m_Color = UnoStruct.eColor.COLOR_WILD;

		card.m_Number = UnoStruct.eNumber.NUMBER_DROWFOUR;
		UnoCreateManager.Instance.SetDict(card, DrowFour, true, false);
		card.m_Number = UnoStruct.eNumber.NUMBER_WILD;
		UnoCreateManager.Instance.SetDict(card, Wild, true, false);
	}
}
