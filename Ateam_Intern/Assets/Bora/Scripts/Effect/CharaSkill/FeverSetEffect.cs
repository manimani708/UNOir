using UnityEngine;
using System.Collections;

public class FeverSetEffect : MonoBehaviour {

	static FeverSetEffect instance;

	public static FeverSetEffect Instance {
		get {
			if (instance == null) {
				instance = (FeverSetEffect)FindObjectOfType(typeof(FeverSetEffect));

				if (instance == null) {
					Debug.LogError("FeverSetEffect Instance Error");
				}
			}
			return instance;
		}
	}

	[SerializeField]
	GameObject feverSetObj = null; 

	[SerializeField]
	Color[] colorList = new Color[4];

	void Awake() {
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}
	}

	public void CreateFeverSetEffect(UnoStruct.eColor color, Vector3 pos) {

		GameObject obj = (GameObject)Instantiate (feverSetObj, pos, Quaternion.identity);
		obj.GetComponent<ParticleSystem> ().startColor = colorList[(int)color];
	}
}
