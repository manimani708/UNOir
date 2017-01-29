using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FeverText_Debug : MonoBehaviour {

	Text text = null;
	public string FeverText = "Fever!!";

	public GameObject FeverGauge = null;
	FeverGauge feverGauge = null;

	// Use this for initialization
	void Start () {
	
		text = GetComponent<Text>();
		feverGauge = FeverGauge.transform.GetComponent<FeverGauge> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (feverGauge.isFeverMode) {
			text.text = FeverText;
		} else {
			text.text = "";
		}
	}
}
