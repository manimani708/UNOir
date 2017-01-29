using UnityEngine;
using System.Collections;

public class UnoStruct : MonoBehaviour {

	public enum eColor {
		COLOR_RED = 0,
		COLOR_BLUE,
		COLOR_YELLOW,
		COLOR_GREEN,
		COLOR_WILD,

		COLOR_MAX,
	};

	public enum eNumber {
		NUMBER_ZERO = 0,
		NUMBER_ONE,
		NUMBER_TWO,
		NUMBER_THREE,
		NUMBER_FOUR,
		NUMBER_FIVE,
		NUMBER_SIX,
		NUMBER_SEVEN,
		NUMBER_EIGHT,
		NUMBER_NINE,
		NUMBER_REVERSE,
		NUMBER_SKIP,
		NUMBER_DROWTWO,
		NUMBER_DROWFOUR,
		NUMBER_WILD,

		NUMBER_MAX,
	};

	public struct tCard {
		public eColor m_Color;
		public eNumber m_Number;
	};
}
