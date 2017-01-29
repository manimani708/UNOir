using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour {

	[SerializeField] float fTime = 3.0f;

	[SerializeField] SpriteRenderer render = null;
	[SerializeField] float fRotAmount = 1.0f;
	[SerializeField] float fMoveAmount = -10.0f; 
	[SerializeField] float fMinScale = 0.2f;
	[SerializeField] float fMaxScale = 0.5f;

	Vector3 InitScale = Vector3.zero;

	void Start() {
		float fRandom = Random.Range (fMinScale, fMaxScale);
		transform.localScale = new Vector3 (fRandom, fRandom, 0.0f);
		InitScale = transform.localScale;
	}

	void Update () {

		if (render.color.a <= 0.0f) {
		//if(transform.localScale.x <= 0.0f) {
			Destroy (this.gameObject);
			return;
		}

		transform.position += new Vector3(0,fMoveAmount,0) * (Time.deltaTime / fTime);
		transform.eulerAngles += new Vector3(0,0, (360 * fRotAmount) * (Time.deltaTime / fTime));
		//transform.localScale -= InitScale * (Time.deltaTime / fTime);
		render.color -= new Color (0,0,0, 1.0f * (Time.deltaTime / fTime));
	}
}
