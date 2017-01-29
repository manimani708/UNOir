using UnityEngine;
using System.Collections;

public class EffectCreator_Debug : MonoBehaviour {

	public GameObject Player = null;
	public GameObject Enemy = null;
	public GameObject Fever = null; 

	public GameObject NormalAtack = null;
	public GameObject StrongAtack = null;
	public GameObject WeakAtack = null;
	public GameObject EnemyAtack = null; 
	public GameObject BossNormalAtack = null; 
	public GameObject BossSpecialAtack = null; 

	public GameObject solerBeam = null; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			GameObject Obj = (GameObject)Instantiate (NormalAtack, Player.transform.position, transform.rotation);
			int nRandom = Random.Range (0, (int)UnoStruct.eColor.COLOR_WILD);
			Obj.GetComponent<EffectBase> ().Set ((UnoStruct.eColor)nRandom, Enemy.transform.position);
		}

		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			GameObject Obj = (GameObject)Instantiate (StrongAtack, Enemy.transform.position, transform.rotation);
			int nRandom = Random.Range (0, (int)UnoStruct.eColor.COLOR_WILD);
			Obj.GetComponent<EffectBase> ().Set ((UnoStruct.eColor)nRandom, Enemy.transform.position);
		}

		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			GameObject Obj = (GameObject)Instantiate (EnemyAtack, Enemy.transform.position, transform.rotation);
			Obj.GetComponent<EffectBase> ().Set (Fever.transform.position, Input.GetKey(KeyCode.Q));
		}

		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			GameObject Obj = (GameObject)Instantiate (BossNormalAtack);
			Obj.GetComponent<EffectBase> ().Set (Input.GetKey(KeyCode.Q));
		}

		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			GameObject Obj = (GameObject)Instantiate (BossSpecialAtack);
			Obj.GetComponent<EffectBase> ().Set (Input.GetKey(KeyCode.Q));
		}

		if (Input.GetKeyDown (KeyCode.Alpha6)) {
			ReverseEffectManager.Instance.Add ();
		}

		if (Input.GetKeyDown (KeyCode.Alpha7)) {
			Instantiate (solerBeam);
		}

		if (Input.GetKeyDown (KeyCode.Alpha8)) {
			GameObject Obj = (GameObject)Instantiate (WeakAtack, Enemy.transform.position, transform.rotation);
			Obj.GetComponent<EffectBase> ().Set (Fever.transform.position, Input.GetKey(KeyCode.Q));
		}
	}
}
