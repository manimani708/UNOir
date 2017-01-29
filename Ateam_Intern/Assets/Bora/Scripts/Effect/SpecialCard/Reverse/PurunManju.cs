using UnityEngine;
using System.Collections;

public class PurunManju : MonoBehaviour {

	public float Counter { get; private set; }

	public float Deviation = 50f;
	float InitDeviation = 0f;
	public float SubDev = 0f;
	public float XPuruPuru = 5.0f;
	public float Cycle = 1f;
	Vector3 InitScale = Vector3.zero;
	public bool Waver = true;


	void Start () {
		Counter = 0.0f;
		InitScale = transform.localScale;
		InitDeviation = Deviation;

		if (Waver)
			Deviation = 0f;
	}
		
	void Update () {
		float radius = Mathf.PI * 2 / Cycle * Counter;
		float scaleY = InitScale.y + Mathf.Sin (radius) * Deviation;
		float scaleX = InitScale.x + Mathf.Cos (radius) * Deviation / XPuruPuru;

		Vector3 scale = InitScale;
		scale.y = scaleY;
		scale.x = scaleX;
		transform.localScale = scale;

		float Buff = SubDev/2.0f;
		if (Deviation > Buff)
			Deviation -= SubDev;
		else if (Deviation < -Buff)
			Deviation += SubDev;
		else {
			Counter = 0.0f;
			return;
		}
		Counter += 1.0f * Time.deltaTime;
		Counter %= Cycle;
	}

	public void Play(){
		Deviation = InitDeviation;
	}
}
