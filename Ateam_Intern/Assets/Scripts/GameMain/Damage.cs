using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Damage : MonoBehaviour 
{
    public GameObject damageNumberPrefab = null;

    [SerializeField]
    private float diff = 1f;



    public int damage_Debug = 0;

    private Sprite[] numbers;
    private List<int> digitList = new List<int>();

	// Use this for initialization
	void Start () {
		numbers = ResourceHolder.Instance.GetResource (ResourceHolder.eResourceId.ID_NUMBER);
        //numbers = Resources.LoadAll<Sprite>("Textures/Number/number");
	}
	
	// Update is called once per frame
	void Update () 
    {
		/*if(Input.GetKeyDown(KeyCode.Return))
        {
            ShowDamage(damage_Debug,Charactor.Attribute.Solar);
        }*/
	}


    public void ShowDamage(int damage,Charactor.Attribute attribute)
    {
        CreateDamageNumbers(damage,attribute);
    }


    //ダメージの数字を生成
    private void CreateDamageNumbers(int damage, Charactor.Attribute attribute)
    {
        SetDigitList(damage);
        float pivot = GetPivot();
        Vector2 randomDifference = new Vector2(Random.Range(-diff, diff), Random.Range(-diff, diff));

        int i = 0;
        foreach (var elem in digitList)
        {
            GameObject go = Instantiate(damageNumberPrefab);
            SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = numbers[elem];
            spriteRenderer.color = GetDamageColor(attribute);

            //中心からの差
            float difference = i - pivot;

            //親と同じサイズにする
            go.transform.localScale = go.transform.lossyScale * DamageConfig.sizeMagnification;

            //サイズに応じて文字間隔を変える
            float distanceInterval = difference * DamageConfig.distanceIntervalMagnification * DamageConfig.sizeMagnification;
            Vector2 pos = new Vector2(transform.position.x + distanceInterval, transform.position.y);
           
            go.transform.position = pos + randomDifference;
            go.transform.SetParent(this.transform);
            i++;
        }
    }


    //属性によるダメージの色を取得する
    private Color GetDamageColor(Charactor.Attribute attribute)
    {
        Color color;

        switch (attribute)
        {
            case Charactor.Attribute.Solar:
                color = Color.red;
                break;
            case Charactor.Attribute.Water:
                color = Color.blue;
                break;
            case Charactor.Attribute.Thander:
                color = Color.yellow;
                break;
            case Charactor.Attribute.Wind:
                color = Color.green;
                break;
            default:
                color = Color.white;
                break;
        }

        return color;
    }


    //ダメージの桁数を計算
    private void SetDigitList(int damageNum)
    {
        digitList.Clear();

        if (damageNum == 0)
        { 
            digitList.Add(0);
            return;
        }

        while(damageNum > 0)
        {
            int n = damageNum % 10;
            digitList.Insert(0, n);
            damageNum /= 10;
        }
    }


    //桁数から見た中心を計算
    private float GetPivot()
    {
        float pivot;
        int length = digitList.Count;

        if(length % 2 == 1)
        {
            //奇数   
            pivot = (length - 1) / 2;
        }
        else
        {
            //偶数
            pivot = length / 2 - 0.5f;
        }

        return pivot;
    }
}
