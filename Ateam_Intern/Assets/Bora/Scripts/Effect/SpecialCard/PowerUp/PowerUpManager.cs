using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour {

	static PowerUpManager instance;

	public static PowerUpManager Instance {
		get {
			if (instance == null) {
				instance = (PowerUpManager)FindObjectOfType(typeof(PowerUpManager));

				if (instance == null) {
					Debug.LogError("PowerUpManager Instance Error");
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
