using UnityEngine;
using DG.Tweening;

public class TimeGauge : MonoBehaviour 
{
    [SerializeField]
    private float vibrationDuration = 10f;  //振動の最大角度

    [SerializeField]
    private float vibratonTime = 0.5f;      //振動時間

    public bool isVibrating { get; private set; }

    private float localScaleXMax;   //ゲージの最大幅スケール


    void Awake()
    {
        localScaleXMax = transform.localScale.x;
        Scale(0f);
        isVibrating = false;
    }

    void Update()
    {
        if(!isVibrating && Input.GetKeyDown(KeyCode.V))
        {
            Vibration();
        }
    }


    //スケールを更新
    public void Scale(float timePercentage)
    {
        Vector3 newScale = new Vector3(localScaleXMax * timePercentage, transform.localScale.y, transform.localScale.z);
        transform.localScale = newScale;
    }


    //振動アニメーション
    public void Vibration()
    {
        isVibrating = true;
        Transform parentTransform = this.transform.parent;

        Sequence _myseq = DOTween.Sequence();
        _myseq.Append(parentTransform.DOPunchRotation(new Vector3(0f, 0f, vibrationDuration), vibratonTime));
        _myseq.AppendCallback(VibrationCallBack);
    }


    private void VibrationCallBack()
    {
        isVibrating = false;
    }

}
