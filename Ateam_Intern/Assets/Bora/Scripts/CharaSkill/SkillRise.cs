using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillRise : CharaSkillBase {

    static public int nNum = 0;
	
    [SerializeField]
    int nMaxNum = 5;

    public int GetMaxNum { get { return nMaxNum * nCnt; } private set { nMaxNum = value; } }
	//public int nAdd = 2; // 倍率
	static public bool bRun = false;
	//public int nRunCnt { get; private set; }
	//List<int> Temp = new List<int>(); // 現在確率をとっておく。
    static int nCnt = 0;

	static public int nCardNum = 0; 

	void Start () {

		nNum = 0;
		//nRunCnt = 0;
		SkillType = eSkillType.SKILL_RISE;
	}
	
	void LateUpdate () {
		/*if (!bRun)
			return;
		
		nNum -= nCardNum;

		// 終了
		if (nNum <= 0) {
			bRun = false;
            nCnt = 0;
            nNum = 0;
            //Debug.Log("riseEnd");
		}*/

        /*if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("残り : " + nNum + " Max : " + GetMaxNum);
        }*/
	}

	public override void Run() {

        nNum += nMaxNum;
		bRun = true;
        nCnt++;
        //Debug.Log("riseRun");
	}

    static public void SetNum(int i)
    {
        nNum -= i;

        // 終了
        if (nNum <= 0)
        {
            bRun = false;
            nCnt = 0;
            nNum = 0;
            //Debug.Log("riseEnd");
        }
    }
}
