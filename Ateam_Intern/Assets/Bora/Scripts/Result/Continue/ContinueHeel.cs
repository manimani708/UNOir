using UnityEngine;
using System.Collections;

public class ContinueHeel : MonoBehaviour {

	[Range(0.0f,1.0f)]
	public float fPercentage = 0.1f;

	float fTime = 0.0f;
	public float fInterval = 5.0f;

	Player player = null;

	// Use this for initialization
	void Start () {
		fTime = 0.0f;
		player = GameMainUpperManager.instance.player;
	}
	
	// Update is called once per frame
	void Update () {

		if (!BattleManager.Instance.GetIsInBattle ()) {
			fTime = 0.0f;
			return;
		}

		if (player.hpRemain <= 0 || player.isDead) {
			fTime = 0.0f;
			return;
		}

		fTime += Time.deltaTime;
		if (fTime >= fInterval) { 
			fTime = 0.0f;
			int nhp = player.hpMax;
			player.hpRemain += (int)(nhp * fPercentage);
			HeelEffect.Run ();
		}
	}
}
