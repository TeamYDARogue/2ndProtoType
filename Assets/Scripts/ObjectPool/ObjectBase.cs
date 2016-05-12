using UnityEngine;
using System.Collections;

public class ObjectBase : MonoBehaviour {
    /// <summary>
    /// privateな変数をインスペクターから設定できるようにする
    /// </summary>
    [SerializeField]
    /// <summary>
    /// 生存時間
    /// </summary>
    private float activeTime = 2.0f;
    /// <summary>
    /// オブジェクトが有効化されたときに呼び出される。
    /// </summary>
    void OnEnable()
    {
        // 関数名を遅延秒数後に実行する
        // Invoke("関数名",遅延秒数)
        Invoke("NonActive", activeTime);
    }
    /// <summary>
    /// 無効化する
    /// </summary>
    void NonActive()
    {
        gameObject.SetActive(false);
    }
    /// <summary>
    /// オブジェクトが無効化されたときに呼び出される。
    /// </summary>
    void OnDisable()
    {
        // キャンセル
        CancelInvoke();
    }
}