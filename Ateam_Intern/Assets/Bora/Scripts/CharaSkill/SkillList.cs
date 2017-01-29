using UnityEngine;
using System.Collections;

public class SkillList : MonoBehaviour {

	static SkillList instance;

	public static SkillList Instance {
		get {
			if (instance == null) {
				instance = (SkillList)FindObjectOfType(typeof(SkillList));

				if (instance == null) {
					Debug.LogError("SkillList Instance Error");
				}
			}
			return instance;
		}
	}

	void Awake() {
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}
	}
}
