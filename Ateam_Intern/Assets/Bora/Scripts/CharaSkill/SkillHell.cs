using UnityEngine;
using System.Collections;

public class SkillHell : CharaSkillBase {

	[Range(0.0f,1.0f)] public float fPercentage;

	void Start() {
		SkillType = eSkillType.SKILL_HEEL;
	}

	// 回復処理 
	public override void Run() {
		Player player = GameMainUpperManager.instance.player;

		if (player.isDead)
			return;

		if (player.isDead || player.hpRemain <= 0)
			return;
		
		int nhp = player.hpMax;
		player.hpRemain += (int)(nhp * fPercentage);
		HeelEffect.Run (); 
	}
}
