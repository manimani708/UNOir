using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FieldCard : MonoBehaviour {

	static FieldCard instance;

	public static FieldCard Instance {
		get {
			if (instance == null) {
				instance = (FieldCard)FindObjectOfType(typeof(FieldCard));

				if (instance == null) {
					Debug.LogError("FieldCard Instance Error");
				}
			}
			return instance;
		}
	}

	// 変数宣言
	public UnoStruct.tCard m_Card { get; private set; }
	SpriteRenderer spriteRenderer = null;
	public GameObject FeverObj = null;
	FeverGauge feverGauge = null;
	int nTempCnt = 0;
	public bool bNotSet { get; private set; } 

	ParticleSystem EffectParticle = null;
	public GameObject EffectPrefab = null; 
	SetEffect setEffect = null;

	// 同時出し保存用
	List<UnoData> tempList = new List<UnoData>();

	// 
	int nOldFeverCnt = 0;
	public GameObject[] feverEffectObj = new GameObject[3];

	float fNowTempTime = 0.0f;
	public float fTempTime = 2.0f;

	#if DEBUG
	public string Color = "";
	public string Number = "";
	#endif

	void Awake() {
		if (this != Instance) {
			Destroy(this.gameObject);
			return;
		}
		bNotSet = false;
	}

	public void Init() {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	// Use this for initialization
	void Start () {
		GameObject effect = (GameObject)Instantiate (EffectPrefab, transform.position, transform.rotation);
		EffectParticle = effect.GetComponent<ParticleSystem> ();
		EffectParticle.Stop ();
		setEffect = EffectParticle.GetComponent<SetEffect> ();

		feverGauge = FeverObj.GetComponent<FeverGauge> ();
	}
	
	// Update is called once per frame
	void Update () {

		#if DEBUG
		Color = m_Card.m_Color.ToString();
		Number = m_Card.m_Number.ToString();
		#endif

		/*#if DEBUG
		if (Input.GetKeyDown (KeyCode.E)) {
			for (int i = 0; i < tempList.Count; i++) {
				Debug.Log ("保存してる" + i + "番目," + tempList[i].CardData.m_Color.ToString() + "," + tempList[i].CardData.m_Number.ToString());
			}
		}
		#endif*/

		if (tempList.Count > 0) {
			fNowTempTime = 0.0f;
			if (!bNotSet) {
				bNotSet = tempList [tempList.Count - 1].bCardMove;
			}
			//UnoData data = tempList[tempList.Count - 1];
			ResetCheck ();
		} else if (BattleManager.Instance.GetIsInBattle ()) {
			fNowTempTime += Time.deltaTime;
		} else {
			fNowTempTime = 0.0f;
		}

		if (BattleManager.Instance.GetIsInBattle ()) {
			if (nOldFeverCnt < feverGauge.feverPointMax - 1 && feverGauge.feverPoint == feverGauge.feverPointMax - 1) {
				Instantiate (feverEffectObj [0]);
				SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_UNO);
			} else if (nOldFeverCnt == feverGauge.feverPointMax - 1 && feverGauge.feverPoint >= feverGauge.feverPointMax) {
				Instantiate (feverEffectObj [1]);
				SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_FEVER);
			} else if (nOldFeverCnt < feverGauge.feverPointMax - 1 && feverGauge.feverPoint >= feverGauge.feverPointMax) {
				Instantiate (feverEffectObj [2]);
				SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_UNOFEVER);
			}
			nOldFeverCnt = feverGauge.feverPoint;
		}
	}

	void ResetCheck() {

		if (tempList.Count == nTempCnt) {
			nTempCnt = 0;
			for (int i = 0; i < tempList.Count; i++) {
				tempList [i].Collider.enabled = true;
			}
			tempList.Clear ();
			bNotSet = false;
			TrajectoryCreator.Instance.Reset ();
			//Debug.Log ("リセット");
		}
	}

	public UnoData GetMostForwardCard(UnoData my) {
		UnoData temp = my;

		if (tempList.Count > 0) {
			temp = tempList [tempList.Count - 1];
		}
		return temp;
	}

	public void AddTemp(UnoData data) {
		tempList.Add (data);
	}

	public List<UnoData> TempList {
		get { return tempList; }
	} 

	public bool Judge(UnoStruct.tCard card) {

		// Fever中なのでなんでも通す
		if (feverGauge.isFeverMode)
			return true;

		UnoStruct.tCard fieldCard;
		if (tempList.Count > 0) {
			fieldCard = tempList[tempList.Count-1].CardData; // 一番上の同時出しを比較する
		} else {
			fieldCard = m_Card;
		}

		if (card.m_Color == UnoStruct.eColor.COLOR_WILD ||
			fieldCard.m_Color == UnoStruct.eColor.COLOR_WILD ||
			card.m_Color == fieldCard.m_Color ||
			card.m_Number == fieldCard.m_Number) {
			return true;
		}

		return false;
	}

	public bool Change(UnoData data, bool bEffect = true) {

		UnoData tempEnd = null;
		int nTempNum = tempList.Count;
		if (tempList.Count > 0) {
			tempEnd = tempList [tempList.Count - 1];
		} else if(bEffect) {
			return false;
		}

		// カウント
		if (bEffect) {
			nTempCnt++;
			ResetCheck ();
		}

		// １番上のカードか判定
		if (data != tempEnd)
			return false;

		//	格納 
		m_Card = data.CardData;

		int nNumber = ((int)m_Card.m_Color * (int)UnoStruct.eNumber.NUMBER_MAX) + (int)m_Card.m_Number;
		spriteRenderer.sprite = CardResource.Instance.GetCardResource(nNumber);

		if (bEffect) {
			setEffect.ChangeEffectAmount (nTempNum, m_Card.m_Color);
			EffectParticle.Play ();

			if (data.CardData.m_Color == UnoStruct.eColor.COLOR_WILD) {
				SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_WILD);
			}

			for (int i = 0; i < nTempNum; i++) {
				feverGauge.IncrementPoint ();
			}

			// もしこれでフィーバーならtrueを返してキャラカード出現させる。
			if (!feverGauge.isFeverMode && feverGauge.feverPoint >= feverGauge.feverPointMax) {
				return true;
			}
		}
		return false;
	}

	public int TempCnt {
		set { nTempCnt = value; }
		get { return nTempCnt; }
	}

	public bool GetTempFlg() {
		return fNowTempTime >= fTempTime;
	}
}
