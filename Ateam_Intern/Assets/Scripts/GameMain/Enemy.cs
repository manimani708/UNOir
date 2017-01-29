 using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Charactor
{
    [SerializeField]
    private int m_HpMax;
    public int m_hpMax{ get { return m_HpMax;} set { m_HpMax = value; } }

    [SerializeField]
    private int m_Attack;
    public int m_attack{ get { return m_Attack;}set { m_Attack = value; } }

    [SerializeField]
    private Attribute m_Attribute;
    public Attribute m_attribute{ get { return m_Attribute;} set { m_Attribute = value; } }


    [SerializeField]
    private float m_AttackInterval = 3f;                     //攻撃間隔
    public float m_attackInterval{ get { return m_AttackInterval;} set { m_AttackInterval = value; } }

    [SerializeField]
    private  GameObject m_AttackPrefab = null;
    public GameObject m_attackPrefab{ get { return m_AttackPrefab;} set { m_AttackPrefab = value; } }

    [SerializeField]
    private GameObject m_SpecitalAttackPrefab = null;
    public GameObject m_specialAttackPrefab{ get { return m_SpecitalAttackPrefab;}set { m_SpecitalAttackPrefab = value; } }

    [SerializeField]
    private bool m_IsBoss;
    public bool m_isBoss { get { return m_IsBoss;} set { m_IsBoss = value; } }

    [SerializeField,Range(0,100)]
    private int m_SpecialAttackProbability;
    public int m_specialAttackProbability{get{return m_SpecialAttackProbability; } set { m_SpecialAttackProbability = value; } }

    [SerializeField, Range(0, 1)]
    private float m_SpecialAttackDamagePercentage;
    public float m_specialAttackDamagePercentage{ get { return m_SpecialAttackDamagePercentage;} set { m_SpecialAttackDamagePercentage = value; } }

    public int hpRemain { get; set; }                 //残りHP
    public float gaugeSpeed { get; private set;}      //ゲージ上昇スピード
    public bool isDead { get; private set;}
    public Vector2 pivot { get; private set;}

    private GameMainUpperManager upperManager;
    private HpGauge hpGauge;
    private TimeGauge timeGauge;
    private Damage damage;
    private SpriteRenderer spriteRenderer;
    private Dissolver dissolver;
    private int beforeHpRemain;                   //前フレームの残りHP
    private float timer = 0f;                       //攻撃間の時間を測る
    private bool isDiedTrigger = false;
    private bool isAttacking = false;
    private GameObject attackEffect = null;
    private GameObject mask = null;

	// 変更点
	public GameObject DeathObjPrefab = null;
    public EnemyDeath enemyDeath { get; private set;}
	public void CreateDeathObj() {
	GameObject go =	(GameObject)Instantiate (DeathObjPrefab, transform.position, transform.rotation);
        enemyDeath = go.GetComponent<EnemyDeath>();
	}
	// ------

    void Awake()
    {
        upperManager = GameMainUpperManager.instance;
        Init();
    }

    void OnDestroy()
    {
        upperManager.enemyList.IndexOf(this);
        upperManager.enemyList.Remove(this);
        if(attackEffect != null)
        {
            Destroy(attackEffect);
        }

        upperManager.player.ResetTargetEnemy();

    }

    public void Init()
    {
        hpMax = m_hpMax;   //最大HPはどっかのデータクラスから取ってくる
        hpRemain = hpMax;
        beforeHpRemain = hpRemain;
        attack = m_attack;     //攻撃値もどっかからとってくる
        attribute = m_attribute;
        gaugeSpeed = 1f;
        hpGauge = transform.GetComponentInChildren<HpGauge>();
        timeGauge = transform.FindChild("AttackGauge").GetComponentInChildren<TimeGauge>();
        damage = transform.GetComponentInChildren<Damage>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        dissolver = GetComponent<Dissolver>();
        isDead = false;
        upperManager.enemyList.Add(this);
        pivot = transform.FindChild("Pivot").transform.position;
        mask = transform.FindChild("Mask").gameObject;
    }

    public void ResetStatus()
    {
        hpRemain = hpMax;
        beforeHpRemain = hpRemain;
        hpGauge.SetGaugeImmidiate(1f);
        gaugeSpeed = 1f;
        isDead = false;
        timer = 0f;
        timeGauge.Scale(0);
        isDiedTrigger = false;
        isAttacking = false;
        SetMaskSprite(false);
    }

    void Update()
    {
            //HPが変化したらゲージ推移アニメーション
            if (hpRemain != beforeHpRemain)
            {
                if (hpRemain >= hpMax)
                {
                    hpRemain = hpMax;
                }
            hpGauge.ShowAnimation(1f * hpRemain / hpMax, this);
            }

            beforeHpRemain = hpRemain;

        if(!hpGauge.isAnimating)
        {
            hpGauge.JudgeIsIsInBattle(1f * hpGauge.transform.localScale.x, this);
        }


        //戦闘中じゃなかったら以下の行動はしない
        if (BattleManager.Instance.GetIsInBattle() == false) return;



        if(!isAttacking)
        {
            timer += Time.deltaTime * gaugeSpeed;
        }


        //デバッグコマンド
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            SetGaugeSpeed(0.5f);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetGaugeSpeed(1f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SetGaugeSpeed(2f);
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            FullChargeTimeGauge();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            FlashAnimation();
        }
        */


        //HPが0以下なら攻撃しない
        if (!isDead)
        {
            if (!isAttacking && timer >= m_attackInterval)
            {
                AttackPlayer();
            }

            float timePercentage = timer / m_attackInterval;

            if (timePercentage >= 0.75f && !Caution.Instance.isFlashing)
            {
                Caution.Instance.FlashAnimation();
            }

            timeGauge.Scale(timePercentage);
        }

    }
	

    private void AttackPlayer()
    {
        StartCoroutine(AttackCoroutine());
    }


    private IEnumerator AttackCoroutine(bool isSpecialAttack =false)
    {
        isAttacking = true;

        Player player = upperManager.player;
        bool isReverse = player.isReverse;
        if(!isSpecialAttack)
        {
            isSpecialAttack = GetIsSpecialAttack();
        }

        int attackPoint;

        //攻撃エフェクト

        if(m_isBoss)
        {
            if(isSpecialAttack)
            {
                attackEffect = Instantiate(m_specialAttackPrefab);
            }
            else
            {
                attackEffect = Instantiate(m_attackPrefab);   
            }
            attackEffect.GetComponent<EffectBase>().Set(isReverse);
            attackPoint = GetAttackPoint(player, isSpecialAttack);
        }
        else
        {
            attackEffect = (GameObject)Instantiate(m_attackPrefab, transform.position, Quaternion.identity);
            attackEffect.GetComponent<EffectBase>().Set(upperManager.feverGauge.transform.position, isReverse);
            attackPoint = GetAttackPoint(player, false);
        }

        if (attackEffect == null) yield break;

        yield return new WaitWhile(() => attackEffect.GetComponent<EffectBase>().bAtack == false);




        if (isReverse)
        {
            //リバース状態なら攻撃を跳ね返されて自分がダメージを受ける
            attackPoint = Mathf.FloorToInt(attackPoint * BattleDataBase.reverseMagnification);
            Damaged(attackPoint,this.attribute);
            player.DecrementReverseNum();
        }
        else
        {
            //プレイヤーのHPを減らす
            player.Damaged(attackPoint);
           //player.isDamaging = true;
        }

        ResetTimer();
        Caution.Instance.isFlashing = false;

        yield return new WaitWhile(() => attackEffect.GetComponent<EffectBase>().bEnd == false);
        Destroy(attackEffect);

        //player.isDamaging = false;

        isAttacking = false;
    }


    private int GetAttackPoint(Player player,bool isSpecialAttack)
    {
        int attackPoint;

        if(isSpecialAttack)
        {
            attackPoint = Mathf.FloorToInt(player.hpMax * m_specialAttackDamagePercentage);
        }
        else
        {
            attackPoint = attack;
        }

        return attackPoint;
    }


    private bool GetIsSpecialAttack()
    {
        int n = Random.Range(0, 100);

        if (n < m_specialAttackProbability) {return true; }

        return false;
    }


    public void ShowDamage(int damagePoint,Attribute attribute)
    {
        if (isDead) return;

        damage.ShowDamage(damagePoint,attribute);
		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_ENEMYDAMAGE);
    }

    //攻撃インターバルのためのタイマーをリセット
    public void ResetTimer()
    {
        timer = 0f;

    }

    public void GaugeVibration()
    {
        if (isDead) return;
        if (timeGauge.isVibrating) return;

        timeGauge.Vibration();
    }

    public void SetGaugeSpeed(float speed)
    {
        gaugeSpeed = speed;
    }

    public bool SetIsDiedTrigger()
    {
        return isDiedTrigger = true;
    }

    public bool GetIsDiedTrigger()
    {
        bool trigger = isDiedTrigger;
        isDiedTrigger = false;

        return trigger;
    }

    public void FullChargeTimeGauge()
    {
        if (isAttacking) return;

        timer = m_attackInterval;
    }

    public void SetMaskSprite(bool b)
    {
        mask.SetActive(b);
    }


    public void SetSpriteIsVisible(bool b)
    {
        spriteRenderer.enabled = b;
        SetMaskSprite(b);
    }


    //点滅アニメーション
    public void FlashAnimation()
    {
        StartCoroutine(FlashCoroutine());
    }

    IEnumerator FlashCoroutine()
    {
        float interval = FlashConfig.flashInterval;

        for (int i = 0; i < FlashConfig.flashCount; i++)
        {
            dissolver.ResetCutOff();
            yield return new WaitForSeconds(interval);

            dissolver.Skip();
            yield return new WaitForSeconds(interval);
        }


        yield return null;
    }


    //ダメージを食らう
    public void Damaged(int damage, Attribute attribute)
    {
        if (BattleManager.Instance.GetIsInBattle() == false) return;
        if (isDead) return;

        //点滅
        FlashAnimation();
        //ダメージ数字の描画
        ShowDamage(damage, attribute);

        hpRemain -= damage;

        if (hpRemain <= 0)
        {
            hpRemain = 0;
            isDead = true;
        }
    }


    public void SpecialAttack()
    {
        if (!m_isBoss) return;

        StartCoroutine(AttackCoroutine(true));
    }

}


