using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FeverGauge : MonoBehaviour 
{
    [SerializeField]
    private float takeScalingTime = 0.5f;       //イージングに何秒かけるか

    [SerializeField]
    private int FeverPointMax;  //何枚消すとフィーバーになるか
    public int feverPointMax{ get { return FeverPointMax;} set { FeverPointMax = value; } }

    [SerializeField]
    private float feverTime = 5f;   //フィーバー時間
	public float FeverTime { get { return feverTime; } }

    public int feverPoint { get; private set;}                         //現在何枚消したか
    private int beforeFeverPoint;                   //前フレームのフィーバーポイント
    public bool isFeverMode { get; private set;}    //フィーバー中かどうか
    private Texture2D texture;                      //ゲージ画像
    private bool isgaugeChanging = false;           //ゲージ推移中か
    private float gaugeSizeY;                       //ゲージのサイズ割合

    void Awake()
    {
        texture = GetComponent<SpriteRenderer>().sprite.texture;
        gaugeSizeY = 0f;
        feverPoint = 0;
        beforeFeverPoint = feverPoint;
        isFeverMode = false;
        UpdateSprite();
    }

    void Update()
    {
        if(!isFeverMode)
        {
            if (!BattleManager.Instance.GetIsInBattle()) return;

            if(!isgaugeChanging)
            {
                //Maxまで溜まったらフィーバーモード突入
               if (beforeFeverPoint == feverPointMax)
                {
                    isFeverMode = true;
                }
                //フィーバーポイントが変化したらゲージサイズを変化させる
                else if (feverPoint != beforeFeverPoint)
                {
                    isgaugeChanging = true;
                    ChargeGaugeSize(1f * feverPoint / feverPointMax);
                }

            }
        }
        else
        {
            if(!isgaugeChanging)
            {
                if (!BattleManager.Instance.GetIsInBattle()) return;

                feverPoint -=  Mathf.FloorToInt(feverPointMax / feverTime);
                isgaugeChanging = true;
                ReduceGaugeSize(1f * feverPoint / feverPointMax);
            }
        }

        //ゲージサイズ変化中はスプライト更新
        if (isgaugeChanging)
        {
            UpdateSprite();
        }

        /*if(Input.GetKeyDown(KeyCode.F))
        {
            isFeverMode = !isFeverMode;
        }*/

    }

    private void UpdateSprite()
    {
        Rect rect = new Rect(0, 0, texture.width, gaugeSizeY);
        Vector2 pivot = new Vector2(0.5f, 0f);
        Sprite newSprite = Sprite.Create(texture, rect, pivot);
        GetComponent<SpriteRenderer>().sprite = newSprite;
    }


    //ゲージ蓄積アニメーション
    private void ChargeGaugeSize(float percentage)
    {
        DOTween.To(() => gaugeSizeY, (x) => gaugeSizeY = x, texture.height * percentage, takeScalingTime).SetEase(Ease.OutQuad).OnUpdate(() => {beforeFeverPoint = feverPoint; }).OnComplete(() => { isgaugeChanging = false;  beforeFeverPoint = feverPoint; });
    }

    //フィーバー時間中はだんだんゲージが減っていく
    private void ReduceGaugeSize(float percentage)
    {
        DOTween.To(() => gaugeSizeY, (x) => gaugeSizeY = x, texture.height * percentage, 1f).SetEase(Ease.Linear).OnComplete(() => 
        { 
            isgaugeChanging = false; 
            if (feverPoint == 0)
            {
                feverPoint = 0;
                isFeverMode = false;
            }
            beforeFeverPoint = feverPoint; 
        });
    }

    //初期化
    public void Init()
    {
        gaugeSizeY = 0f;
        feverPoint = 0;
        beforeFeverPoint = feverPoint;
        isFeverMode = false;
        isgaugeChanging = false;
        UpdateSprite();
    }

    //フィーバーポイントを1増やす
    public void IncrementPoint()
    {
        //非戦闘中は増やさない
        if (!BattleManager.Instance.GetIsInBattle()) return;

        //フィーバー中は増やさない
        if (isFeverMode) return;

        feverPoint++;
        if (feverPoint >= feverPointMax) { feverPoint = feverPointMax; }
    }

    public void SetPoint(int n)
    {
        //非戦闘中は操作不可
        if (!BattleManager.Instance.GetIsInBattle()) return;

        //フィーバー中はポイント操作不可
        if (isFeverMode) return;

        feverPoint = n;
    }

    public void SetIsFever(bool b)
    {
        isFeverMode = b;
    }


    public void FullChargeImmidiate()
    {
        SetPoint(feverPointMax);
        gaugeSizeY = texture.height;
        UpdateSprite();
    }

}
