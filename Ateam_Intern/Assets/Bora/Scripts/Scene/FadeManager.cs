using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移時のフェードイン・アウトを制御するためのクラス .
/// </summary>
public class FadeManager : MonoBehaviour
{

    #region Singleton

    private static FadeManager instance;

    public static FadeManager Instance {
        get {
            if (instance == null) {
                instance = (FadeManager)FindObjectOfType(typeof(FadeManager));

                if (instance == null) {
                    Debug.LogError(typeof(FadeManager) + "is nothing");
                }
            }

            return instance;
        }
    }

    #endregion Singleton

    /// フェード中の透明度
    float fadeAlpha = 0;
    /// フェード中かどうか
    bool isFading = false;
    /// フェード色
	[SerializeField]
	Color fadeColor = Color.black;


    public void Awake() {
        if (this != Instance) {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void OnGUI()
    {
        if (this.isFading)
        {
            //色と透明度を更新して白テクスチャを描画 .
            this.fadeColor.a = this.fadeAlpha;
            GUI.color = this.fadeColor;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }
    }

    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
	public void LoadLevel(string scene, float interval, bool bStopBgm)
    {
        if (isFading) return;
		this.isFading = true;
		StartCoroutine(TransScene(scene, interval, bStopBgm));
    }

    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
	private IEnumerator TransScene(string scene, float interval, bool bStopBgm)
    {
        //だんだん暗く .
//        this.isFading = true;
        float time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

		if (bStopBgm) {
			SoundManager.Instance.StopBGM ();
		}

		// キャッシュの開放（場合に合わせて行った方がいいと思う
		//Resources.UnloadUnusedAssets();
        //シーン切替 .
        SceneManager.LoadScene(scene);

        //だんだん明るく .
        time = 0;
		while (this.fadeAlpha >= 0.2f)
        {
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        this.isFading = false;
    }

    public bool GetFadeing() {
        return isFading;
    }
}

