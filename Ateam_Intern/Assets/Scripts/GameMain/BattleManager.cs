using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour {

    static BattleManager instance;

    public static BattleManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (BattleManager)FindObjectOfType(typeof(BattleManager));
                if (instance == null)
                {
                    Debug.LogError("BattleManager Instance Error");
                }
            }
            return instance;
        }
    }

    [SerializeField]
    private List<GameObject> enemiesList = null;

    [SerializeField]
    private GameObject readyGoPrefab = null;

    [SerializeField]
    private GameObject enemyBackPrefab = null;

    [SerializeField]
    private GameObject bossStartPrefab = null;

    public int nowBattleCount { get; private set;}

    private GameMainUpperManager upperManager;
    private bool isInBattle = false;
    private bool isSequenceOneStep = false;
    private int battleCount;
    private bool isOneStepGoing = false;
    private int phaseCount = -1;
    private bool beforeIsInBattle;


    void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        beforeIsInBattle = isInBattle;
    }


    void Start () 
    {
        upperManager = GameMainUpperManager.instance;
        battleCount = enemiesList.Count;
    }
	

    void Update () 
    {
        if(!isInBattle && !isOneStepGoing)
        {
            if((phaseCount < battleCount + 1) || Input.GetKeyDown(KeyCode.G))
            GoSequenceOneStep();
        }



        //非戦闘時
        if(!isInBattle)
        {
            ////プレイヤーが生きていたら戦闘終了時に敵を消す
            //if ((isInBattle != beforeIsInBattle) && !upperManager.player.isDead)
            //{
            //    if (phaseCount > 0)
            //    {
            //        StartCoroutine(EnemyDieSequence());
            //    }
            //}


            //戦闘のフローを進める
            if (JudgeSequeuceOneStep() == true)
            {
                phaseCount++;

                if (phaseCount == 0)
                {
                    StartCoroutine(StageStartSequence());
                }
                else if (phaseCount <= battleCount)
                {
                    if(!upperManager.player.isDead)
                    {
                        //プレイヤーが生きていたら次のステージへ
                        nowBattleCount = phaseCount;
                        StartCoroutine(BattleStartSequence());
                    }
                    else
                    {
                        StartCoroutine(StageFaildCoroutine());
                    }
                }
                else if (phaseCount == battleCount + 1)
                {
                    if (!upperManager.player.isDead)
                    {
                        StartCoroutine(StageClearCoroutine());
                    }
                    else
                    {
                       StartCoroutine(StageFaildCoroutine());
                    }
                }

            }
        }


        beforeIsInBattle = isInBattle;
	}

    IEnumerator StageStartSequence()
    {
        //フェードイン
        //Debug.Log("フェードイン");
        yield return new WaitForSeconds(0f);

        //雲が左右に分かれる
        //Debug.Log("雲が左右に分かれる");
        yield return new WaitForSeconds(0f);

        isOneStepGoing = false;
    }



    IEnumerator BattleStartSequence()
    {
        if (nowBattleCount > 1) 
        {
             yield return StartCoroutine(EnemyDieSequence()); 
        }

        //STAGE n のアニメーション
        {
            if(nowBattleCount < battleCount)
            {
                //ザコ戦
                GameObject enemyBack = Instantiate(enemyBackPrefab);

                //AddComponentされるまで待機
                yield return new WaitWhile(() => enemyBack.GetComponent<Enemy_AllFadeOut>() == null);

                //フェードアニメーションが終わったら次へ
                yield return new WaitWhile(() => enemyBack.GetComponent<Enemy_AllFadeOut>().bEnd == false);

                Destroy(enemyBack);
            }
            else
            {
                //ボス戦
                GameObject bossStart = Instantiate(bossStartPrefab);

                yield return new WaitWhile(() => bossStart.transform.FindChild("Boss_Appear").GetComponent<Boss_Appear>().bEnd == false);

                Destroy(bossStart);
            }
        }


        //ディゾルブイン
        {
            GameObject enemies = enemiesList[nowBattleCount - 1];
            GameObject enemyManager = Instantiate(enemies);//現在のバトルカウントの敵を生成するようにする
            Transform upper = GameObject.Find("Upper").transform;
            enemyManager.transform.SetParent(upper);
            yield return new WaitWhile(() => enemyManager.GetComponent<EnemyManager>().End == false);
        }

                
        //READY GO!
        {
            GameObject readyGo = Instantiate(readyGoPrefab);
            yield return new WaitWhile(() => readyGo.transform.FindChild("Go").GetComponent<Go>().bEnd == false);

            Destroy(readyGo);
        }


        //戦闘中にする
        SetIsInBattle(true);

        isOneStepGoing = false;
    }


    //敵消滅コルーチン
    private IEnumerator EnemyDieSequence()
    {
        upperManager.player.ResetTargetEnemy();
        foreach (Enemy elem in upperManager.enemyList)
        {
            //敵スプライトを非表示にする
            elem.SetSpriteIsVisible(false);
            // ディゾルブアウトオブジェクトの生成
            elem.CreateDeathObj();

        }


        yield return new WaitWhile(() => upperManager.enemyList[0].enemyDeath.End == false);

        foreach (Enemy elem in upperManager.enemyList)
        {
            Destroy(elem.gameObject);
        }

    }


    //ステージクリア処理
    private IEnumerator StageClearCoroutine()
    {
        yield return StartCoroutine(EnemyDieSequence());

        upperManager.feverGauge.SetIsFever(false);

        //勝利リザルトの表示
        ResultManager.Instance.SetResult(true);
    }


    private IEnumerator StageFaildCoroutine()
    {
        upperManager.feverGauge.SetIsFever(false);

        //敗北リザルトの表示
        ResultManager.Instance.SetResult(false);

        //リザルト処理が終わるまで待機
        yield return new WaitWhile(() => ResultManager.Instance.bResult == true);

        yield return StartCoroutine(ContinueThisBattle());
    }


    private IEnumerator ContinueThisBattle()
    {
        //死んでいたらゲージなどをリセットして同じステージをコンティニュー
        TurnData.Instance.AllReset();
        upperManager.player.ResetStatus();
        upperManager.feverGauge.Init();
        //foreach (Enemy elem in upperManager.enemyList)
        //{
        //   elem.ResetStatus();
        //}

        //READY GO!
        {
            GameObject readyGo = Instantiate(readyGoPrefab);
            yield return new WaitWhile(() => readyGo.transform.FindChild("Go").GetComponent<Go>().bEnd == false);

            Destroy(readyGo);
        }

        //戦闘中にする
        SetIsInBattle(true);

        //フェーズを一つ戻す
        phaseCount--;

        isOneStepGoing = false;
    }

    private bool JudgeSequeuceOneStep()
    {
        if(isSequenceOneStep)
        {
            isSequenceOneStep = false;
            return true;
        }

        return false;
    }

    public void GoSequenceOneStep()
    {
        isSequenceOneStep = true;
        isOneStepGoing = true;
    }

    public void SetIsInBattle(bool b)
    {
        isInBattle = b;

        //バトル終了時にTurnDataをリセットする
        if(isInBattle)
        {
            TurnData.Instance.AllReset();
        }
    }

    public bool GetIsInBattle()
    {
        return isInBattle;
    }
}
