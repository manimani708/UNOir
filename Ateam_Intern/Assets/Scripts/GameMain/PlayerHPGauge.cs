using UnityEngine;
using System.Collections;

public class PlayerHPGauge : HpGauge 
{
    private HPNumber hpNumber;
    private Sprite[] gaugeSprites = null;

	void Start () 
    {
	    hpNumber = transform.parent.GetComponentInChildren<HPNumber>();
        //↓ゲージのスプライトを取得するように変える
        gaugeSprites = ResourceHolder.Instance.GetResource(ResourceHolder.eResourceId.ID_HPGAUGE);
	}
	
	void Update()
    {
        if (hpNumber.GetStateChangeTrigger() == true)
        {
            ChangeSprite();
        }
    }

    private void ChangeSprite()
    {
        GetComponent<SpriteRenderer>().sprite = gaugeSprites[(int)hpNumber.state];
    }
}
