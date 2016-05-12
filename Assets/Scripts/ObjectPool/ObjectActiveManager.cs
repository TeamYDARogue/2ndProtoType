using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// returnされたActiveではないオブジェクトを使いまわすマネージャー
/// </summary>
public class ObjectActiveManager : MonoBehaviour {
    private float CreateInterval = 2.0f; //生成間隔
	
	void Start () {
        //関数名,初回呼び出しまでの遅延秒数,次回呼び出しの遅延秒数)
        InvokeRepeating("Create", CreateInterval, CreateInterval);
	}

    void Create()
    {
        //使い回しobject代入//
        GameObject activeObject = ObjectPool.current.GetPooledObject();
        //使いまわせないオブジェクトをreturn
        if (activeObject == null)return;
        //使い回し
        activeObject.transform.position = transform.position;
        activeObject.transform.rotation = transform.rotation;
        activeObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            activeObject.SetActive(false);
        }
    }		
}
