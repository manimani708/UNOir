using UnityEngine;
using System.Collections;

public class Boss_Appear : MonoBehaviour {
	
	public float fTime = 0.75f;
	Vector3 MaxSize = Vector3.zero;
	public bool bEnd { get; private set; }

	public Boss_TextMove textMove = null;

	float fNowWait = 0.0f;
	public float fWait = 1.0f; 

	bool bSe = false;

	// Use this for initialization
	void Start () {
		bEnd = false;
		MaxSize = transform.localScale;
		transform.localScale = Vector3.zero;
		SoundManager.Instance.StopBGM (SoundManager.eBgmValue.BGM_ENEMYBATTLE);
		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_ALARM);
	}
	
	// Update is called once per frame
	void Update () {

		//if (TouchManager.Instance.TouchCheck ()) {
		//	bEnd = true;
		//}

		if (!textMove.bEnd || bEnd)
			return;

		if (!bSe) {
			SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_BOSSSTARTTEXT);
			bSe = true;
		}

		if (transform.localScale.x >= MaxSize.x) {
			fNowWait += Time.deltaTime;

			if (fNowWait >= fWait) {
				bEnd = true;
				SoundManager.Instance.StopSE ();
				SoundManager.Instance.PlayBGM (SoundManager.eBgmValue.BGM_BOSSBATTLE);
			}
		} else {
			transform.localScale += MaxSize * (Time.deltaTime / fTime);
		}
	}
}
