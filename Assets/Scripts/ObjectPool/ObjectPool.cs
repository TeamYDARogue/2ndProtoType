using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 2016.05.12
/// 制作者:Yuta Hayashi
/// ObjectPool
/// </summary>


public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;
    // シングルトン
    public static ObjectPool instance
    {
        get
        {
            if (_instance == null)
            {
                // シーン上から取得する
                var ObjectPool = new GameObject("ObjectPool");
                _instance = ObjectPool.AddCompornent<ObjectPool>();
                _instance = GetCompornent<ObjectPool>;
            }
            return _instance;
        }
    }
    // ゲームオブジェクトのDictionary
    private Dictionary<int, List<GameObject>> pooledGameObjects = new Dictionary<int, List<GameObject>>();

    // ゲームオブジェクトをpooledGameObjectsから取得。必要であれば新たに生成。
    public GameObject GetGameObject(GameObject prefab, Vector2 position, Quaternion rotation)
    {
        // プレハブのインスタンスID InstanceID
        int InstanceID = prefab.GetInstanceID();

        // DictionaryにInstanceIDが存在しなければ作成する
        if (pooledGameObjects.ContainsKey(InstanceID) == false)
        {
            pooledGameObjects.Add(key, new List<GameObject>());
        }

        List<GameObject> gameObjects = pooledGameObjects[InstanceID];
        GameObject go = null;
        for (int i = 0; i < gameObjects.Count; i++)
        {
            go = gameObjects[i];
            // 未使用時(非アクティブ）
            if (go.activeInHierarchy == false)
            {

                go.transform.position = position;//位置を指定
                go.transform.rotation = rotation;//回転を指定（いらないかもね）
                go.SetActive(true);//アクティブ状態にする
                return go;
            }
        }

        // 使用できるものがないので新たに生成する
        go = (GameObject)Instantiate(prefab, position, rotation);
        // ObjectPoolゲームオブジェクトの子要素にする
        go.transform.parent = transform;
        // リストに追加
        gameObjects.Add(go);
        return go;
    }

    // ゲームオブジェクトを非アクティブにする。こうすることで再利用可能状態にする
    public void ReleaseGameObject(GameObject go)
    {
        // 非アクティブにする
        go.SetActive(false);
    }
}