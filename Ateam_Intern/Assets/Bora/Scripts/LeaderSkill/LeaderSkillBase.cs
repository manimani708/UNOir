using UnityEngine;
using System.Collections;

public class LeaderSkillBase : MonoBehaviour {

	public enum eLeaderSkillType {
		SKILL_ATACK = 0,	//	攻撃UP
		SKILL_HEEL,			//	回復
		SKILL_RISE,			//	確率UP
		SKILL_EXPUP,		//	経験値UP

		SKILL_MAX,
	};

	eLeaderSkillType skillType = eLeaderSkillType.SKILL_MAX;
	public UnoStruct.eColor skillColor = UnoStruct.eColor.COLOR_MAX;

	public eLeaderSkillType SkillType {
		get { return skillType; }
		set { skillType = value; }
	}

	public UnoStruct.eColor SkillColor {
		get { return skillColor; }
		set { skillColor = value; }
	}

	public virtual void SetEnemy() {
		// オーバーライド用
	}
}
