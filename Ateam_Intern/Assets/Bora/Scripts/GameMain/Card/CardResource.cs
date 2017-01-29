using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardResource : MonoBehaviour {

	static CardResource instance;

	public static CardResource Instance {
		get {
			if (instance == null) {
				instance = (CardResource)FindObjectOfType(typeof(CardResource));

				if (instance == null) {
					Debug.LogError("CardResource Instance Error");
				}
			}

			return instance;
		}
	}
		
	Dictionary<string, Sprite> cardResource = new Dictionary<string, Sprite>();
	private List<PlayerCharactor> CharaList = new List<PlayerCharactor> ();

	void Awake() {
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}
		Sprite[] spriteAll = ResourceHolder.Instance.GetResource (ResourceHolder.eResourceId.ID_CARD);
		//Sprite[] spriteAll = Resources.LoadAll<Sprite> ("Textures/Card/Card");
		for(int i = 0; i < spriteAll.Length; i++) {
			cardResource.Add (spriteAll[i].name, spriteAll[i]);
		}
	}

	public void SetCharaCard() {
		CharaList = GameMainUpperManager.instance.charactorAndFriend;
		for(int i = 0; i < CharaList.Count; i++) {
			UnoStruct.tCard card = CharaList [i].GetTCard(); 
			int nNumber = ((int)CharaList[i].attribute * (int)UnoStruct.eNumber.NUMBER_MAX) + (int)CharaList[i].cardNum;
			string key = "Card_" + nNumber.ToString();
			cardResource[key] = CharaList [i].noFrameSprite;
			//Debug.Log (cardResource[key].ToString() + "," + card.m_Color.ToString() + "," + card.m_Number.ToString());
		}
	}

	public Sprite GetCardResource(int nNumber) {
		
		Sprite sprite = null;
		string key = "Card_" + nNumber.ToString();
		cardResource.TryGetValue (key, out sprite);

		return sprite;
	}

	public List<PlayerCharactor> GetPlayerList() {
		return CharaList;
	}
}
