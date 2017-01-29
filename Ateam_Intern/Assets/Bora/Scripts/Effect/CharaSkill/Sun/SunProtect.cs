using UnityEngine;
using System.Collections;

public class SunProtect : EffectBase {

	SkillRise skillBase = null;
	SpriteRenderer render = null;
    //float fTempAlpha = 0.0f;

    bool bStart = true;
    [SerializeField]
    float fStartTime = 0.5f;

    static bool bUse = false;

    [SerializeField]
    float fRotTime = 30.0f;

	[SerializeField]
	float fAtten = 0.25f;

	[SerializeField]
	float fRivisionAlpha = 0.25f;

	void Start () {
        render = GetComponent<SpriteRenderer>();
	}

	void Update () {

        // 回転
        transform.eulerAngles += new Vector3(0, 0, 360 * (Time.deltaTime / fRotTime));

        if (bStart)
        {
            render.color += new Color(0, 0, 0, 1.0f * (Time.deltaTime / fStartTime));
            if (render.color.a >= 1.0f)
            {
                render.color = new Color(1,1,1,1);
                bStart = false;
            }
            return;
        }

        float fNowAlpha = ((1.0f - fRivisionAlpha) * (float)SkillRise.nNum / (float)skillBase.GetMaxNum) + fRivisionAlpha;
        if (render.color.a > fNowAlpha)
        {
            render.color -= new Color(0, 0, 0, 1.0f * (Time.deltaTime / fAtten));
            //render.color -= new Color(0, 0, 0, (fTempAlpha - fNowAlpha) * (Time.deltaTime / fAtten));

             if (render.color.a < fNowAlpha)
             {
                 render.color = new Color(1,1,1,fNowAlpha);
             }
             //Debug.Log("Down");
        }
        else if (render.color.a < fNowAlpha)
        {
            render.color += new Color(0, 0, 0, 1.0f * (Time.deltaTime / fAtten));
            //render.color += new Color(0, 0, 0, (fTempAlpha - fNowAlpha) * (Time.deltaTime / fAtten));

            if (render.color.a > fNowAlpha)
            {
                render.color = new Color(1,1,1,fNowAlpha);
            }
            //Debug.Log("Up");
        }
        /*else
        {
            fTempAlpha = render.color.a;
        }*/

		// 勝ちなら強制終了
		if (ResultManager.Instance.bWin) {
			bUse = false;
			Destroy (this.gameObject);
		}

        if (SkillRise.nNum <= 0)
        {
            //skillBase.nNum = 0;
			render.color -= new Color (0,0,0, fRivisionAlpha * (Time.deltaTime / fAtten)); 
			if (render.color.a <= 0.0f) {
				bUse = false;
                Destroy(this.gameObject);
                //Debug.Log("destroySun");
			}
		}

        /*if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("レンダー : " + render.color.a + " 現在 : " + fNowAlpha);
        }*/
	}

	public override void Set(CharaSkillBase skillData) {
		skillBase = (SkillRise)skillData;
        skillBase.Run();

		if (bUse) {
            Destroy(this.gameObject);
            //Debug.Log("nonSun");
		} else {
			bUse = true;
            SoundManager.Instance.PlaySE(SoundManager.eSeValue.SE_SUN);
            //Debug.Log("createSun");
		}
	}
}
