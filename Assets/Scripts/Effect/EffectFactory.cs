using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EffectFactory : MonoBehaviour
{
    /// <summary>
    /// エフェクトの生成工場	
    /// </summary>
    
    /// <summary>
    /// Effectの名前をInspector上で登録できる
    /// </summary>
    [SerializeField]
    private string m_effectName = string.Empty;

    /// <summary>
    /// Effectのpositionの設定
    /// </summary>
    [SerializeField]
    private Vector3 m_effectPos;
    
    /// <summary>
    /// Effectの表示時間
    /// </summary>
    [SerializeField]
    private float effectTime;

    private EffectLifeTime m_LifeTime = new EffectLifeTime();

    /// <summary>
    /// EffectFactoryの生成
    /// シングルトン化
    /// </summary>
    private static EffectFactory m_instance = null;
    public static EffectFactory Instace
    {
        get
        {
            if (m_instance == null)
            {
                var obj = new GameObject("EffectFactory");
                m_instance = obj.AddComponent<EffectFactory>();
                m_instance.GetComponent<EffectFactory>().enabled = true;
            }
            return m_instance;
        }
    }

    void Awake()
    {
        ResourceManager.Instance.ResourcesLoad("Game");
    }

    void Update()
    {
        Play();
    }

    private void Play()
    {
        Create(m_effectName, m_effectPos, Quaternion.identity);
    }

    /// <summary>
    /// Effectの生成
    /// </summary>
    /// <param name="resName">ResourceManagerに登録されているEffectの名前</param>
    /// <param name="effectPos">エフェクトの位置</param>
    /// <param name="effectRotation">エフェクトの向き</param>
    public void Create(string resName, Vector3 effectPostion, Quaternion effectRotation)
    {
        GameObject go = ResourceManager.Instance.GetResourceScene(resName) as GameObject;

        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject effectGo = Instantiate(go, effectPostion, effectRotation) as GameObject;
            m_LifeTime.lifetime = effectTime;
            m_LifeTime.ObjectDestroy(effectGo);
        }
    }

}