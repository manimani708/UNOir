using UnityEngine;
using System.Collections;

public class FeverCard : MonoBehaviour {

    float fTime = 0.15f;
    float fMaxAlpha = 0.5f;

    bool bAdd = true;
    SpriteRenderer render = null;
    UnoData unoData = null;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        render.sortingOrder = 22;
        unoData = GetComponentInParent<UnoData>();
    }
	
	void Update () {

        if (!FeverEffectManager.Instance.GetFever())
        {
            render.color = new Color(1,1,1,0);
            return;
        }

        // 持ったら消す
        render.enabled = !unoData.Click;


        if (bAdd)
        {
            render.color += new Color(0,0,0, fMaxAlpha * (Time.deltaTime / fTime));

            if (render.color.a >= fMaxAlpha)
            {
                bAdd = false;
            }

        }
        else
        {
            render.color -= new Color(0, 0, 0, fMaxAlpha * (Time.deltaTime / fTime));

            if (render.color.a <= 0.0f)
            {
                bAdd = true;
            }

        }
	}
}
