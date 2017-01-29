using UnityEngine;
using System.Collections;

public class FlowerCreator : MonoBehaviour {

	[SerializeField] GameObject flower = null;
	[SerializeField] float fInterval = 0.5f;
	[SerializeField] Vector2 Range = new Vector3(5.0f,1.0f);
	[SerializeField] Sprite[] spriteList;

	float fNowInterval = 0.0f;

	void Start() {
		fNowInterval = fInterval;
	}

	void Update () {

		fNowInterval += Time.deltaTime;

		if (fNowInterval >= fInterval) {
			GameObject temp = (GameObject)Instantiate (flower, transform.position, Quaternion.identity);
			temp.transform.position += new Vector3 (Random.Range(-Range.x, Range.x), Random.Range(-Range.y, Range.y), 0);
			temp.GetComponent<SpriteRenderer> ().sprite = spriteList [Random.Range (0, spriteList.Length)];
			fNowInterval = 0.0f;
		}
	}
}
