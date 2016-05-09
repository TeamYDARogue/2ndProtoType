using UnityEngine;
using System.Collections;

/// <summary>
/// シーンインターフェイス
/// </summary>

public class IScene : MonoBehaviour {

    [SerializeField]
    protected string bgmName = string.Empty;

    [SerializeField]
    protected FadeTimeData m_fadeTime = new FadeTimeData(1, 1);


    public void AudioStart()
    {
        BGMPlayer.Instance.Play(bgmName, m_fadeTime);
    }

    public void AudioStop()
    {
        BGMPlayer.Instance.Stop();
    }
}
