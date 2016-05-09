using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// SE再生用クラス
/// </summary>
public class SEPlayer : MonoBehaviour {

    public struct SEData
    {
        public SEData(string resName):this()
        {
            this.resName = resName;
            clip = Resources.Load("SE/" + resName) as AudioClip;
        }
        public string resName;
        public AudioClip clip;
    }

    /// <summary>
    /// SE再生リスト
    /// </summary>
    private List<AudioSource> m_sources = new List<AudioSource>();

    /// <summary>
    /// SE登録変数
    /// </summary>
    private Dictionary<string, SEData> m_audioMap = new Dictionary<string, SEData>();

    /// <summary>
    /// 最大ボリューム値
    /// </summary>
    public const float m_maxVolume = 0.6f;


    /// <summary>
    /// Singleton
    /// </summary>
    private static SEPlayer m_instance = null;
    public static SEPlayer Instance
    {
        get
        {
            if(m_instance == null)
            {
                var obj = new GameObject("SEPlayer");
                m_instance = obj.AddComponent<SEPlayer>();
            }
            return m_instance;
        }
    }

    /// <summary>
    /// 再生
    /// </summary>
    /// <param name="resName">リソースの名前</param>
    /// <param name="pitch">ピッチ</param>
    /// <param name="loop">ループするかどうか</param>
    public void Play(string resName,float pitch = 1.0f,bool loop = false)
    {
        if (!m_audioMap.ContainsKey(resName)) { m_audioMap.Add(resName,new SEData(resName));}

        m_sources.Add(gameObject.AddComponent<AudioSource>());
        var index = m_sources.Count - 1;

        m_sources[index].clip = m_audioMap[resName].clip;
        m_sources[index].pitch = pitch;
        m_sources[index].loop = loop;
        m_sources[index].volume = m_maxVolume;
        m_sources[index].Play();
    }

    /// <summary>
    /// 音量変更
    /// </summary>
    /// <param name="resName">リソースの名前</param>
    /// <param name="volume">変更ボリューム値</param>
    public void ChangeVolume(string resName,float volume)
    {
        foreach(var source in m_sources)
        {
            if(source.clip.name == resName)
            {
                source.volume = volume;
                break;
            }
        }
    }

    /// <summary>
    /// 停止
    /// </summary>
    /// <param name="resName">リソースの名前</param>
    /// <param name="time">時間(基本ZERO)</param>
    public void Stop(string resName,float time = 0.0f)
    {
        StartCoroutine(WaitStop(resName,time));
    }

    IEnumerator WaitStop(string resName,float time)
    {
        yield return new WaitForSeconds(time);

        foreach(var source in m_sources)
        {
            if(source.clip.name == resName)
            {
                source.Stop();
                break;
            }
        }
    }

    /// <summary>
    /// 全てのSEを停止する
    /// </summary>
    public void AllStop()
    {
        foreach(var source in m_sources)
        {
            source.Stop();
        }
    }

    /// <summary>
    /// 利用していないSEを定期的にメモリから削除する
    /// </summary>
    void Update()
    {
        foreach(var source in m_sources)
        {
            if(!source.isPlaying)
            {
                Destroy(source);
                m_sources.Remove(source);
                break;
            }
        }
    }

    public bool IsPlaying(string resName)
    {
        foreach(var source in m_sources)
        {
            if(source.isPlaying && source.clip.name == resName)
            {
                return true;
            }
        }
        return false;
    }
}
