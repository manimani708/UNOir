using UnityEngine;
using System.Collections;

public class CardRotation : MonoBehaviour {

	public float fRotTime = 2.0f;
	public Sprite Table = null;
	public Sprite Back = null;

	SpriteRenderer spriteRenderer = null;

	public bool bEnd = false; // { get; private set; }
	Vector3 InitPos = Vector3.zero;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		InitPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		//transform.eulerAngles += new Vector3 (0, 360 * (Time.deltaTime / fRotTime), 0);

		// 裏
		if (transform.eulerAngles.y > 90 && transform.eulerAngles.y <= 270) {
			spriteRenderer.sprite = Back;
		} 
		// 表
		else if(transform.eulerAngles.y < 90 || transform.eulerAngles.y >= 270) {
			spriteRenderer.sprite = Table;
		}
	}

	public bool End {
		get { return bEnd; }
		set { bEnd = value; }
	}
}
