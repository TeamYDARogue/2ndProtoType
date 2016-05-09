using UnityEngine;
using System.Collections;

public class CameraShaker : MonoBehaviour {

    [SerializeField]
    private float m_shakeDecay = 0.001f;

    [SerializeField]
    private float m_coefShakeIntensity = 0.01f;

    private Vector3 m_originPosition;
    private Quaternion m_originRotation;
    private float m_shakeIntensity;


	// Use this for initialization
	void Start () {
        m_originRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
	    if(m_shakeIntensity > 0)
        {
            transform.position = m_originPosition + Random.insideUnitSphere * m_shakeIntensity;
            transform.rotation = new Quaternion(
                m_originRotation.x + Random.Range(-m_shakeIntensity, m_shakeIntensity) * 2f,
                m_originRotation.y + Random.Range(-m_shakeIntensity, m_shakeIntensity) * 2f,
                m_originRotation.z + Random.Range(-m_shakeIntensity, m_shakeIntensity) * 2f,
                m_originRotation.w + Random.Range(-m_shakeIntensity, m_shakeIntensity) * 2f);

            m_shakeIntensity -= m_shakeDecay;
        }
        else
        {
            transform.rotation = m_originRotation;
        }
	}

    public void Shake()
    {
        m_originPosition = transform.position;
        m_shakeIntensity = m_coefShakeIntensity;
    }
}
