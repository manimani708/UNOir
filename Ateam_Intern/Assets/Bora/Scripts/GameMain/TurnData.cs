using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnData : MonoBehaviour {

	static TurnData instance;

	public static TurnData Instance {
		get {
			if (instance == null) {
				instance = (TurnData)FindObjectOfType(typeof(TurnData));

				if (instance == null) {
					Debug.LogError("TurnData Instance Error");
				}
			}

			return instance;
		}
	}

	// 変数宣言
	public struct tTurnDataBase {
		public int nNum;		// 数字カードの枚数 
		public float fNumber; 	// 数字合計(補正含めた)
		public int nReverse; 	// リバース使用有無
		public int nSkip;		// スキップ使用有無
	};

	// これを結果として返す
	public struct tTurnData {
		public int nSetCard;
		public int nReverseNum;
		public int nSkipNum; 
		public tTurnDataBase Red;
		public tTurnDataBase Green;
		public tTurnDataBase Blue;
		public tTurnDataBase Yellow;
	};

	// ドロー２，４の処理をする
	public class CDrowData {
		public UnoStruct.eColor DrowColor;// { get : set };
		public float fLife;
		public float fAmount;

		public CDrowData() {
			DrowColor = UnoStruct.eColor.COLOR_MAX;
			fLife = 0.0f;
			fAmount = 0.0f;
		}

		public void Run(bool bFlg) {
			if (DrowColor != UnoStruct.eColor.COLOR_WILD) {
				PowerUpManager.Instance.Run (DrowColor, bFlg);
			} else {
				for (int i = 0; i < (int)UnoStruct.eColor.COLOR_WILD; i++) {
					PowerUpManager.Instance.Run ((UnoStruct.eColor)i, bFlg);
				}
			}
		}
	};

	tTurnData m_turnData;
	List<CDrowData> DrowDataList = new List<CDrowData> ();
	[SerializeField] float fDrowTwoTime = 10.0f;	 // ドロー倍率のかかる秒数
	[SerializeField] float fDrowFourTime = 10.0f;	 // ドロー倍率のかかる秒数
	[SerializeField] float fDrowTwoAmount = 1.2f;
	[SerializeField] float fDrowFourAmount = 1.4f;

	// スキル関係	
	public LeaderSkillAtack leaderSkillAtack { get; set; }
	public List<ContinueAtackUp> continueAtackUpList = new List<ContinueAtackUp> ();
	public List<CharaSkillBase> charaSkillList = new List<CharaSkillBase> ();
	public GameObject CharaSkillEffect = null; 

	[SerializeField]
	int nColorMagNum = 5;

	[SerializeField]
	float fColorMagnification = 2.0f;

	[SerializeField]
	float[] fMagnification;

	// PowerUpのBGM管理
	bool bOldBattle = false;

	void Awake() {
		if (this != Instance) {
			Destroy(this.gameObject);
			return;
		}
		leaderSkillAtack = null;
	}

	// Use this for initialization
	void Start () {
		AllReset ();
	}
	
	// Update is called once per frame
	void Update () {

		if (DrowDataList.Count > 0) {

			if (!BattleManager.Instance.GetIsInBattle ()) {
				SoundManager.Instance.PauseBGM (true);
				bOldBattle = BattleManager.Instance.GetIsInBattle ();
				return;
			}

			if (!bOldBattle) {
				SoundManager.Instance.PauseBGM (false);
			}
		}

		// ドロー系更新
		List<int> Destroy = new List<int>();
		for (int i = 0; i < DrowDataList.Count; i++) {
			DrowDataList[i].fLife -= Time.deltaTime;
			if (DrowDataList [i].fLife <= 0.0f) {
				Destroy.Add (i);
				DrowDataList [i].Run (false);
			} else {
				DrowDataList [i].Run (true);
			}
		}
		for (int i = 0; i < Destroy.Count; i++) {
			DrowDataList.RemoveAt(Destroy[i] - i);
		}

		if (Destroy.Count > 0 && DrowDataList.Count <= 0) {
			SoundManager.Instance.StopBGM (SoundManager.eBgmValue.BGM_ATACKUP);
		}
		bOldBattle = BattleManager.Instance.GetIsInBattle ();
	}

	public void Add(UnoStruct.tCard card) {

		// カードが追加された
		m_turnData.nSetCard++;

		// キャラカードかチェック
		List<PlayerCharactor> playerList = CardResource.Instance.GetPlayerList ();
		for (int i = 0; i < playerList.Count; i++) {
			UnoStruct.tCard charaCard = playerList[i].GetTCard();
			if (charaCard.m_Color == card.m_Color &&
			    charaCard.m_Number == card.m_Number) {
				GameObject Obj = (GameObject)Instantiate (CharaSkillEffect);
				Obj.GetComponent<CharaSkillManager> ().Set (charaSkillList [i]);
				break;
			}
		}

		switch(card.m_Number) {

		case UnoStruct.eNumber.NUMBER_WILD:
			break;

		// ドロー4処理
		case UnoStruct.eNumber.NUMBER_DROWFOUR:
			CDrowData drowFour = new CDrowData ();
			drowFour.DrowColor = card.m_Color;
			drowFour.fAmount = fDrowFourAmount;
			drowFour.fLife = fDrowFourTime;
            PowerUpAlpha.Instance.Reset();
            if (!SoundManager.Instance.NowOnBGM(SoundManager.eBgmValue.BGM_ATACKUP))
            {
                SoundManager.Instance.PlayBGM(SoundManager.eBgmValue.BGM_ATACKUP);
            }
			DrowDataList.Add (drowFour);
			break;

		// ドロー２処理
		case UnoStruct.eNumber.NUMBER_DROWTWO:
			CDrowData drowTwo = new CDrowData ();
			drowTwo.DrowColor = card.m_Color;
			drowTwo.fAmount = fDrowTwoAmount;
			drowTwo.fLife = fDrowTwoTime;
            PowerUpAlpha.Instance.Reset();
            if (!SoundManager.Instance.NowOnBGM(SoundManager.eBgmValue.BGM_ATACKUP))
            {
                SoundManager.Instance.PlayBGM(SoundManager.eBgmValue.BGM_ATACKUP);
            }
			DrowDataList.Add (drowTwo);
			break;

		// スキップ処理
		case UnoStruct.eNumber.NUMBER_SKIP:
			switch (card.m_Color) {

			case UnoStruct.eColor.COLOR_RED:
				m_turnData.Red.nSkip++;
				break;
			case UnoStruct.eColor.COLOR_BLUE:
				m_turnData.Blue.nSkip++;
				break;
			case UnoStruct.eColor.COLOR_GREEN:
				m_turnData.Green.nSkip++;
				break;
			case UnoStruct.eColor.COLOR_YELLOW:
				m_turnData.Yellow.nSkip++;
				break;
			}
			break;

		// リバース処理
		case UnoStruct.eNumber.NUMBER_REVERSE:
			ReverseEffectManager.Instance.Add ();
			switch (card.m_Color) {

			case UnoStruct.eColor.COLOR_RED:
				m_turnData.Red.nReverse++;
				break;
			case UnoStruct.eColor.COLOR_BLUE:
				m_turnData.Blue.nReverse++;
				break;
			case UnoStruct.eColor.COLOR_GREEN:
				m_turnData.Green.nReverse++;
				break;
			case UnoStruct.eColor.COLOR_YELLOW:
				m_turnData.Yellow.nReverse++;
				break;
			}
			break;
		
		// 数字処理
		case UnoStruct.eNumber.NUMBER_ZERO:
			float fNum = (float)Random.Range (1,10);
			switch (card.m_Color) {

			case UnoStruct.eColor.COLOR_RED:
				m_turnData.Red.nNum++;
				m_turnData.Red.fNumber += fNum;
				break;
			case UnoStruct.eColor.COLOR_BLUE:
				m_turnData.Blue.nNum++;
				m_turnData.Blue.fNumber += fNum;
				break;
			case UnoStruct.eColor.COLOR_GREEN:
				m_turnData.Green.nNum++;
				m_turnData.Green.fNumber += fNum;
				break;
			case UnoStruct.eColor.COLOR_YELLOW:
				m_turnData.Yellow.nNum++;
				m_turnData.Yellow.fNumber += fNum;
				break;
			}
			break;
			
		case UnoStruct.eNumber.NUMBER_ONE:
		case UnoStruct.eNumber.NUMBER_TWO:
		case UnoStruct.eNumber.NUMBER_THREE:
		case UnoStruct.eNumber.NUMBER_FOUR:
		case UnoStruct.eNumber.NUMBER_FIVE:
		case UnoStruct.eNumber.NUMBER_SIX:
		case UnoStruct.eNumber.NUMBER_SEVEN:
		case UnoStruct.eNumber.NUMBER_EIGHT:
		case UnoStruct.eNumber.NUMBER_NINE:
			switch (card.m_Color) {

			case UnoStruct.eColor.COLOR_RED:
				m_turnData.Red.nNum++;
				m_turnData.Red.fNumber += (float)card.m_Number;
				break;
			case UnoStruct.eColor.COLOR_BLUE:
				m_turnData.Blue.nNum++;
				m_turnData.Blue.fNumber += (float)card.m_Number;
				break;
			case UnoStruct.eColor.COLOR_GREEN:
				m_turnData.Green.nNum++;
				m_turnData.Green.fNumber += (float)card.m_Number;
				break;
			case UnoStruct.eColor.COLOR_YELLOW:
				m_turnData.Yellow.nNum++;
				m_turnData.Yellow.fNumber += (float)card.m_Number;
				break;
			}
			break;
		}
	}
		
	public tTurnData GetTurnData() { 
		tTurnData data = m_turnData;
		AllReset (); // 保存したので初期化
	
		// ドロー倍率
		for (int i = 0; i < DrowDataList.Count; i++) {
			switch (DrowDataList [i].DrowColor) {

			case UnoStruct.eColor.COLOR_RED:
				data.Red.fNumber *= DrowDataList[i].fAmount; 
				//Debug.Log ("Red : " + DrowDataList[i].fAmount);
				break;
			case UnoStruct.eColor.COLOR_BLUE:
				data.Blue.fNumber *= DrowDataList[i].fAmount; 
				//Debug.Log ("Blue : " + DrowDataList[i].fAmount);
				break;
			case UnoStruct.eColor.COLOR_GREEN:
				data.Green.fNumber *= DrowDataList[i].fAmount; 
				//Debug.Log ("Green : " + DrowDataList[i].fAmount);
				break;
			case UnoStruct.eColor.COLOR_YELLOW:
				data.Yellow.fNumber *= DrowDataList[i].fAmount; 
				//Debug.Log ("Yellow : " + DrowDataList[i].fAmount);
				break;
			case UnoStruct.eColor.COLOR_WILD:
				data.Red.fNumber *= DrowDataList[i].fAmount;
				data.Blue.fNumber *= DrowDataList[i].fAmount;
				data.Green.fNumber *= DrowDataList[i].fAmount;
				data.Yellow.fNumber *= DrowDataList[i].fAmount;
				//Debug.Log ("All : " + DrowDataList[i].fAmount);
				break;
			}
		}

		// リーダースキルの攻撃力アップ分を追加する
		/*if (leaderSkillAtack) {
			switch (leaderSkillAtack.SkillColor) {

			case UnoStruct.eColor.COLOR_RED:
				data.Red.fNumber *= leaderSkillAtack.fGetPercentage(); 
				break;
			case UnoStruct.eColor.COLOR_BLUE:
				data.Blue.fNumber *= leaderSkillAtack.fGetPercentage(); 
				break;
			case UnoStruct.eColor.COLOR_GREEN:
				data.Green.fNumber *= leaderSkillAtack.fGetPercentage(); 
				break;
			case UnoStruct.eColor.COLOR_YELLOW:
				data.Yellow.fNumber *= leaderSkillAtack.fGetPercentage(); 
				break;
			}
		}*/

		// コンティニューの攻撃力アップ分を追加する
		for (int i = 0; i < continueAtackUpList.Count; i++) {
			float fAmount = continueAtackUpList [i].AtackAmount;
			data.Red.fNumber *= fAmount;
			data.Blue.fNumber *= fAmount;
			data.Green.fNumber *= fAmount;
			data.Yellow.fNumber *= fAmount;
		}

		// リバースとスキップの合計を計算
		data.nReverseNum = data.Red.nReverse + data.Blue.nReverse + data.Green.nReverse + data.Yellow.nReverse;  
		data.nSkipNum = data.Red.nSkip + data.Blue.nSkip + data.Green.nSkip + data.Yellow.nSkip;

		if (data.nSkipNum > 0) {
			SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_SKIP);
		}
			
		// --- 各色毎の倍率チェック ----
		// Red
		if (data.Red.nNum >= nColorMagNum) {
			data.Red.fNumber *= fColorMagnification;
			//Debug.Log ("Red * " + fColorMagnification);
		}
		// Blue
		if (data.Blue.nNum >= nColorMagNum) {
			data.Blue.fNumber *= fColorMagnification;
			//Debug.Log ("Blue * " + fColorMagnification);
		}
		// Green
		if (data.Green.nNum >= nColorMagNum) {
			data.Green.fNumber *= fColorMagnification;
			//Debug.Log ("Green * " + fColorMagnification);
		}
		// Yellow
		if (data.Yellow.nNum >= nColorMagNum) {
			data.Yellow.fNumber *= fColorMagnification;
			//Debug.Log ("Yellow * " + fColorMagnification);
		}

		// --- カード合計枚数でチェック ---
		switch (data.nSetCard) {

		case 0:
			// カードなし
			break;

		case 1:
		case 2:
		case 3:
		case 4:
			SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_SETCARD_DO);
			//Debug.Log ("ド");
			break;

		case 5:
		case 6:
		case 7:
		case 8:
			SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_SETCARD_MI);
			//Debug.Log ("ミ");
			break;

		case 9:
		case 10:
		case 11:
		case 12:
			SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_SETCARD_SO);
			//Debug.Log ("ソ");
			break;

		case 13:
		case 14:
		case 15:
		case 16:
			SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_SETCARD_SI);
			//Debug.Log ("シ");
			break;
		}

		if (data.nSetCard > 0) {
			data.Red.fNumber *= fMagnification [data.nSetCard - 1];
			data.Blue.fNumber *= fMagnification [data.nSetCard - 1];
			data.Yellow.fNumber *= fMagnification [data.nSetCard - 1];
			data.Green.fNumber *= fMagnification [data.nSetCard - 1];
			//Debug.Log ("カード合計枚数 : " + data.nSetCard + ",倍率 : " + fMagnification[data.nSetCard - 1]);
		}

		return data;
	}

	public int CardAmount {
		get { return m_turnData.nSetCard; }
	}

	public void AllReset() {
		m_turnData.nSetCard = 0;
		m_turnData.nReverseNum = 0;
		m_turnData.nSkipNum = 0;
		m_turnData.Red.nNum = 0;
		m_turnData.Red.fNumber = 0;
		m_turnData.Red.nSkip = 0;
		m_turnData.Red.nReverse = 0;
		m_turnData.Blue.nNum = 0;
		m_turnData.Blue.fNumber = 0;
		m_turnData.Blue.nSkip = 0;
		m_turnData.Blue.nReverse = 0;
		m_turnData.Green.nNum = 0;
		m_turnData.Green.fNumber = 0;
		m_turnData.Green.nSkip = 0;
		m_turnData.Green.nReverse = 0;
		m_turnData.Yellow.nNum = 0;
		m_turnData.Yellow.fNumber = 0;
		m_turnData.Yellow.nSkip = 0;
		m_turnData.Yellow.nReverse = 0;
	} 

	public void AddContinueAtack(ContinueAtackUp continueAtackUp) {
		continueAtackUpList.Add (continueAtackUp);	
	}
}
