using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour {

    //[SerializeField]
    UnoData unoData = null;
	//UnoStruct.eColor MyColor = UnoStruct.eColor.COLOR_MAX;

    SpriteRenderer render = null;
    Vector3 InitPos = Vector3.zero;


    Sprite[] TempSprite = new Sprite[4];

	// Use this for initialization
    void Start() {
        InitPos = transform.localPosition;
        render = GetComponent<SpriteRenderer>();
        unoData = GetComponentInParent<UnoData>();
        TempSprite = ResourceHolder.Instance.GetResource(ResourceHolder.eResourceId.ID_POWERUP);
	}

	// Update is called once per frame
	void Update () {

		bool bNowUse = PowerUpManager.Instance.GetUse (unoData.CardData.m_Color);
        if (!bNowUse || !BattleManager.Instance.GetIsInBattle())
        {
            render.color = new Color(1, 1, 1, 0);
            return;
        }

        render.sprite = TempSprite[(int)unoData.CardData.m_Color];

        render.color = PowerUpAlpha.Instance.NowColor;
        transform.localPosition = InitPos - PowerUpAlpha.Instance.NowPos;
	}
}
