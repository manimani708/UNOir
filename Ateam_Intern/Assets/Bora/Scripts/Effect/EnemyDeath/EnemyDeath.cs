using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour {

	[SerializeField]
	Material m_Material;
	public Shader m_Shader = null; 
	YieldInstruction m_Instruction = new WaitForEndOfFrame();
	public float fMaxCutOff = 1.0f; 
	public float fDuration = 1.0f;
	float fNowCutOff = 0.0f;
	bool bEnd = false;

	public Texture DissolveTex = null;

	void Awake() {
		Material mat = new Material (m_Shader);
		m_Material = GetComponent<SpriteRenderer> ().material = mat;
		mat.SetTexture ("_DissolveTex", DissolveTex);

		m_Material.SetFloat("_CutOff", fNowCutOff);

		transform.DetachChildren ();

		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_ENEMYDEATH);
	}

	void Update() {

		if (fNowCutOff < fMaxCutOff) {
			fNowCutOff += fMaxCutOff * (Time.deltaTime / fDuration);
			m_Material.SetFloat ("_CutOff", fNowCutOff);
		} else {
			bEnd = true;
			Destroy (this.gameObject);
		}
	}

	public bool End {
		get { return bEnd; }
	} 
}
