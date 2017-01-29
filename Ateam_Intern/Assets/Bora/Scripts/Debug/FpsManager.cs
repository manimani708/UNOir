using UnityEngine;
using System.Collections;

public class FpsManager : MonoBehaviour {

	//----- Singleton
	protected static FpsManager instance;

	public static FpsManager Instance {
		get {
			if (instance == null) {
				instance = (FpsManager)FindObjectOfType(typeof(FpsManager));

				if (instance == null) {
					Debug.LogError("FpsManager Instance Error");
				}
			}

			return instance;
		}
	}

	[SerializeField]
	bool bAndroid = true;

	[SerializeField]
	bool bEditor = true;

	void Awake() {
		if (this != Instance) {
			Destroy(this.gameObject);
			return;
		}

		// シーン遷移で破棄させない
		DontDestroyOnLoad(gameObject);
	}


	int frameCount;
	float nextTime;
	int PrevFps;

	public int FontSize = 10;

	public int nCnt = 0;

	GUIStyle Style;

	void Start () {
		// 次の時間を保存
		nextTime = Time.time + 1;

		frameCount = 0;
		PrevFps = 0;

		Style = new GUIStyle();
		Style.fontSize = FontSize;
	}
		
	void Update () {
		// フレームの加算
		frameCount++;

		//if (Time.time >= nextTime) {
		//	// 1秒経ったらFPSを保存
		//	Debug.Log("FPS : "+frameCount);
		//	frameCount = 0;
		//	nextTime += 1;
		//}

		/*if (Input.touchCount > 0) {
			if (Input.GetTouch (0).phase == TouchPhase.Began || Input.GetTouch (0).phase == TouchPhase.Ended) {
				if (Input.GetTouch (0).fingerId == TouchManager.Instance.nTouchId) {
					nCnt++;
				}
			}
		}*/
	}


	void OnGUI() {
		if (Application.platform == RuntimePlatform.Android) {
			if (!bAndroid) {
				return;
			}
		} else {
			if (!bEditor) {
				return;
			}
		}

		if (Time.time >= nextTime) {
			// 1秒経ったらFPSを保存
			PrevFps = frameCount;
			frameCount = 0;
			nextTime += 1;
		}
		string label = "  FPS:" + PrevFps;
		GUI.Label (new Rect(0,0,400,400), label, Style);

		/*label = "  Touch " + nCnt; // + PrevFps;
		GUI.Label(new Rect(0, 0, 400, 400), label, Style);

		label = "  TempList : " + FieldCard.Instance.TempList.Count; // + PrevFps;
		GUI.Label(new Rect(0, 100, 400, 400), label, Style);

		label = "  TempCnt : " + FieldCard.Instance.TempCnt; // + PrevFps;
		GUI.Label(new Rect(0, 200, 400, 400), label, Style);*/
	}
}
