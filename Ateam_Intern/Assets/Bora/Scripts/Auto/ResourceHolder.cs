using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceHolder : MonoBehaviour {
	
	private static ResourceHolder instance;

	public static ResourceHolder Instance {
		get {
			if (instance == null) {
				instance = (ResourceHolder)FindObjectOfType(typeof(ResourceHolder));

				if (instance == null) {
					Debug.LogError(typeof(ResourceHolder) + "is nothing");
				}
			}

			return instance;
		}
	}
		
	// リソースID
	public enum eResourceId {

		ID_CHARASKILLCARD = 0,
		ID_CUTIN,
		ID_ATACKEFFECT,
		ID_DROWEFFECT,
		ID_FEVERNUM,
		ID_CARD,
		ID_NUMBER,
		ID_HP_NUMBER,
        ID_CHARAWAKU,
        ID_FEVERROGO,
        ID_UNOFEVEROGO,
        ID_POWERUP,
        ID_HPGAUGE,
        ID_CHARASKILLCARDEFFECT,
		ID_MAX,
	};

	List<Sprite[]> resourceAll = new List<Sprite[]> ();

	public void Awake() {
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}

		DontDestroyOnLoad (this.gameObject);

		if (resourceAll.Count > 0) {
			return;
		}

		resourceAll.Add (Resources.LoadAll<Sprite> ("Textures/CharaSkill/CharaSkillCard"));
		resourceAll.Add (Resources.LoadAll<Sprite> ("Textures/Effect/CharaSkill/CutIn"));
		resourceAll.Add (Resources.LoadAll<Sprite> ("Textures/Effect/PlayerAtack/atack_effect"));
		resourceAll.Add (Resources.LoadAll<Sprite> ("Textures/Effect/SpecialCard/Drow/DrowEffect"));
		resourceAll.Add (Resources.LoadAll<Sprite> ("Textures/Fever/feverNum"));
		resourceAll.Add (Resources.LoadAll<Sprite> ("Textures/Card/Card"));
		resourceAll.Add (Resources.LoadAll<Sprite> ("Textures/Number/number"));
		resourceAll.Add (Resources.LoadAll<Sprite> ("Textures/Number/hp_number"));
        resourceAll.Add (Resources.LoadAll<Sprite> ("Textures/PlayerCharactor/Charawaku_all"));
        resourceAll.Add (Resources.LoadAll<Sprite> ("Textures/Fever/Fever"));
        resourceAll.Add (Resources.LoadAll<Sprite> ("Textures/Fever/UnoFever"));
        resourceAll.Add (Resources.LoadAll<Sprite> ("Textures/Effect/SpecialCard/Drow/powerup"));
        resourceAll.Add (Resources.LoadAll<Sprite> ("Textures/Gauge/player_hp_Gauge"));
        resourceAll.Add (Resources.LoadAll<Sprite> ("Textures/CharaSkill/CharaSkillCardEffect"));
	}

	public Sprite[] GetResource(eResourceId id) {
		return resourceAll[(int)id];
	}
}
