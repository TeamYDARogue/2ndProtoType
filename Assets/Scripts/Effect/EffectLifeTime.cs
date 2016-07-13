using UnityEngine;
using System.Collections;

public class EffectLifeTime : MonoBehaviour {

    private float m_effectTime;

    /// <summary>
    /// Effectの再生時間を取得するプロパティ
    /// </summary>
    public float lifetime
    {
        get { return m_effectTime; }
        set { m_effectTime = value; }
    }
   
    public void ObjectDestroy(GameObject go)
    {
        Destroy(go,m_effectTime);
    }
}
