using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_BackFadeIn : MonoBehaviour {

	public float fTime = 0.25f;
	SpriteRenderer renderer = null;
	public bool bEnd { get; private set; }
	public List<GameObject> RogoList = new List<GameObject>();
	Enemy_StageRogo stageRogo = null;

	// Use this for initialization
	void Start () {
		bEnd = false;
		renderer = GetComponent<SpriteRenderer> ();
		renderer.color = new Color (1,1,1,0);
	}

	// Update is called once per frame
	void Update () {

		if (FadeManager.Instance.GetFadeing ())
			return;

		if (TouchManager.Instance.TouchCheck ()) {
			gameObject.AddComponent<Enemy_AllFadeOut> ().End = true;
			stageRogo = null;
			bEnd = true;
		}

		if (stageRogo && stageRogo.bEnd) {
			this.gameObject.AddComponent<Enemy_AllFadeOut> ();
			stageRogo = null;
		}

		if (bEnd)
			return;

		renderer.color += new Color (0,0,0, 1.0f * (Time.deltaTime/fTime));

		if (renderer.color.a >= 1.0f) {
			bEnd = true;
			int nStage = BattleManager.Instance.nowBattleCount - 1; // ここでステージ数取得
			GameObject Obj = (GameObject)Instantiate(RogoList[nStage], transform.position, transform.rotation);
			stageRogo = Obj.GetComponent<Enemy_StageRogo> ();
			Obj.transform.SetParent (this.transform);
            SoundManager.Instance.PlaySE(SoundManager.eSeValue.SE_STAGEROGO);
		}
	}
}
