using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class HPNumber : MonoBehaviour 
{
    public enum State
    {
        Safe,       //安全
        Caution,    //注意
        Pinti,      //ピンチ
    }

    [SerializeField]
    private GameObject numberPrefab = null;

    [SerializeField,Range(0,1)]
    private float cautionBorder = 0.5f;

    [SerializeField, Range(0, 1)]
    private float pintiBorder = 0.25f;


    public int number { get; private set;}
    public State state { get; private set;}

    private List<int> hpMaxDigit = new List<int>();
    private List<int> hpRemainDigit = new List<int>();
    private Sprite[] numberSprites = null;
   
    private List<NumberObject> numberObject = new List<NumberObject>();
    private bool isTweening = false;
    private int pivotNum = 0;
    private State beforeState;
    //private int beforeNumber;
    private bool stateChangeTrigger = false;
    

    public void Init () 
    {
        state = State.Safe;
        beforeState = state;
        number = GameMainUpperManager.instance.player.hpMax;
       // beforeNumber = number;
        SetDigitList(number, hpMaxDigit);
        SetDigitList(number, hpRemainDigit);

		numberSprites = ResourceHolder.Instance.GetResource (ResourceHolder.eResourceId.ID_HP_NUMBER);
        //numberSprites = Resources.LoadAll<Sprite>("Textures/Number/hp_number");
        CreateNumberSprites();
	}
	

    void Update () 
    {
        //デバッグコマンド
	    if(Input.GetKeyDown(KeyCode.J))
        {
            state = State.Safe;
            ChangeSprite();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            state = State.Caution;
            ChangeSprite();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            state = State.Pinti;
            ChangeSprite();
        }


        //if (isTweening)
        {
            float percentage = 1f * number / GameMainUpperManager.instance.player.hpMax;

            if (percentage <= pintiBorder)
            {
                state = State.Pinti;
            }
            else if (percentage <= cautionBorder)
            {
                state = State.Caution;
            }
            else
            {
                state = State.Safe;
            }

            ChangeSprite();
        }

        if(IsStateChanged())
        {
            stateChangeTrigger = true;
        }

        // beforeNumber = number;

        beforeState = state;
	}

    public void ShowAnimation(int endNum,float time)
    {
        //if (isTweening) return;

        isTweening = true;
        DOTween.To(() => number, (x) => number = x, endNum, time).OnComplete(OnAnimationCompleted);
    }

    private void OnAnimationCompleted()
    {
        isTweening = false;
    }

    //体力のスプライトを生成
    private void CreateNumberSprites()
    {
        int digitCount = hpMaxDigit.Count;
        float difference = 0f;

        //数字
        for (int j = 0; j < 2;j++)
        {
            for (int i = digitCount; i > 0; i--)
            {
                CreateSprite(hpMaxDigit[i - 1], difference);
                difference = difference + 0.25f;
            }

            if(j == 0)
            {
                CreateSprite(10, difference);
                difference = difference + 0.25f;
            }
        }

        pivotNum = GetPivot();
    }


    private void CreateSprite(int n,float difference)
    {
        GameObject go = Instantiate(numberPrefab);
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = numberSprites[n];

        Vector2 pos = new Vector2(transform.position.x - difference, transform.position.y);
        difference = difference + 0.25f;

        go.transform.position = pos;
        go.transform.SetParent(this.transform);
        numberObject.Add(new NumberObject(go, n));
    }


    private void ChangeSprite()
    {
        SetDigitList(number, hpRemainDigit);
        int digitCount = hpRemainDigit.Count;

        int i = 0;
        foreach(var obj in numberObject)
        {
            //残りHPの更新
            if(i > pivotNum)
            {
                if(obj.gameobject.activeSelf == true)
                {
                    //最大桁より少ない桁数になったら非表示
                    if(i - pivotNum > digitCount)
                    {
                       obj.gameobject.SetActive(false);
                        i++;
                        continue; 
                    }
                }
                else
                {
                    //回復などして桁数が増えたら（戻ったら）再表示
                    if(i-pivotNum <= digitCount)
                    {
                       obj.gameobject.SetActive(true);
                    }
                    else
                    {
                        i++;
                        continue;
                    }
                }

                obj.num = hpRemainDigit[(digitCount - 1) - (i - pivotNum - 1)];
            }

            int n = 11 * (int)state + obj.num;
            obj.gameobject.GetComponent<SpriteRenderer>().sprite = numberSprites[n];
            i++;
        }
    }


    //桁の数字をセット
    private void SetDigitList(int num,List<int> digit)
    {
        digit.Clear();

        if (num == 0)
        {
            digit.Add(0);
            return;
        }

        while (num > 0)
        {
            int n = num % 10;
            digit.Insert(0, n);
            num /= 10;
        }
    }



    //桁数から見た中心を計算
    private int GetPivot()
    {
        int pivot;
        int length = numberObject.Count;

        if (length % 2 == 1)
        {
            //奇数   
            pivot = (length - 1) / 2;
        }
        else
        {
            //偶数
            pivot = length / 2;
        }

        return pivot;
    }

    class NumberObject
    {
        public GameObject gameobject { get; private set;}
        public int num { get; set;}

       public NumberObject(GameObject obj,int num)
        {
            this.gameobject = obj;
            this.num = num;
        }
    }

	public State GetState() {
		return state;
	}

    public bool IsStateChanged()
    {
        if(state != beforeState)
        {
            return true;
        }

        return false;
    }

    public bool GetStateChangeTrigger()
    {
        bool trigger = stateChangeTrigger;

        stateChangeTrigger = false;

        return trigger;
    }
}
