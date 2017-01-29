using UnityEngine;
using System.Collections;

public class HeelEffect : MonoBehaviour {

	static bool bRun = false; 
	static int nEmitNum = 0;

	[SerializeField]
	float fTime = 0.5f;

	[SerializeField]
	int nMaxEmit = 60;

	[SerializeField]
	ParticleSystem particle = null;

	void Update() {

		if (!bRun)
			return;

		int nEmit = (int)(nMaxEmit * (Time.deltaTime / fTime));
		particle.Emit (nEmit);

		nEmitNum += nEmit;
		if (nEmitNum >= nMaxEmit) {
			bRun = false;
		}
	}

	static public void Run() {
		if (bRun)
			return;

		nEmitNum = 0;
		bRun = true;
		SoundManager.Instance.PlaySE (SoundManager.eSeValue.SE_HEEL);
	}
}
