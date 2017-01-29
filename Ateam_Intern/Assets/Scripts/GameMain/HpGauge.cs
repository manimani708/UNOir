using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class HpGauge : MonoBehaviour
{
    [SerializeField]
    private float TakeScalingTime = 1.5f;       //イージングに何秒かけるか
    public float takeScalingTime{ get { return TakeScalingTime;} set { TakeScalingTime = value; } }

    public bool isAnimating { get; private set;}

    private float localScaleXMax;               //ゲージの最大幅スケール


    void Awake()
    {
        localScaleXMax = transform.localScale.x;

    }


    //ゲージ推移アニメーション
    public void ShowAnimation(float hpPercentage,object obj)
    {
        isAnimating = true;
        transform.DOScaleX(localScaleXMax * hpPercentage, takeScalingTime).SetEase(Ease.OutQuad).OnComplete(() => JudgeIsIsInBattle(hpPercentage,obj));   
    }


    public void SetGaugeImmidiate(float hpPercentage)
    {
        Vector3 newScale = new Vector3(localScaleXMax * hpPercentage, transform.localScale.y, transform.localScale.z);
        transform.localScale = newScale;
    }


    public void JudgeIsIsInBattle(float percentage,object obj)
    {
        isAnimating = false;

        if (percentage > 0f) return;

        string objectType = obj.GetType().ToString();

        switch (objectType)
        {
            case "Player":
                if (percentage <= 0f)
                {
                    BattleManager.Instance.SetIsInBattle(false);
                }
                break;
            case "Enemy":
                if (percentage <= 0f)
                {
                    //非戦闘中だったらreturn
                    if (BattleManager.Instance.GetIsInBattle() == false) return;

                    Enemy enemy = obj as Enemy;

                    enemy.SetIsDiedTrigger();
                    //敵を暗くする
                    enemy.SetMaskSprite(true);

                    //ロックオンサイトを消す
                    GameMainUpperManager.instance.player.ResetTargetEnemy();


                    List<Enemy> enemyList = GameMainUpperManager.instance.enemyList;
                    foreach(Enemy elem in enemyList)
                    {
                        if (!elem.isDead) return;
                    }
                    //敵が全員死んでいたら戦闘終了
                    BattleManager.Instance.SetIsInBattle(false);
                }
                break;
            default:
                break;
        }
    }






}
