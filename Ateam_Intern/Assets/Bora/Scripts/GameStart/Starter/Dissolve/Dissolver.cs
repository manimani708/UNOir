using UnityEngine;
using System.Collections;

public class Dissolver : MonoBehaviour {

	[SerializeField]
    Material m_Material;
	public Shader m_Shader = null; 
    YieldInstruction m_Instruction = new WaitForEndOfFrame();
	public float fInitCutOff = 0.1f;
	public float fMaxCutOff = 1.0f; 
	public float fDuration = 1.0f;
	float fNowCutOff = 0.0f;

	[SerializeField]
	bool bEnd = false;

	public Texture DissolveTex = null;

	bool bSe = false;

	void Awake() {
		Material mat = new Material (m_Shader);
		m_Material = GetComponent<SpriteRenderer> ().material = mat;
		mat.SetTexture ("_DissolveTex", DissolveTex);

		fNowCutOff = fMaxCutOff;
		m_Material.SetFloat("_CutOff", fNowCutOff);
	}

	void Update() {

		if (!bSe) {
			bSe = true;

			if (GetComponent<Enemy> ().m_isBoss) {
				SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_BOSSSTART);
			} else {
				SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_ENEMYSTART);
			}
		}

		if (fNowCutOff > fInitCutOff) {
			fNowCutOff -= (fMaxCutOff - fInitCutOff) * (Time.deltaTime / fDuration);
			if (fNowCutOff < fInitCutOff) {
				fNowCutOff = fInitCutOff;
			}
			m_Material.SetFloat ("_CutOff", fNowCutOff);
		} else {
			bEnd = true;
		}
	}

	public bool End {
		get { return bEnd; }
		set { bEnd = value; }
	} 

	public void Skip() {
		fNowCutOff = fInitCutOff;
		m_Material.SetFloat ("_CutOff", fNowCutOff);
	}

    public void ResetCutOff()
    {
        fNowCutOff = 1f;
        m_Material.SetFloat("_CutOff", fNowCutOff);
    }
}
