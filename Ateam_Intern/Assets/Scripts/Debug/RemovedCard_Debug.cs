using UnityEngine;
using System.Collections;

public class RemovedCard_Debug : MonoBehaviour 
{
    [SerializeField]
    private int Num;
    public int num{ get { return Num;}set { Num = value;}}

    [SerializeField]
    private RemovedCard.Symbol Symbol;
    public RemovedCard.Symbol symbol{ get { return Symbol;}set { Symbol = value; } }

    [SerializeField]
    private Charactor.Attribute Attribute;
    public Charactor.Attribute attribute { get { return Attribute; } private set { Attribute = value; } }
}
