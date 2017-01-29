using UnityEngine;
using System.Collections;

public class DrowEffect_Debug : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			bool bUse = DrowEffectManager.Instance.GetUse (UnoStruct.eColor.COLOR_RED);
			DrowEffectManager.Instance.Run (UnoStruct.eColor.COLOR_RED, !bUse);
		} 
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			bool bUse = DrowEffectManager.Instance.GetUse (UnoStruct.eColor.COLOR_BLUE);
			DrowEffectManager.Instance.Run (UnoStruct.eColor.COLOR_BLUE, !bUse);
		} 
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			bool bUse = DrowEffectManager.Instance.GetUse (UnoStruct.eColor.COLOR_YELLOW);
			DrowEffectManager.Instance.Run (UnoStruct.eColor.COLOR_YELLOW, !bUse);
		} 
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			bool bUse = DrowEffectManager.Instance.GetUse (UnoStruct.eColor.COLOR_GREEN);
			DrowEffectManager.Instance.Run (UnoStruct.eColor.COLOR_GREEN, !bUse);
		} 
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			FeverEffectManager.Instance.Run (true);
		} 
		if (Input.GetKeyDown (KeyCode.Alpha6)) {
			FeverEffectManager.Instance.Run (false);
		} 
	}
}
