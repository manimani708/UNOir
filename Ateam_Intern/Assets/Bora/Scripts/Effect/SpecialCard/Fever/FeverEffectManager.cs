using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FeverEffectManager : MonoBehaviour {

	static FeverEffectManager instance;

	public static FeverEffectManager Instance {
		get {
			if (instance == null) {
				instance = (FeverEffectManager)FindObjectOfType(typeof(FeverEffectManager));

				if (instance == null) {
					Debug.LogError("FeverEffectManager Instance Error");
				}
			}
			return instance;
		}
	}

	List<ParticleSystem> ParticleList = new List<ParticleSystem>();
	public FeverGauge feverGauge = null;
	bool bOldFeverFlg = false;

	int nOldFeverPoint = 0;
	Sprite[] feverNumSprite = new Sprite[3];

	[SerializeField]
	SpriteRenderer feverNum = null;
	Vector3 InitfeverNumScale = Vector3.zero;

	void Awake() {
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}

		for (int i = 0; i < transform.childCount - 1; i++) {
			ParticleList.Add (transform.GetChild(i).GetComponent<ParticleSystem>());
			ParticleList [i].loop = false;
			ParticleList [i].Stop();
		}

		InitfeverNumScale = feverNum.transform.localScale;
		feverNumSprite = ResourceHolder.Instance.GetResource (ResourceHolder.eResourceId.ID_FEVERNUM);
		//feverNumSprite = Resources.LoadAll<Sprite> ("Textures/Fever/feverNum");
	}

	void Update() {

		if (bOldFeverFlg != feverGauge.isFeverMode) {
			Run (feverGauge.isFeverMode);
		} 
		if (feverGauge.isFeverMode) {

			int nAs = feverGauge.feverPointMax / (int)feverGauge.FeverTime; 

			if (!BattleManager.Instance.GetIsInBattle ()) {
				feverNum.enabled = false;
				nOldFeverPoint = feverGauge.feverPoint / nAs;
				return;
			} else {
				feverNum.enabled = true;
			}

			if (feverGauge.feverPoint <= 2 * nAs) {
				if (nOldFeverPoint != feverGauge.feverPoint / nAs) {
					feverNum.sprite = feverNumSprite[feverGauge.feverPoint / nAs]; 
					feverNum.transform.localScale = InitfeverNumScale;
					feverNum.color = new Color (1,1,1,1);
				}
				feverNum.transform.localScale -= InitfeverNumScale * Time.deltaTime;
				//feverNum.color -= new Color (0,0,0, Time.deltaTime); 
			}
			nOldFeverPoint = feverGauge.feverPoint / nAs;
		}

		bOldFeverFlg = feverGauge.isFeverMode;
	}

	public bool GetFever() {
		return ParticleList [0].loop;
	}

	public void Run(bool bRun) {

		if (bRun) {
			
			for (int i = 0; i < ParticleList.Count; i++) {
				ParticleList [i].Play ();
				ParticleList [i].loop = true;
			}
			feverNum.enabled = true;
		} else {

			for (int i = 0; i < ParticleList.Count; i++) {
				ParticleList [i].loop = false;
			}
			feverNum.color = new Color (1,1,1,0);
			feverNum.enabled = false;
		}
	}
}
