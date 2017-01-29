using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {

	[SerializeField] float fTime = 3.0f;
	float fNowDelay = 0.0f;
	[SerializeField] float fDealy = 1.0f;
	[SerializeField] float fEndAlpha = 0.75f;

	[SerializeField] GameObject Right = null;
	[SerializeField] GameObject Left = null;
	[SerializeField] SpriteRenderer Sky = null;
	[SerializeField] SpriteRenderer Tree = null;

	[SerializeField] Vector3 CroudMoveAmount = new Vector3(8.0f,0.0f,0.0f);
	[SerializeField] FlowerCreator flowerCreator = null; 

	bool bEnd = false;


	// Use this for initialization
	void Awake () {
		Sky.color  = new Color (1,1,1,0);
		Tree.color = new Color (1,1,1,0);
		flowerCreator.enabled = false;
		flowerCreator.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (bEnd)
			return;

		Right.transform.position += CroudMoveAmount * (Time.deltaTime / fTime);
		Left.transform.position  -= CroudMoveAmount * (Time.deltaTime / fTime);
		Sky.color  += new Color (0,0,0,1) * (Time.deltaTime / fTime);
		Tree.color += new Color (0,0,0,1) * (Time.deltaTime / fTime);

		if (Sky.color.a >= fEndAlpha) {
			if (fNowDelay <= 0.0f) {
				flowerCreator.enabled = true;
				flowerCreator.gameObject.SetActive (true);
			}
			fNowDelay += Time.deltaTime;

			if (fNowDelay >= fDealy + (1.0f - fEndAlpha)) {
				bEnd = true;
				//SoundManager.Instance.PlayBGM (SoundManager.eBgmValue.BGM_WIN);
			}
		}
	}

	public bool End {
		get { return bEnd; }
	}
}
