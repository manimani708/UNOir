using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchManager : MonoBehaviour {

	static TouchManager instance;

	public static TouchManager Instance {
		get {
			if (instance == null) {
				instance = (TouchManager)FindObjectOfType(typeof(TouchManager));

				if (instance == null) {
					Debug.LogError("TouchManager Instance Error");
				}
			}
			return instance;
		}
	}

	bool bTouch = false;
	public int nTouchId { get; private set; }

	[SerializeField]
	FeverGauge fever = null;
	public FeverGauge feverGauge { get { return fever; } private set { fever = value; } }

	void Awake() {
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}
		nTouchId = 0;
	}

	// Update is called once per frame
	void Update () {

		// タッチオフ
		bTouch = false;

		Vector2 pos = Vector2.zero;
		if (Application.platform == RuntimePlatform.Android) {

			if (Input.touchCount > 0) {
				if (Input.GetTouch (0).phase == TouchPhase.Began) {
					pos = Input.GetTouch (0).position;
					bTouch = true;
					nTouchId = Input.GetTouch (0).fingerId;
					//FieldCard.Instance.TempList.Clear ();
				} else if (Input.GetTouch (0).phase == TouchPhase.Ended) {
					List<UnoData> list = UnoCreateManager.Instance.GetCardList();
					for(int i = 0; i < list.Count; i++) {
						list [i].CollisionChange (true);
					}
				}
			}

		} else {
			
			if (Input.GetMouseButtonDown (0)) {
				pos = Input.mousePosition;
				bTouch = true;
			} else if (Input.GetMouseButtonUp(0)) {
				List<UnoData> list = UnoCreateManager.Instance.GetCardList();
				for(int i = 0; i < list.Count; i++) {
					list [i].CollisionChange (true);
				}
			}
		}

		// カットインなどの場合はこれ以上はいかない
		if (GameController.Instance.bStop)
			return;
		
		if(!BattleManager.Instance.GetIsInBattle())
			return;

		if (!bTouch)
			return;

		Vector2 Point = Camera.main.ScreenToWorldPoint (pos);
		Collider2D collider2D = Physics2D.OverlapPoint (Point);

		if (!collider2D)
			return;

		if (collider2D.tag == "Enemy") {
			Enemy enemy = collider2D.gameObject.GetComponent<Enemy> ();
			GameMainUpperManager.instance.player.ChangeTargetEnemy (enemy);

		} else {
			UnoData data = collider2D.gameObject.GetComponent<UnoData> ();

			if (FieldCard.Instance.bNotSet)
				return;

			if (data.bCharaSkillEffect)
				return;

			if (data.bCardMove)
				return;

			data.OnTouch ();
			List<UnoData> list = UnoCreateManager.Instance.GetCardList ();
			for (int i = 0; i < list.Count; i++) {
				list [i].CollisionChange (false);
			}
		}
	}

	public bool Touch {
		get { return bTouch; }
	}

	public bool TouchCheck() {

		if (GameController.Instance.bStop)
			return false;

		bool bNowTouch = false;

		if (Application.platform == RuntimePlatform.Android) {
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
				bNowTouch = true;
			}
		} else {
			if (Input.GetMouseButtonDown (0)) {
				bNowTouch = true;
			}
		}

		return bNowTouch;
	}

	public bool OnlyTouch(Touch touch) {
		return touch.fingerId == nTouchId;
	}
}
