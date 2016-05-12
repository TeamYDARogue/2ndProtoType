using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Activeではないオブジェクトをリターン
/// </summary>
public class ObjectPool : MonoBehaviour {
    public static ObjectPool current;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private int maxPooledValue = 10;
    List<GameObject> pooledObjects;

    private bool willGrow = true;//シーン中にmaxPooledValueを増やしても対応できるフラグ

    void Awake()
    {
        current = this;
    }

    void Start () {
        pooledObjects = new List<GameObject>();
        //Poolします
        for(int i = 0; i < maxPooledValue; i++)
        {
            //インスタンス生成
            GameObject poolObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
            //無効化
            poolObject.SetActive(false);
            //配列追加
            pooledObjects.Add(poolObject);
        }
	}
	/// <summary>
    /// Activeオブジェクトを使いまわす
    /// </summary>
    /// <returns>ActiveGameObject</returns>
    public GameObject GetPooledObject()
    {
        //すべてのオブジェクトに対して
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            //ゲームオブジェクトがシーンで有効でないとき
            if (!pooledObjects[i].activeInHierarchy) 
            {
                return pooledObjects[i];
            }
        }
        if (willGrow)
        {
            GameObject poolObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
            pooledObjects.Add(poolObject);
            return poolObject;
        }
        return null;

    }

}
