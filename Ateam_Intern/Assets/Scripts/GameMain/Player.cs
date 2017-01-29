using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField]
    public GameObject lockOnSitePrefab = null;

    public int hpMax { get; private set; }         //最大HP
    public int hpRemain { get; set; }              //残りHP
    public bool isDead { get; private set;}        //死んでいるかどうか
    public int reverseNum { get; private set; }    //リバースがいくつ溜まっているか
    public bool isReverse { get; private set;}     //リバース状態かどうか
    public Enemy lockOnTarget { get; private set;}  //ターゲットとなる敵（指定していなかったらnull）

    private GameMainUpperManager upperManager;
    private HpGauge hpGauge;
    private HPNumber hpNumber;
    private int beforeHpRemain;                   //前フレームの残りHP
    private bool isDiedTrigger = false;
    private GameObject lockOnSite = null;


    public void Init(int hpSum)
    {
        hpMax = hpSum;
        hpRemain = hpMax;
        beforeHpRemain = hpRemain;
        hpGauge = transform.GetComponentInChildren<HpGauge>();
        hpNumber = transform.FindChild("HPGauge").GetComponentInChildren<HPNumber>();
        isDead = false;
        reverseNum = 0;
        isReverse = false;
        lockOnTarget = null;
    }

    public void ResetStatus()
    {
        hpRemain = hpMax;
        isDead = false;
        reverseNum = 0;
        isReverse = false;
        lockOnTarget = null;
        isDiedTrigger = false;
        ResetTargetEnemy();
    }

    void Start()
    {
        upperManager = GameMainUpperManager.instance;
    }

    void Update()
    {
        //HPが変化したらゲージ推移アニメーション
        if (hpRemain != beforeHpRemain)
        {
            if(hpRemain >= hpMax)
            {
                hpRemain = hpMax;
            }
            hpGauge.ShowAnimation(1f * hpRemain / hpMax, this);
            hpNumber.ShowAnimation(hpRemain, hpGauge.takeScalingTime);
        }

        /*if(BattleManager.Instance.GetIsInBattle())
        {
            if(Input.GetKeyDown(KeyCode.Y))
            {
                ChangeTargetEnemy(upperManager.enemyList[0]);
            }
            if(Input.GetKeyDown(KeyCode.U))
            {
                if(upperManager.enemyList.Count>=2)
                {
                    ChangeTargetEnemy(upperManager.enemyList[1]);
                }
            }
        }*/


        beforeHpRemain = hpRemain;
    }

    public void CardAction()
    {
        
        float fNumber;              //カードの数字×ドロー2、ドロー4の倍率
        int nNum;                   //各色に対応する場に置いたカードの枚数

        TurnData.tTurnData turnData = TurnData.Instance.GetTurnData();

        //スキップ処理
		if(turnData.nSkipNum > 0)
        {
            SkipEnemy();
        }

        //リバース処理
		int nReverseNum = turnData.nReverseNum;
		if(nReverseNum > 0)
        {
            ChargeReverse(nReverseNum);
        }

        if (upperManager == null) return;

        foreach (PlayerCharactor elem in upperManager.charactorList)
        {

           switch (elem.attribute)
            {
                case Charactor.Attribute.Solar:
                    fNumber = turnData.Red.fNumber;
                    nNum = turnData.Red.nNum;
                    break;
                case Charactor.Attribute.Thander:
                    fNumber = turnData.Yellow.fNumber;
                    nNum = turnData.Yellow.nNum;
                    break;
                case Charactor.Attribute.Water:
                    fNumber = turnData.Blue.fNumber;
                    nNum = turnData.Blue.nNum;
                    break;
                case Charactor.Attribute.Wind:
                    fNumber = turnData.Green.fNumber;
                    nNum = turnData.Green.nNum;
                    break;
                default:
                    fNumber = 0f;
                    nNum = 0;
                    break;
            }

            //攻撃値を計算、0より大きかったら攻撃
            int attackPoint = elem.GetAttackPoint(fNumber);
            if(attackPoint > 0)
            {
                bool isStrongAttack = nNum >= BattleDataBase.strongAttackBorder ? true : false;
                elem.AttackEnemy(attackPoint, isStrongAttack, lockOnTarget);
            }
        }

    }


    public void ChangeTargetEnemy(Enemy target)
    {
        if (target.isDead) return;

        //選択中の敵をタッチしたら選択解除
        if(lockOnTarget != null)
        {
            int id = lockOnTarget.GetInstanceID();

            ResetTargetEnemy();

            if(target.GetInstanceID() == id)
            {
                return;
            }

        }


        lockOnTarget = target;
        //選択アニメーションをここにいれる
        lockOnSite = (GameObject)Instantiate(lockOnSitePrefab, target.pivot, Quaternion.identity);

    }

    public void ResetTargetEnemy()
    {
        if (!lockOnTarget) return;

        lockOnTarget = null;
        Destroy(lockOnSite);
    }

    //敵のタイマーをリセットする
    private void SkipEnemy()
    {
        List<Enemy> enemyList = upperManager.enemyList;
        foreach (Enemy elem in enemyList)
        {
            elem.ResetTimer();
            elem.GaugeVibration();
        }
    }


    //リバースを溜める
	private void ChargeReverse(int n)
    {
        reverseNum += n;
        isReverse = true;
    }

    public void DecrementReverseNum()
    {
        reverseNum--;

        if (reverseNum < 0)
        {
            reverseNum = 0; 
        }

        if(reverseNum == 0)
        {
            isReverse = false;
        }
    }


    public bool GetIsDiedTrigger()
    {
        bool trigger = isDiedTrigger;
        isDiedTrigger = false;

        return trigger;
    }


    public void Damaged(int damage)
    {
        if (BattleManager.Instance.GetIsInBattle() == false) return;
        if (isDead) return;

        hpRemain -= damage;

        if (hpRemain <= 0)
        {
            hpRemain = 0;
            isDead = true;
            isDiedTrigger = true;
        }
    }

}



