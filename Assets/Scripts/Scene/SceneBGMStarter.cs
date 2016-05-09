using UnityEngine;
using System.Collections;

public class SceneBGMStarter : ISceneBGM {

    [SerializeField]
    protected string bgmName = string.Empty;

    [SerializeField]
    protected FadeTimeData m_fadeTime = new FadeTimeData(1, 1);

    
    void Awake()
    {

    }

    void Update()
    {
        if (BGMPlayer.Instance.IsPlayingByName(bgmName)) return;

        Debug.Log("Called SceneBGMStarter Update!!");
        AudioStart();
    }

    protected override void AudioStart()
    {
        BGMPlayer.Instance.Play(bgmName, m_fadeTime);
    }
}
