using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillDelay : CharaSkillBase {

	[Range(0.0f,1.0f)] public float fSpeed;
	public float fTime = 2.0f;
	float fNowTime = 0.0f;
	public bool bRun { get; private set; } 
	List<Enemy> enemyList = new List<Enemy>();

	// Use this for initialization
	void Start () {
		
		SkillType = eSkillType.SKILL_DELAY;
	}
	
	// Update is called once per frame
	void Update () {

		if (!bRun) 
			return;

		CheckDeathEnemy ();

		if (!BattleManager.Instance.GetIsInBattle ())
			return;

		fNowTime += Time.deltaTime;
		for (int i = 0; i < enemyList.Count; i++) {
			enemyList[i].SetGaugeSpeed(fSpeed);
		}

		if (fNowTime >= fTime) {
			bRun = false;
			for (int i = 0; i < enemyList.Count; i++) {
				enemyList[i].SetGaugeSpeed(1.0f);
			}
		}
	}

	public override void Run() {
		bRun = true;
		fNowTime = 0.0f;
		enemyList = GameMainUpperManager.instance.enemyList;
	}

	void CheckDeathEnemy() {

		List<Enemy> temp = GameMainUpperManager.instance.enemyList;

		if (temp.Count <= 0 || temp [0] != enemyList [0]) {
			bRun = false;
			for (int i = 0; i < enemyList.Count; i++) {
				enemyList[i].SetGaugeSpeed(1.0f);
			}
		}
	}
}
