using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrowEffect : MonoBehaviour {

	UnoData unoData = null;
	SpriteRenderer render = null;

	//float fChangeTime = 0.5f; 
	Vector3 InitScale = Vector3.zero;

	static List<Sprite> spriteList = new List<Sprite> ();

	void Awake() {
		if (spriteList.Count <= 0) {

			Sprite[] all = ResourceHolder.Instance.GetResource (ResourceHolder.eResourceId.ID_DROWEFFECT);
			//Sprite[] all = Resources.LoadAll<Sprite> ("Textures/Effect/SpecialCard/Drow/DrowEffect");

			for (int i = 0; i < all.Length; i++) {
				spriteList.Add(all [i]);
			}
		}
	}

	void Start () {
		render = GetComponent<SpriteRenderer> ();
		render.sortingOrder = -2;
		unoData = GetComponentInParent<UnoData> ();
		transform.localScale = InitScale = new Vector3(1.1f,1.1f,1.0f);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
	}

	void Update () {
		
		// sprite更新
		render.sprite = spriteList [(int)unoData.CardData.m_Color];

		if (DrowEffectManager.Instance.GetUse (unoData.CardData.m_Color) ) {
			render.color = new Color(1,1,1,DrowAlpha.Instance.a);
		} else {
			render.color = new Color(1,1,1,0);
		}
	}
}
