using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour
{
    void Awake()
    {
        var resource = ResourceManager.Instance;
        var scene = SceneManager.Instance;

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }
}
