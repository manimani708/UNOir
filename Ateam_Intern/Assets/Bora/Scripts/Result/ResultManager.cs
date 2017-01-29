using UnityEngine;
using System.Collections;

public class ResultManager : MonoBehaviour {

	static ResultManager instance;

	public static ResultManager Instance {
		get {
			if (instance == null) {
				instance = (ResultManager)FindObjectOfType(typeof(ResultManager));

				if (instance == null) {
					Debug.LogError("ResultManager Instance Error");
				}
			}
			return instance;
		}
	}
		
	public bool bResult { get; private set; }
	[SerializeField] Sun sun = null;

	public GameObject GameOverObj = null;
//	public GameObject NoAnswerObj = null;
	public GameObject ResultObj = null;
	public GameObject ContinueObj = null;
	GameOver gameOver = null;
//	NoAnswer noAnswer = null;
	Result result = null;
	Continue continueCs = null;

	public bool bWin { get; private set; }
	[SerializeField]
	DeathRed deathRed = null;

	void Awake() {
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}
		bResult = false;
		bWin = false;
	}

	// Use this for initialization
	void Start () {
		sun.enabled = false;
		gameOver = GameOverObj.GetComponent<GameOver> ();
//		noAnswer = NoAnswerObj.GetComponent<NoAnswer> ();
		result = ResultObj.GetComponent<Result> ();
		continueCs = ContinueObj.GetComponent<Continue> ();
	}

	// Update is called once per frame
	void Update () {

		if (!bWin && deathRed.bEnd) {
			gameOver.bOn = true;
			bWin = true;
			SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_ONWINDOW);
			SoundManager.Instance.PlayBGM (SoundManager.eBgmValue.BGM_GAMEOVER);
		}

		if (sun && sun.End) {// && TouchManager.Instance.TouchCheck()) {
			WinDrow ();		
		}
	}

	void WinDrow() {
		sun = null;
		ResultObj.SetActive (true);
		result.bOn = true;
		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_ONWINDOW);
		// WinBgm再生
	}

	public void SetResult(bool bFlg) {

		if (bResult)
			return;

		// 勝ち
		if (bFlg) {
			sun.enabled = true;
			SoundManager.Instance.StopBGM ();
			//SoundManager.Instance.PlayBGM (SoundManager.eBgmValue.BGM_WIN);
			SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_WIN);
		} 

		// 負け
		else {
			GameOverObj.SetActive (true);
			ResultObj.SetActive (true);
			ContinueObj.SetActive (true);
			deathRed.Run ();
			//gameOver.bOn = true;
		}
		bWin = bFlg;
		bResult = true;
	}

	public void EndResult() {
		gameOver.Start ();
//		noAnswer.Start ();
		result.Start ();
		continueCs.Start ();
		GameOverObj.SetActive (false);
//		NoAnswerObj.SetActive (false);
		ResultObj.SetActive (false);
		ContinueObj.SetActive (false);
		deathRed.End ();

		bResult = false;
	}
}
