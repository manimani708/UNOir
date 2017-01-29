using UnityEngine;
using System.Collections;

public class WhirlWind : EffectBase {

	bool bRun = false;
	SpriteRenderer render = null;
	Vector3 Distance = Vector3.zero;

	public float fTime = 0.75f;
	public Vector3 TargetPos = Vector3.zero;
	
	// Use this for initialization
	void Start () {
		render = GetComponent<SpriteRenderer> ();
		Distance = TargetPos - transform.position;
	}

	// Update is called once per frame
	void Update () {

		if (!bRun || bEnd)
			return;

		render.color -= new Color (0, 0, 0, 1.0f * (Time.deltaTime / fTime));
		transform.position += Distance * (Time.deltaTime / fTime);

		if (render.color.a <= 0.0f) {
			bEnd = true;
		}
	}

	public void Run() {
		bRun = true;
	}
}
