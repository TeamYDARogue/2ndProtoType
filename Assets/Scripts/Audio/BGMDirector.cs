using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BGMDirector : MonoBehaviour {
    
    /// <summary>
    /// Sliderの変数
    /// </summary>
    [SerializeField] 
    private Slider m_BGMSlider;

    /// <summary>
    /// BGMの名前を登録できる
    /// </summary>
    [SerializeField]
    private string bgmName = string.Empty;

    /// <summary>
    /// FadeIn,Fadeoutが出来る構造体
    /// </summary>
    FadeTimeData m_fadeTime = new FadeTimeData(1,1);


	void Awake()
    {
        m_BGMSlider = GetComponent<Slider>();
    }
	
	void Update()
    {
        VolumeBGM();
        ChangeVolume();
    }

    private void VolumeBGM()
    {
        if (BGMPlayer.Instance.IsPlayingByName(bgmName)) return;
        BGMPlayer.Instance.Play(bgmName,m_fadeTime);
    }

    private void ChangeVolume()
    {
        BGMPlayer.Instance.ChangeVolume(m_BGMSlider.value);
    }
}
