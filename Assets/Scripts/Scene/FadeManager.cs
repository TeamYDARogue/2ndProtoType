using UnityEngine;
using System.Collections;

/// <summary>
/// フェードインやフェードアウトを管理するクラス
/// </summary>

public class FadeManager : MonoBehaviour {

    private Texture2D m_texture = null;
    private float m_fadeAlpha = 0;
    private float m_fadeTime = 0;
    
    public bool IsFading
    {
        get;
        private set;
    }


    void Awake()
    {
        m_texture = new Texture2D(32, 32, TextureFormat.RGB24, false);
        m_texture.SetPixel(0, 0, Color.white);
        m_texture.Apply();
    }

    void OnGUI()
    {
        GUI.color = new Color(0, 0, 0, m_fadeAlpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), m_texture);
    }


    /// <summary>
    /// フェードインの開始
    /// </summary>
    /// <param name="time">時間</param>
    public void StartFadeIn(float time)
    {
        StartFade(time, 1, 0);
    }

    /// <summary>
    /// フェードアウトの開始
    /// </summary>
    /// <param name="time">時間</param>
    public void StartFadeOut(float time)
    {
        StartFade(time, 0, 1);
    }

    /// <summary>
    /// フェード処理**
    /// (iTweenの利用)
    /// </summary>
    /// <param name="time">フェードする時間</param>
    /// <param name="begin">開始する値</param>
    /// <param name="end">終了する値</param>
    private void StartFade(float time,float begin,float end)
    {
        IsFading = true;
        m_fadeTime = time;
        iTween.ValueTo(gameObject, iTween.Hash("from", begin, "to", end, "time", m_fadeTime, "onupdate", "UpdateHandler"));
    }

    private void UpdateHandler(float value)
    {
        m_fadeAlpha = value;
        m_fadeTime -= Time.deltaTime;
        if(m_fadeTime <= 0)
        {
            IsFading = false;
        }
    }
}
