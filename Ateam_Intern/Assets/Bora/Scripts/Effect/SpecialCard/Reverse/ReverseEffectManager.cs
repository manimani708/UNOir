using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReverseEffectManager : MonoBehaviour {

	static ReverseEffectManager instance;

	public static ReverseEffectManager Instance {
		get {
			if (instance == null) {
				instance = (ReverseEffectManager)FindObjectOfType(typeof(ReverseEffectManager));

				if (instance == null) {
					Debug.LogError("ReverseEffectManager Instance Error");
				}
			}
			return instance;
		}
	}

	List<ReverseEffect> effectList = new List<ReverseEffect> ();
	public GameObject ReverseObj = null; 

	void Awake() {
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}
	}

	// リバース数
	public int GetAmount() {
		return effectList.Count;
	}

	public void Add() {
		GameObject temp = (GameObject)Instantiate (ReverseObj);

		if (effectList.Count > 0) {
			temp.SetActive (false);
		}

		effectList.Add (temp.GetComponent<ReverseEffect>());
	}

	public void SetNext() {
		if (effectList.Count <= 0)
			return;
		
		effectList[0].gameObject.SetActive(true);
	}

	public void Run() {
		if (effectList.Count <= 0) {
			//Debug.Log ("Reverseできないけど？");
			return;
		}

		// 走らせて殺す
		effectList [0].Run ();
		effectList.RemoveAt (0);

		// プルンって音？
		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_REVERSE);
	}
}
