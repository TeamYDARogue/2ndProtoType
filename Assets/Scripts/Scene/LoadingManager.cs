using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LoadingManager : MonoBehaviour {

    [SerializeField]
    private List<Sprite> m_sprites = new List<Sprite>();

    private Image m_image = null;

    private FadeManager m_fadeManager = null;
    private FadeTimeData m_fadeTime;
    private bool isFadeOut = false;

    /// <summary>
    /// ローディングの情報設定
    /// </summary>
    /// <param name="fadeManager">フェードする管理インスタンス</param>
    /// <param name="fadeTime">フェードタイム情報</param>
    public void SetLoadingData(FadeManager fadeManager,FadeTimeData fadeTime)
    {
        this.m_fadeManager = fadeManager;
        this.m_fadeTime = fadeTime;

        m_image = FindObjectOfType<Image>();
        Debug.Log("Loading...SpriteNumber = " + SceneManager.gameStage);
        Debug.Log("Loading...SpriteName = " + m_sprites[SceneManager.gameStage].name);
        m_image.sprite = m_sprites[SceneManager.gameStage];
    }

    /// <summary>
    /// フェードスタート
    /// </summary>
    public void StartFade()
    {
        m_fadeManager.StartFadeOut(m_fadeTime.outTime);
        isFadeOut = true;
    }

    /// <summary>
    /// ｱｯﾌﾟﾃﾞｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴｴﾄ
    /// </summary>
    void Update()
    {
        if (!isFadeOut) return;
        if (m_fadeManager.IsFading) return;

        m_fadeManager.StartFadeIn(m_fadeTime.inTime);
        Destroy(gameObject);
    }
}