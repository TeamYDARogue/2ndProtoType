using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// BGMの再生用クラス
/// 
/// AudioClip   = mp3やoggなどのオーディオファイル
/// AudioSource = AudioClipの再生をする
/// 
/// </summary>
public class BGMPlayer : MonoBehaviour {

    /// <summary>
    /// BGMのデータ
    /// </summary>
    public struct BGMData
    {
        public BGMData(string resName):this()
        {
            this.resName = resName;
            clip = Resources.Load("BGM/" + resName) as AudioClip;
        }

        public string resName;
        public AudioClip clip;
    }

    /// <summary>
    /// 最小ボリューム値
    /// </summary>
    private const float m_minVolume = 0;
    
    /// <summary>
    /// 最大ボリューム値
    /// </summary>
    private const float m_maxVolume = 0.5f;
    
    /// <summary>
    /// 初期のフェードボリューム値
    /// </summary>
    private const float m_startFadeInVolume = 0.005f;

    /// <summary>
    /// オーディオ登録変数
    /// </summary>
    private static AudioSource m_source = null;


    Dictionary<string, BGMData> audioMap = new Dictionary<string, BGMData>();

    FadeTimeData m_fadeTime;


    public bool IsPlaying
    {
        get
        {
            return m_source.isPlaying;
        }
    }

    /// <summary>
    /// BGMPlayerの生成
    /// </summary>
    private static BGMPlayer m_instance = null;
    public static BGMPlayer Instance
    {
        get
        {
            if(m_instance == null)
            {
                var obj = new GameObject("BGMPlayer");
                m_instance = obj.AddComponent<BGMPlayer>();
                m_source = obj.AddComponent<AudioSource>();
                m_source.loop = true;
            }
            return m_instance;
        }
    }

    /// <summary>
    /// 再生中かどうか名前で取得する
    /// </summary>
    /// <param name="resName">BGMの名前</param>
    /// <returns>再生中 : true / 再生してない : false</returns>
    public bool IsPlayingByName(string resName)
    {
        if (m_source.clip == null) return false;
        if(m_source.clip.name == resName && m_source.isPlaying)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// BGMの再生
    /// </summary>
    /// <param name="resName">Resources/BGMの中にあるBGMの名前</param>
    /// <param name="fadeTime">フェードインの時間</param>
    public void Play(string resName,FadeTimeData fadeTime)
    {
        if(!audioMap.ContainsKey(resName))
        {
            audioMap.Add(resName, new BGMData(resName));
        }

        m_fadeTime = fadeTime;
        m_source.clip = audioMap[resName].clip;
        m_source.Play();
        m_source.volume = m_startFadeInVolume;
        StartFadeIn(m_fadeTime.inTime);
    }

    /// <summary>
    /// 停止
    /// </summary>
    public void Stop()
    {
        StartFadeOut(m_fadeTime.outTime);
    }

    /// <summary>
    /// フェードイン開始
    /// </summary>
    /// <param name="time"></param>
    void StartFadeIn(float time)
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", m_startFadeInVolume, "to", m_maxVolume, "time", time, "onupdate", "UpdateHandler"));
    }

    /// <summary>
    /// フェードアウト開始
    /// </summary>
    /// <param name="time"></param>
    void StartFadeOut(float time)
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", m_maxVolume, "to", m_minVolume, "time", time, "onupdate", "UpdateHandler"));
    }

    void UpdateHandler(float value)
    {
        m_source.volume = value;
        if(m_source.volume <= 0)
        {
            m_source.Stop();
        }
    }
}
