using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeaderSkillHeel : LeaderSkillBase {

	[Range(0.0f,1.0f)] public float fPercentage;

	bool bOldBattle = false;

	// Use this for initialization
	void Start () {

		bOldBattle = BattleManager.Instance.GetIsInBattle ();
	}
	
	// Update is called once per frame
	void Update () {

		if(bOldBattle && !BattleManager.Instance.GetIsInBattle()) {
			Player player = GameMainUpperManager.instance.player;
			if (player.hpRemain <= 0 || player.isDead) {
				bOldBattle = BattleManager.Instance.GetIsInBattle ();
				return;
			}
				
			Heel ();
		}
		bOldBattle = BattleManager.Instance.GetIsInBattle ();
	}

	void Heel() {
		Player player = GameMainUpperManager.instance.player;
		int nhp = player.hpMax;
		player.hpRemain += (int)(nhp * fPercentage); 
		HeelEffect.Run ();
	}
}
