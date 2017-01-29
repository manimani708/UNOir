using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrowEffectManager : MonoBehaviour {

	static DrowEffectManager instance;

	public static DrowEffectManager Instance {
		get {
			if (instance == null) {
				instance = (DrowEffectManager)FindObjectOfType(typeof(DrowEffectManager));

				if (instance == null) {
					Debug.LogError("DrowEffectManager Instance Error");
				}
			}
			return instance;
		}
	}
		
	Dictionary<UnoStruct.eColor, bool> DictColor = new Dictionary<UnoStruct.eColor, bool>();

	void Awake() {

		if (this != Instance) {
			Destroy(this.gameObject);
			return;
		}

		for (int i = 0; i < (int)UnoStruct.eColor.COLOR_MAX; i++) {
			DictColor.Add ((UnoStruct.eColor)i, false);
		}
	}

	public bool GetUse(UnoStruct.eColor color) {
		return DictColor [color];
	}

	public void Run(UnoStruct.eColor color,  bool bUse) {
		DictColor [color] = bUse;
	}
}
