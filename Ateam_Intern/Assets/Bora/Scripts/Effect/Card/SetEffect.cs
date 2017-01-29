using UnityEngine;
using System.Collections;

public class SetEffect : MonoBehaviour {

	[SerializeField] Vector3 InitScale = Vector3.zero;

	[SerializeField] ParticleSystem[] particle = null;
	[SerializeField] Vector3[] InitSize = null;
	//[SerializeField] float[] InitSpeed;

	[SerializeField]
	float[] Scale = null;

	//[SerializeField]
	//float[] Speed;

	[SerializeField]
	Color[] FourColor = new Color[4];

	void Start() {
		particle = new ParticleSystem[4];
		InitSize = new Vector3[4];
		//InitSpeed = new float[4];
		InitScale = transform.localScale;

		for (int i = 0; i < transform.childCount; i++) {
			particle [i] = transform.GetChild (i).GetComponent<ParticleSystem>();
			InitSize [i] = particle [i].transform.localScale;
			//InitSpeed [i] = particle [i].startSpeed;

			for (int j = 1; j <= particle [i].transform.childCount; j++) {
				particle [i + j] = particle [i].transform.GetChild (j - 1).GetComponent<ParticleSystem>();
				InitSize [i + j] = particle [i + j].transform.localScale; 
				//InitSpeed [i + j] = particle [i + j].startSpeed;
			}
		}
		//Debug.Log (transform.childCount);
	}

	public void ChangeEffectAmount(int nAmount, UnoStruct.eColor color) {

		//Debug.Log (nAmount);

		Color nowColor = new Color (1,1,1,1);

		if(color != UnoStruct.eColor.COLOR_WILD) {
			nowColor = FourColor[(int)color];
		} else {
			nowColor = FourColor[Random.Range(0, FourColor.Length)];
		}

		// カード枚数に応じてエフェクトを変更
		switch (nAmount) {

		case 0:
			// カードなし
			break;

		case 1:
		case 2:
		case 3:
		case 4:
			for (int i = 0; i < particle.Length; i++) {
				particle [i].transform.localScale = InitSize [i] * Scale [0];
				//particle [i].startSpeed = InitSpeed [i] * Speed [0];
				particle [i].startColor = nowColor;
			}
			break;

		case 5:
		case 6:
		case 7:
		case 8:
			for (int i = 0; i < particle.Length; i++) {
				particle [i].transform.localScale = InitSize [i] * Scale [1];
				//particle [i].startSpeed = InitSpeed [i] * Speed [1];
				particle [i].startColor = nowColor;
			}
			break;

		case 9:
		case 10:
		case 11:
		case 12:
			for (int i = 0; i < particle.Length; i++) {
				particle [i].transform.localScale = InitSize [i] * Scale [2];
				//particle [i].startSpeed = InitSpeed [i] * Speed [2];
				particle [i].startColor = nowColor;
			}
			break;

		case 13:
		case 14:
		case 15:
		case 16:
			for (int i = 0; i < particle.Length; i++) {
				particle [i].transform.localScale = InitSize [i] * Scale [3];
				//particle [i].startSpeed = InitSpeed [i] * Speed [3];
				particle [i].startColor = nowColor;
			}
			break;
		}
	}
}
