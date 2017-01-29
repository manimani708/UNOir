using UnityEngine;
using System.Collections;

public class FeverRogo : MonoBehaviour {

    [SerializeField]
    bool bUnoFever = false;

    float fNowChangeTime = 0.0f;
    [SerializeField]
   float fChangeTime = 0.1f;

	[SerializeField]
	float fTime = 0.75f;
	Vector3 MaxScale = Vector3.zero;

	[SerializeField]
	float fFadeTime = 0.5f;

	[SerializeField]
	Vector3 FadeAddScale = new Vector3(0.2f, 0.2f, 0.0f);

	int nChangeCnt = 0;

	SpriteRenderer renderer = null;
    Sprite[] tempSpriteAll = new Sprite[4];

	// Use this for initialization
	void Start () {
		MaxScale = transform.localScale;
		transform.localScale = Vector3.zero;

		renderer = GetComponent<SpriteRenderer> ();

		// 仕様変更
        if (!bUnoFever) {
            tempSpriteAll = ResourceHolder.Instance.GetResource(ResourceHolder.eResourceId.ID_FEVERROGO);
        }
        else {
            tempSpriteAll = ResourceHolder.Instance.GetResource(ResourceHolder.eResourceId.ID_UNOFEVEROGO);
        } 
        renderer.sprite = tempSpriteAll[nChangeCnt];
	}

	// Update is called once per frame
	void Update () {

        fNowChangeTime += Time.deltaTime;
        if (fNowChangeTime >= fChangeTime)
        {
            nChangeCnt = (nChangeCnt + 1) % tempSpriteAll.Length;
            renderer.sprite = tempSpriteAll[nChangeCnt];
            fNowChangeTime = 0.0f;
        }

		if (transform.localScale.x < MaxScale.x) {
			transform.localScale += MaxScale * (Time.deltaTime / fTime);
		} else {
			renderer.color -= new Color (0, 0, 0, 1.0f * (Time.deltaTime / fFadeTime));
			transform.localScale += FadeAddScale * (Time.deltaTime / fFadeTime);

			if (renderer.color.a <= 0.0f) {
				Destroy (this.gameObject);
			}
		}
	}
}
