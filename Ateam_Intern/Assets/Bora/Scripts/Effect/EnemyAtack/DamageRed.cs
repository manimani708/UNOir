using UnityEngine;
using System.Collections;

public class DamageRed : MonoBehaviour {

	static DamageRed instance;

	public static DamageRed Instance {
		get {
			if (instance == null) {
				instance = (DamageRed)FindObjectOfType(typeof(DamageRed));

				if (instance == null) {
					Debug.LogError("DamageRed Instance Error");
				}
			}
			return instance;
		}
	}

	bool bRun = false;

	public float fMaxAlpha = 0.25f;
	public float fTime = 0.1f;
	public SpriteRenderer spriteRenderer = null;
	bool bAdd = true;
	Color InitColor;

	void Awake() {
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}
		InitColor = spriteRenderer.color;
	}
	
	// Update is called once per frame
	void Update () {

		/*#if DEBUG
		if(Input.GetKeyDown(KeyCode.A)) {
			Run();
		}
		#endif*/
	
		if (!bRun) 
			return;

		if (bAdd) {
			spriteRenderer.color += new Color (0, 0, 0, fMaxAlpha * (Time.deltaTime / fTime));

			if (spriteRenderer.color.a >= fMaxAlpha) {
				bAdd = false;
				Color color = InitColor;
				color.a = fMaxAlpha;
				spriteRenderer.color = color;
			}
		} else {
			spriteRenderer.color -= new Color (0, 0, 0, fMaxAlpha * (Time.deltaTime / fTime));

			if (spriteRenderer.color.a <= 0.0f) {
				bAdd = true;
				Color color = InitColor;
				color.a = 0.0f;
				spriteRenderer.color = color;
				bRun = false;
			}
		}
	}

	public void Run() {

		if (bRun)
			return;

		bRun = true;
		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_DAMAGERED);
	}
}
