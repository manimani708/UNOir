using UnityEngine;
using System.Collections;

public class CharaSkillEffectData : MonoBehaviour {
	
	static CharaSkillEffectData instance;

	public static CharaSkillEffectData Instance {
		get {
			if (instance == null) {
				instance = (CharaSkillEffectData)FindObjectOfType(typeof(CharaSkillEffectData));

				if (instance == null) {
					Debug.LogError("CharaSkillEffectData Instance Error");
				}
			}
			return instance;
		}
	}

	static Sprite[] effectSprite = new Sprite[5]; 

	bool bAdd = true;
	public Vector3 InitScale { get; private set; }
	float fTime = 0.2f;
	Vector3 AddScale = new Vector3(0.1f, 0.1f, 0.1f);
	public float fAddAlpha { get; private set; } 

	public Color nowColor { get; private set; }
	public Vector3 nowScale { get; private set; }

	void Awake() {
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}
		fAddAlpha = 1.0f;
		nowColor = new Color (1, 1, 1, 1.0f); // - fAddAlpha);
		//InitScale = nowScale = new Vector3 (0.95f,0.95f,0.95f);
		InitScale = nowScale = new Vector3 (1.0f,1.0f,1.0f);
	}

	void Update() {
		
		if (bAdd) {
			//nowScale += AddScale * (Time.deltaTime / fTime);
			nowColor += new Color (0,0,0, fAddAlpha * (Time.deltaTime / fTime));

			if(nowColor.a >= 1.0f) {
			//if (nowScale.x >= InitScale.x + AddScale.x) {
				bAdd = false;
			}

		} else {
			//nowScale -= AddScale * (Time.deltaTime / fTime);
			nowColor -= new Color (0,0,0, fAddAlpha * (Time.deltaTime / fTime));

			if(nowColor.a <= 1.0f - fAddAlpha) {
			//if (nowScale.x <= InitScale.x) {
				bAdd = true;
			}
		}
	}

	public Sprite GetSprite(int n) {

		if(!effectSprite[0]) {
			effectSprite = ResourceHolder.Instance.GetResource (ResourceHolder.eResourceId.ID_CHARASKILLCARDEFFECT);
			//effectSprite = Resources.LoadAll<Sprite> ("Textures/CharaSkill/CharaSkillCard");
		}

		int nNum = 0;
		switch (n) {
		case 0:
			nNum = 4;
			break;
		case 1:
			nNum = 0;
			break;
		case 2:
			nNum = 1;
			break;
		case 3:
			nNum = 3;
			break;
		case 4:
			nNum = 2;
			break;
		}

		return effectSprite [nNum];
	}
}
