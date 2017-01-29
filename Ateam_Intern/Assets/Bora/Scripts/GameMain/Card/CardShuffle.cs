using UnityEngine;
using System.Collections;

public class CardShuffle : MonoBehaviour
{

    public bool bShuffle { get; private set; }
    Vector3 CenterPos = new Vector3(0.0f, -2.2f, 0.0f);
    UnoData unoData = null;
    bool bSet = true;

    float fNowTime = 0.0f;
    float fTime = 0.1f;

    void Start()
    {
        bShuffle = false;
        unoData = GetComponent<UnoData>();
    }

    void Update()
    {
        if (!bShuffle)
            return;

        fNowTime += Time.deltaTime;
        if (bSet)
        {
            transform.position += (CenterPos - unoData.GetInitPos) * (Time.deltaTime / fTime);

            if (fNowTime >= fTime)
            {
                transform.position = CenterPos;
                bSet = false;
                fNowTime = 0.0f;
                unoData.Change();
            }
        }
        else
        {
            transform.position -= (CenterPos - unoData.GetInitPos) * (Time.deltaTime / fTime);

            if (fNowTime >= fTime)
            {
                transform.position = unoData.GetInitPos;
                bSet = true;
                fNowTime = 0.0f;

                bShuffle = false;
            }
        }
    }

    public void Shuffle()
    {
        bShuffle = true;
        bSet = true;
    }

}
