using UnityEngine;
using System.Collections;

public class DeathRed : MonoBehaviour {

	public bool bEnd = false; 
	bool bRun = false;

	public float fMaxAlpha = 0.5f;
	public float fTime = 1.0f;
	public SpriteRenderer spriteRenderer = null;
	Color InitColor;
	bool bAdd = true;

	void Awake() {
		InitColor = spriteRenderer.color;
	}

	// Update is called once per frame
	void Update () {

		if (!bRun) 
			return;

		if (bAdd) {
			spriteRenderer.color += new Color (0, 0, 0, fMaxAlpha * (Time.deltaTime / fTime));

			if (spriteRenderer.color.a >= fMaxAlpha) {
				bRun = false;
				bEnd = true;
			}
		} else {
			spriteRenderer.color -= new Color (0, 0, 0, fMaxAlpha * (Time.deltaTime / (fTime / 2.0f)));

			if (spriteRenderer.color.a <= 0.0f) {
				bRun = false;
				bEnd = true;
				//spriteRenderer.enabled = false;
			}
		}
	}

	public void Run() {
		bRun = true;
		bAdd = true;
		bEnd = false;
		spriteRenderer.enabled = true;
		spriteRenderer.color = new Color (1,1,1,0);
		SoundManager.Instance.PauseBGM (true);
		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_LOSE);
	}

	public void End() {
		bRun = true;
		bAdd = false;
		bEnd = false;
		SoundManager.Instance.PauseBGM (false);
		SoundManager.Instance.StopBGM (SoundManager.eBgmValue.BGM_GAMEOVER);
	}
}
