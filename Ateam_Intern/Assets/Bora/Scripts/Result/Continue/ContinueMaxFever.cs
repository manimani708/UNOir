using UnityEngine;
using System.Collections;

public class ContinueMaxFever : MonoBehaviour {

	FeverGauge gauge = null;

	void Start () {

		// ゲージをMAXに
		GameObject fever = GameObject.FindWithTag ("FeverGauge");	
		gauge = fever.GetComponentInChildren<FeverGauge> ();
	}

	void Update() {

		if (!BattleManager.Instance.GetIsInBattle ())
			return;

		gauge.SetPoint (gauge.feverPointMax);
		Destroy (this.GetComponent<ContinueMaxFever>());
	}
}
