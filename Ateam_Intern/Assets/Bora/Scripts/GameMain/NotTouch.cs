using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotTouch : MonoBehaviour {

	public GameObject hpGauge = null; 
	SpriteRenderer spriteRenderer = null; 
	public float fAlpha = 0.5f;

	bool bOldInBattle = true;
	bool bOldStop = true;

	[SerializeField]
	List<SpriteRenderer> rendererList = new List<SpriteRenderer>();

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Update() {

		if(rendererList.Count <= 0) {
			for (int i = 0; i < hpGauge.transform.childCount; i++) {
				Transform trans = hpGauge.transform.GetChild (i);

				if (trans.tag == "Number") {
					for (int j = 0; j < trans.childCount; j++) {
						rendererList.Add (trans.GetChild(j).GetComponent<SpriteRenderer>());
					}
				} else {
					rendererList.Add (trans.GetComponent<SpriteRenderer>());
				}
			}
		}

		bool bNowInBattle = BattleManager.Instance.GetIsInBattle ();
		bool bNowStop = GameController.Instance.bStop;

		if ((bOldInBattle && !bNowInBattle) || 
			(!bOldStop && bNowStop)) {
			spriteRenderer.color = new Color (0, 0, 0, fAlpha);

			for (int i = 0; i < rendererList.Count; i++) {
				rendererList [i].sortingLayerName = "NotTouch";
			}
		} else if ((!bOldInBattle && bNowInBattle) || 
					(bOldStop && !bNowStop)) {
			spriteRenderer.color = new Color (0, 0, 0, 0);

			for (int i = 0; i < rendererList.Count; i++) {
				rendererList [i].sortingLayerName = "Gauge";
			}
		}

		bOldInBattle = bNowInBattle;
		bOldStop = bNowStop;
	}
}
