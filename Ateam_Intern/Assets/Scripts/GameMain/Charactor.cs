using UnityEngine;
using System.Collections;

public class Charactor : MonoBehaviour 
{
    //属性
    public enum Attribute
    {
        Solar,               //日
		Water,               //水
		Thander,             //雷
		Wind,                //風
    }

    public int hpMax { get; protected set; }                //最大HP
    public int attack { get; protected set; }            //攻撃力
    public Attribute attribute { get; protected set; }    //属性
}
