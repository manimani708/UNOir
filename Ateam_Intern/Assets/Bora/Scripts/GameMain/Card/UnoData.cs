using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnoData : MonoBehaviour {

	// 変数宣言
	UnoStruct.tCard m_Card;
	UnoData unoData = null;
	SpriteRenderer spriteRenderer = null;
	BoxCollider2D collider2D = null;
	[SerializeField]
	Vector3 InitPos = Vector3.zero;
	Vector3 InitScale = Vector3.zero;
	[SerializeField]
	bool bOn = false;
	[SerializeField]
	bool bClick = false;

	bool bSet = false;
	float fNowTime = 0.0f;
	float fSetTime = 0.15f; 
	float fReturnTime = 0.1f;
	public float fSwip = 1.0f; // カードのサイズを基準に判定

	// 不正解時の演出
	NotAnswer notAnswer = null;

	// コリジョンサイズ操作
	Vector2 colSize = Vector2.zero; 
	[Range(0.0f,1.0f)]
	const float fColChange = 0.4f;

	// 移動制限
	float fMoveRange = 1.5f;

	// Layer値
	const int nInitOrder = 6;

	// いけない所に行こうとしている時の演出
	static bool bRangeOut = false;

	#if DEBUG
	[SerializeField]
	string Color = "";
	[SerializeField]
	string Number = "";
	#endif

	public List<UnoData> AdjacentList = new List<UnoData> ();

	// 補助機能 
	const float fAlpha = 0.5f;

	// 
	public bool bCharaSkillEffect { get; private set; }
	float fCharaSkillEffectTime = 0.5f;
	Vector3 MaxScale = new Vector3 (5.0f,5.0f,0.0f); 

	// キャラカードエフェクト
	CharaSkillEffect charaSkillEffect = null;

	// カードが判定により自動で動いているか
	public bool bCardMove = false;
	Vector3 SetDistance = Vector3.zero;
	Vector3 ReturnDistance = Vector3.zero;

    // シャッフル中か
    CardShuffle cardShuffle = null;

	public bool OnTouch() {

		if (bClick || cardShuffle.bShuffle)
			return false;

		if (!FieldCard.Instance.Judge (m_Card)) { 
			notAnswer.Play ();
			spriteRenderer.sortingOrder = nInitOrder; 
			return false;
		}
			
		spriteRenderer.sortingOrder++;
		TrajectoryCreator.Instance.Create (InitPos);
		FieldCard.Instance.AddTemp (unoData);
		UnoCreateManager.Instance.TouchOnly (false);
		bClick = true;
		EventTrigger = true;
		for (int i = 0; i < AdjacentList.Count; i++) {
			AdjacentList [i].EventTrigger = true;
		}

		// SE再生
		if(TouchManager.Instance.feverGauge.isFeverMode) {
			SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_CARDPUSHFEVER);
		} else {
			SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_CARDPUSH);
		}

		return true;
	}

	public void OffTouch() {
		if (!bClick)
			return;

		bClick = false;
		collider2D.enabled = true;
		spriteRenderer.sortingOrder = nInitOrder; 
		fNowTime = 0.0f;
		bCardMove = true;
		UnoCreateManager.Instance.TouchOnly (true);
        bRangeOut = false; 
        SetDistance = FieldCard.Instance.transform.position - transform.position;

		//UnoData data = FieldCard.Instance.GetMostForwardCard (unoData);
		//if(data.GetInitPos.y + fSwip > data.transform.position.y || 
		if(!BattleManager.Instance.GetIsInBattle ()) {
			bSet = false;
			ReturnDistance = InitPos - transform.position;
			FieldCard.Instance.TempCnt++;
			return;
		}

		// セットできる
		bSet = true;
	}

	public void Init () {
		fNowTime = 0.0f;
		charaSkillEffect = GetComponentInChildren<CharaSkillEffect> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		collider2D = GetComponent<BoxCollider2D> ();
		InitPos = transform.position;
		InitScale = transform.localScale;
		Change ();
		notAnswer = transform.FindChild ("Not").GetComponent<NotAnswer> ();
		colSize = collider2D.size;
		unoData = GetComponent<UnoData> ();
		spriteRenderer.sortingOrder = nInitOrder;
		bCharaSkillEffect = false;
		EventTrigger = true;
        cardShuffle = gameObject.AddComponent<CardShuffle>();
	}
	
	void Update () {
		
		#if DEBUG
		Color = m_Card.m_Color.ToString();
		Number = m_Card.m_Number.ToString();
		#endif

		if (!FieldCard.Instance.Judge (m_Card)) {
			UnoCreateManager.Instance.NonSelect ();
		}

		if (bClick) {
			
			if (Application.platform == RuntimePlatform.Android) {
				if (!TouchManager.Instance.OnlyTouch (Input.GetTouch (0))) {
					return;
				}

				if (Input.GetTouch (0).phase == TouchPhase.Ended) {
					OffTouch ();
					return;
				}
			} else if (Input.GetMouseButtonUp (0)) {
				OffTouch ();
				return;
			}

			if (!BattleManager.Instance.GetIsInBattle ()) {
				OffTouch ();
			}

			if (FieldCard.Instance.GetMostForwardCard (unoData) == unoData) {
				Vector3 pos = Vector3.zero;
				if (Application.platform == RuntimePlatform.Android) {
					pos = Input.GetTouch (0).position;
				} else {
					pos = Input.mousePosition;
				}
				pos.z = 10.0f;

				bRangeOut = false;
				Vector3 temp = Camera.main.ScreenToWorldPoint (pos);
				temp.z = InitPos.z;
				if (temp.x - InitPos.x > fMoveRange) {
					temp.x = InitPos.x + fMoveRange;
					bRangeOut = true;
				} else if(temp.x - InitPos.x < -fMoveRange) {
					temp.x = InitPos.x - fMoveRange;
					bRangeOut = true;
				}

				if (temp.y - InitPos.y > fMoveRange) {
					temp.y = InitPos.y + fMoveRange;
					bRangeOut = true;
				} else if(temp.y - InitPos.y < -fMoveRange) {
					temp.y = InitPos.y - fMoveRange;
					bRangeOut = true;
				}

				// 座標格納
				transform.position = temp;
				//transform.position = InitPos + Vector3.ClampMagnitude (temp - InitPos, fMoveRange);
			} else {
				transform.position = FieldCard.Instance.GetMostForwardCard (unoData).transform.position;
			}

		} else if (bCardMove) {
			fNowTime += Time.deltaTime;

			if (!bSet) {
				if (fNowTime < fReturnTime) {
					transform.position += ReturnDistance * (Time.deltaTime / fReturnTime);
				} else {
					transform.position = InitPos;
					bCardMove = false;
				}
			} else {
				if (fNowTime < fSetTime) { 
					transform.position += SetDistance * (Time.deltaTime / fSetTime);
				} else {
					transform.position = InitPos;
					bCardMove = false;
					ChangeCard ();
				}
			}
		} else {
			if (!collider2D.enabled) {
				collider2D.enabled = true;
				//Debug.Log (transform.name);
			}
		}

		// 補助機能オンかどうか
		if (FieldCard.Instance.GetTempFlg () && !FieldCard.Instance.Judge (m_Card)) {
			spriteRenderer.color = new Color (fAlpha, fAlpha, fAlpha, 1);
		} else if (bRangeOut && !EventTrigger) {
			spriteRenderer.color = new Color (fAlpha, fAlpha, fAlpha, 1);
		} else {
			spriteRenderer.color = new Color(1,1,1,1);
		}

		// キャラスキルエフェクト発動中？
		if (bCharaSkillEffect) {
			transform.localScale -= (MaxScale - InitScale) * (Time.deltaTime / fCharaSkillEffectTime);

			if (transform.localScale.x <= InitScale.x) {
				bCharaSkillEffect = false;
				transform.localScale = InitScale;
			}
		}
	}
		
	void LateUpdate() {
		if (!bCharaSkillEffect)
			return;

		if (GameController.Instance.bStop) {
			spriteRenderer.enabled = false;
		} else {
			spriteRenderer.enabled = true;
		}
	}

	public void Change() {
		m_Card = UnoCreateManager.Instance.GetLotteryCardData (transform.position);
		int nNumber = ((int)m_Card.m_Color * (int)UnoStruct.eNumber.NUMBER_MAX) + (int)m_Card.m_Number;
		spriteRenderer.sprite = CardResource.Instance.GetCardResource(nNumber);
		charaSkillEffect.RunCheck (m_Card);
	}

	// Debug用カード変更コマンド
	public void DebugCard(UnoStruct.tCard card) {
		m_Card = card;
		int nNumber = ((int)m_Card.m_Color * (int)UnoStruct.eNumber.NUMBER_MAX) + (int)m_Card.m_Number;
		spriteRenderer.sprite = CardResource.Instance.GetCardResource(nNumber);
		charaSkillEffect.RunCheck (m_Card);
	}

	void ChangeCard() {
		
		TurnData.Instance.Add (m_Card);

		// フィーバーにはいったかどうかでランダムかどうか判定
		if (!FieldCard.Instance.Change (unoData)) {
			m_Card = UnoCreateManager.Instance.GetLotteryCardData (transform.position);
		} else {
			List<PlayerCharactor> CharaList = CardResource.Instance.GetPlayerList ();
			UnoStruct.tCard card;
			if (m_Card.m_Color != UnoStruct.eColor.COLOR_WILD) {
				card = m_Card;
			} else {
				card = GameMainUpperManager.instance.GetLeaderTCard();
			}
			for (int i = 1; i < CharaList.Count; i++) {
				if ((int)CharaList [i].GetTCard ().m_Color == (int)card.m_Color) {
					m_Card = CharaList [i].GetTCard ();
					break;
				}
			}
			SetCharaSkillEffect ();
		}

		int nNumber = ((int)m_Card.m_Color * (int)UnoStruct.eNumber.NUMBER_MAX) + (int)m_Card.m_Number;
		spriteRenderer.sprite = CardResource.Instance.GetCardResource(nNumber);
		charaSkillEffect.RunCheck (m_Card);
	}

	void SetCharaSkillEffect() {
		// エフェクト生成
		bCharaSkillEffect = true;
		FeverSetEffect.Instance.CreateFeverSetEffect(m_Card.m_Color, transform.position);
		transform.localScale = MaxScale;
		spriteRenderer.enabled = false;
	}

    public void Shuffle()
    {
        cardShuffle.Shuffle();
    }

	public UnoStruct.tCard CardData { 
		get { return m_Card; }
		set { m_Card = value; }
	}

	public Vector3 GetInitPos { 
		get { return InitPos; }
	}

	public bool EventTrigger {
		get { return bOn; }
		set { bOn = value; }
	}

	public bool Click {
		get { return bClick; }
		set { bClick = value; }
	}

	public Collider2D Collider {
		get { return collider2D; }
	}

	public void CollisionChange(bool bAdd) {
		if (bAdd) {
			collider2D.size = colSize;
		} else {
			collider2D.size = colSize * fColChange;
		}
	}

	public void ColSet() {

		collider2D.enabled = false;
		//Debug.Log (transform.name);
	}

	public int GetOreder() {
		return spriteRenderer.sortingOrder;
	}

	void OnTriggerEnter2D(Collider2D col) {

		if (GameController.Instance.bStop && !BattleManager.Instance.GetIsInBattle())
			return;

		if (bClick || col.tag != "Card" || !EventTrigger)
			return;

		if (Application.platform == RuntimePlatform.Android) {
			if (Input.touchCount <= 0)
				return;

			if (!TouchManager.Instance.OnlyTouch (Input.GetTouch (0)))
				return;

			if(Input.GetTouch (0).phase == TouchPhase.Began || Input.GetTouch (0).phase == TouchPhase.Ended) {
				return;
			}

		} else {
			if(!Input.GetMouseButton (0))
				return;
		}

		if (FieldCard.Instance.TempList.Count <= 0)
			return;

		UnoData data = col.GetComponent<UnoData> ();

		if (data.bCharaSkillEffect)
			return;

		if (FieldCard.Instance.bNotSet)
			return;

		int nCnt = 0;
		for (nCnt = 0; nCnt < data.AdjacentList.Count; nCnt++) {
			if (unoData != data.AdjacentList [nCnt])
				continue;

			break;
		}

		if (nCnt == data.AdjacentList.Count)
			return;
		
		// 判定
		if (!OnTouch ())
			return;

		data.ColSet ();
		spriteRenderer.sortingOrder = data.GetOreder() + 1;
		//Debug.Log (transform.name + transform.position + "," + col.name + col.transform.position);
	}
}
