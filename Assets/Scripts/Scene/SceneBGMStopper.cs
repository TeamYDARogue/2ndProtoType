using UnityEngine;
using System.Collections;

public class SceneBGMStopper : ISceneBGM {

    public override void AudioStop()
    {
        BGMPlayer.Instance.Stop();
    }
}