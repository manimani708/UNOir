using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerCharactor : Charactor
{
    [SerializeField]
    public GameObject weakAttackPrefab = null;

    [SerializeField]
    public GameObject strongAttackPrefab = null;

    public UnoStruct.eNumber cardNum { get; private set; }            //キャラクターに対応するカードの数字
    public Sprite sprite { get; private set; }          //キャラクター画像
    public Sprite noFrameSprite { get; private set;}    //枠のないキャラクター画像
    public bool isLeader { get; private set;}           //リーダーかどうか



    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            GetTCard();
        }
    }

    public void Init(CharactorData data)
    {
        hpMax = data.hp;
        attack = data.attack;
        attribute = data.attribute;
		cardNum = data.cardNum;
        isLeader = data.isLeader;
		//リソースからスプライトを取得
		Sprite[] all = ResourceHolder.Instance.GetResource (ResourceHolder.eResourceId.ID_CHARAWAKU);
		//Sprite[] all = Resources.LoadAll<Sprite>("Textures/PlayerCharactor/Charawaku_all");
		for (int i = 0; i < all.Length; i++) {
			if (data.spriteName == all [i].name) {
				sprite = all [i];
			}
		}
		Sprite[] allNoFrameSprite = ResourceHolder.Instance.GetResource (ResourceHolder.eResourceId.ID_CHARASKILLCARD);
        //Sprite[] allNoFrameSprite = Resources.LoadAll<Sprite>("Textures/CharaSkill/CharaSkillCard");
        noFrameSprite = allNoFrameSprite[(int)attribute];
      

        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void InitFriend(CharactorData data)
    {
        hpMax = 0;
        attack = 0;
        attribute = data.attribute;
        cardNum = data.cardNum;
        isLeader = false;
        sprite = null;
		Sprite[] allNoFrameSprite = ResourceHolder.Instance.GetResource (ResourceHolder.eResourceId.ID_CHARASKILLCARD);
        //Sprite[] allNoFrameSprite = Resources.LoadAll<Sprite>("Textures/CharaSkill/CharaSkillCard");
        noFrameSprite = allNoFrameSprite[4];
    }


    public void AttackEnemy(int attackPoint,bool isStrongAttack, Enemy lockOnTarget = null)
    {
        AttackTarget attackTarget = GetAttackTarget(attackPoint,lockOnTarget);
        if (attackTarget == null) return;

        StartCoroutine(AttackCoroutine(attackTarget, isStrongAttack));
    }


    private IEnumerator AttackCoroutine(AttackTarget attackTarget,bool isStrongAttack)
    {
        //攻撃エフェクト
        GameObject attackEffectPrefab = isStrongAttack ? strongAttackPrefab : weakAttackPrefab;
        Vector2 initialPosition = isStrongAttack ? attackTarget.instance.transform.position : this.transform.position;
        GameObject attackEffect = (GameObject)Instantiate(attackEffectPrefab, initialPosition, Quaternion.identity);
        attackEffect.GetComponent<EffectBase>().Set((UnoStruct.eColor)attribute, attackTarget.instance.pivot);

        //ダメージやHPの減った結果を保存
        int damagePoint = attackTarget.instance.hpRemain - attackTarget.hpResult;

        yield return new WaitWhile(() => attackEffect.GetComponent<EffectBase>().bAtack == false);

        attackTarget.instance.Damaged(damagePoint, this.attribute);

        yield return new WaitWhile(() => attackEffect.GetComponent<EffectBase>().bEnd == false);
        Destroy(attackEffect);
    }


    private AttackTarget GetAttackTarget(int attackPoint,Enemy lockOnTarget)
    {
        int judgeLineHpResult = 1000000000;
        float weaknessMagnification;              //属性倍率
        List<Enemy> targetEnemyList = new List<Enemy>();
        List<AttackTarget> sameHpTargetEnemyList = new List<AttackTarget>();

        if(!lockOnTarget)
        {
            //ターゲットロックオンしていない場合
            List<Enemy> weaknessEnemyList = new List<Enemy>();
            List<Enemy> nonWeaknessEnemyList = new List<Enemy>();
            List<Enemy> resistanceEnemyList = new List<Enemy>();

            List<Enemy> enemyList = GameMainUpperManager.instance.enemyList;

            //敵が存在しなかったらreturn
            if (enemyList.Count == 0) return null;

            //敵を弱点属性とそうでないものに仕分ける
            foreach (Enemy elem in enemyList)
            {
                // Enemy enemy = obj.GetComponent<Enemy>();

                //死んでいる敵はカウントしない
                if (elem.isDead) continue;

                if (IsTargetAttributeWeakness(elem) == true)
                {
                    weaknessEnemyList.Add(elem);
                }
                else if (IsTargetAttributeResistance(elem) == true)
                {
                    //抵抗属性
                    resistanceEnemyList.Add(elem);
                }
                else
                {
                    nonWeaknessEnemyList.Add(elem);
                }
            }


            if (weaknessEnemyList.Count == 0 && nonWeaknessEnemyList.Count == 0 && resistanceEnemyList.Count == 0)
            {
                //敵が全て死んでいたらreturn
                return null;
            }
            else if (weaknessEnemyList.Count > 0)
            {
                //弱点属性の敵がいたらターゲットをそれ（それら）に絞る
                targetEnemyList = weaknessEnemyList;
                weaknessMagnification = BattleDataBase.weaknessMagnification;
            }
            else if (nonWeaknessEnemyList.Count > 0)
            {
                targetEnemyList = nonWeaknessEnemyList;
                weaknessMagnification = 1f;
            }
            else
            {
                targetEnemyList = resistanceEnemyList;
                weaknessMagnification = BattleDataBase.resistanceMagnification;
            }
        }
        else
        {
            //ターゲットロックオンしている場合
            targetEnemyList.Add(lockOnTarget);
            if (IsTargetAttributeWeakness(lockOnTarget) == true)
            {
                weaknessMagnification=BattleDataBase.weaknessMagnification;
            }
            else if(IsTargetAttributeResistance(lockOnTarget) == true)
            {
                weaknessMagnification = BattleDataBase.resistanceMagnification;
            }
            else
            {
                weaknessMagnification = 1f;
            }
        }


        //被攻撃後HPが同じになるターゲットを抽出
        foreach (Enemy targetEnemy in targetEnemyList)
        {
            int enemyHpRemain = targetEnemy.hpRemain;

            //攻撃後の敵の残りHPを計算
            int enemyHpResult = enemyHpRemain - Mathf.FloorToInt(attackPoint * weaknessMagnification);

            //攻撃後に最も敵のHPが低くなるようにターゲット選択
            if (enemyHpResult < judgeLineHpResult)
            {
                judgeLineHpResult = enemyHpResult;
                sameHpTargetEnemyList.Clear();
                sameHpTargetEnemyList.Add(new AttackTarget(targetEnemy, enemyHpResult));
            }
            else if (enemyHpResult == judgeLineHpResult)
            {
                sameHpTargetEnemyList.Add(new AttackTarget(targetEnemy, enemyHpResult));
            }
        }

        //被攻撃後HPが同じになるターゲットからランダムで選択
        int i = UnityEngine.Random.Range(0, sameHpTargetEnemyList.Count);
        AttackTarget attackTarget = sameHpTargetEnemyList[i];

        return attackTarget;
    }


    //弱点属性かどうかを判別する
    private bool IsTargetAttributeWeakness(Enemy enemy)
    {
        switch (this.attribute)
        {
            case Attribute.Solar:
                if (enemy.attribute == Attribute.Wind) { return true; }
                break;
            case Attribute.Thander:
                if (enemy.attribute == Attribute.Water) { return true; }
                break;
            case Attribute.Water:
                if (enemy.attribute == Attribute.Solar) { return true; }
                break;
            case Attribute.Wind:
                if (enemy.attribute == Attribute.Thander) { return true; }
                break;
            default:
                break;
        }

        return false;
    }

    //抵抗属性かどうかを判別する
    private bool IsTargetAttributeResistance(Enemy enemy)
    {
        switch (this.attribute)
        {
            case Attribute.Solar:
                if (enemy.attribute == Attribute.Water) { return true; }
                break;
            case Attribute.Thander:
                if (enemy.attribute == Attribute.Wind) { return true; }
                break;
            case Attribute.Water:
                if (enemy.attribute == Attribute.Thander) { return true; }
                break;
            case Attribute.Wind:
                if (enemy.attribute == Attribute.Solar) { return true; }
                break;
            default:
                break;
        }

        return false;
    }


    //攻撃値を計算して返す
    public int GetAttackPoint(float fNumber)
    {
        if (fNumber <= 0f) return 0;

        int attackPoint = Mathf.FloorToInt(fNumber * attack);

        return attackPoint;
    }


    public UnoStruct.tCard GetTCard()
    {
        UnoStruct.tCard tCard;
        tCard.m_Number = cardNum;
        tCard.m_Color = (UnoStruct.eColor)attribute;

        return tCard;
    }


}

