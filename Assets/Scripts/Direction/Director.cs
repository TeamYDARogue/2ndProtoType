using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour
{
    [SerializeField]
    private Camera m_camera;
    private CameraShaker m_shaker;
    void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        m_shaker = m_camera.GetComponent<CameraShaker>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            m_shaker.Shake();
        }
    }
}
