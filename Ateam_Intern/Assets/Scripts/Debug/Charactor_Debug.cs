using System;
using UnityEngine;
using UnityEngine.UI;


public class Charactor_Debug : MonoBehaviour
{

    [SerializeField]
    private int Hp;
    public int hp { get { return Hp; } private set { Hp = value; } }
    [SerializeField]
    private int Attack;
    public int attack { get { return Attack; } private set { Attack = value; } }
    [SerializeField, TextAreaAttribute(4, 4)]
    private string SkillText;
    public string skillText { get { return SkillText; } private set { SkillText = value; } }
    [SerializeField]
    private Charactor.Attribute Attribute;
    public Charactor.Attribute attribute { get { return Attribute; } private set { Attribute = value; } }


    public string spriteName { get; private set; }
    public CharactorData charactorData { get; private set;}

    void Awake()
    {
        spriteName = GetComponent<Image>().sprite.name;
        charactorData = new CharactorData(this);
    }

    public void Init(CharactorData charactorData)
    {
        this.hp = charactorData.hp;
        this.attack = charactorData.attack;
        this.skillText = charactorData.skillText;
        this.spriteName = charactorData.spriteName;
        this.attribute = charactorData.attribute;
    }

}