public class AttackTarget
{
    public Enemy instance { get; private set; }
    public int hpResult { get; private set; }

    public AttackTarget(Enemy instance, int hpResult)
    {
        this.instance = instance;
        this.hpResult = hpResult;
    }
}


[Serializable]
public class CharactorData
{
    [SerializeField]
    private int Hp;
    public int hp { get { return Hp; } private set { Hp = value; } }
    [SerializeField]
    private int Attack;
    public int attack { get { return Attack; } private set { Attack = value; } }
    [SerializeField]
    private string SkillText;
    public string skillText { get { return SkillText; } private set { SkillText = value; } }
    [SerializeField]
    private string SpriteName;
    public string spriteName { get { return SpriteName; } private set { SpriteName = value; } }
    [SerializeField]
    private Charactor.Attribute Attribute;
    public Charactor.Attribute attribute { get { return Attribute; } private set { Attribute = value; } }
    [SerializeField]
    private UnoStruct.eNumber CardNum;
    public UnoStruct.eNumber cardNum{ get { return CardNum;}set { CardNum = value; } }
    [SerializeField]
    private bool IsLeader;
    public bool isLeader{ get { return IsLeader;} set { IsLeader = value; } }


    //デバッグとしてのデータ登録用
    public CharactorData(Charactor_Debug charactorData)
    {
        this.hp = charactorData.hp;
        this.attack = charactorData.attack;
        this.skillText = charactorData.skillText;
        this.spriteName = charactorData.spriteName;
        this.attribute = charactorData.attribute;
    }
}