using UnityEngine;
using System.Collections;

public class CharaSkillBase : MonoBehaviour {

	public enum eSkillType {
		SKILL_ATACK = 0,	//	割合ダメージ
		SKILL_HEEL,			//	回復
		SKILL_DELAY,		//	遅延	
		SKILL_RISE,			//	確率UP
	
		SKILL_MAX,
	};

	eSkillType skillType = eSkillType.SKILL_MAX;
	public UnoStruct.eColor skillColor = UnoStruct.eColor.COLOR_MAX;

	public eSkillType SkillType {
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

	public virtual void Run() {
		// オーバーライド用
	}
}
