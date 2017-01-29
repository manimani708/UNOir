using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DamageNumber : MonoBehaviour {

    [SerializeField]
    private float AnimationTime = 0.2f;

	// Use this for initialization
	void Start () 
    {
        ShowAnimation();
	}

	

    private void ShowAnimation()
    {
        Sequence _myseq = DOTween.Sequence();
        _myseq.Append(transform.DOLocalMoveY(0.5f, AnimationTime / 2f));
        _myseq.Append(transform.DOLocalMoveY(0f, AnimationTime / 2f));
        _myseq.AppendCallback(OnCompleteAnimation);
    }


    private void OnCompleteAnimation()
    {
        Destroy(gameObject);
    }

}
