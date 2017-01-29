using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContinueCardBase : MonoBehaviour {

	[SerializeField]
	bool bDecision = false;
	public bool bTouch = false;
	public bool bEnd { get; set; }
	public float fTime = 1.0f;
	public float fRotAmount = 3.0f;
	SpriteRenderer render = null;

	Vector3 InitPos = Vector3.zero; 
	Vector3 Distance = Vector3.zero;

	[SerializeField]
	Vector3 InitScale = Vector3.zero;
	public Vector3 MaxScale = new Vector3(2,2,0);

	public ContinueSkillCreator skillCreator = null;

	public int nValue { get; set; }

	[SerializeField]
	ParticleSystem particle = null;

	[SerializeField]
	ContinueMessage message = null;

	[SerializeField]
	CardRotation cardRot = null;

	[SerializeField]
	Sprite[] spriteList = new Sprite[3];

	public void ReInit() {

		if (InitPos.y != 0.0f) {
			transform.localPosition = InitPos;
		}
		transform.localScale = InitScale;
		cardRot.Back = spriteList [nValue];
		transform.eulerAngles = new Vector3 (0,0,0);
	}

	void Start() {
		InitPos = transform.localPosition;
		Distance.x = InitPos.x;
		fRotAmount = 2.5f * 360.0f;
		render = GetComponent<SpriteRenderer> ();
	}

	public virtual void Update() {

		if (bDecision) {
			if (bTouch) {
				transform.Rotate (new Vector3 (0, fRotAmount * (Time.deltaTime / fTime), 0));
				transform.localScale += (MaxScale - InitScale) * (Time.deltaTime / fTime);
				transform.localPosition -= Distance * (Time.deltaTime / fTime);

				if (transform.localScale.x >= MaxScale.x) {
					bDecision = false;
					transform.eulerAngles = new Vector3 (0,fRotAmount,0);
					skillCreator.Set ();
					message.SetValue (nValue);
					particle.Play ();
					SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_CONTINUEPARTICLE);
					//particle.Simulate (0.0f, false, true);
				}
			} else {
				transform.localScale -= InitScale * (Time.deltaTime / fTime);

				if (transform.localScale.x <= 0.0f) {
					bDecision = false;
				}
			}
		} 
	}

	public virtual void Run(bool bFlg) {
		bDecision = true;
		bTouch = bFlg;

		if (bTouch) {
			skillCreator.SetValue (nValue);
			render.sortingOrder = 4;
			SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_CONTINUECARDROT);
		} else {
			render.sortingOrder = 3;
		}
	}

	public bool Decision {
		get { return bDecision; }
		set { bDecision = value; }
	}
}
