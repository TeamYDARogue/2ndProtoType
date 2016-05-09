using UnityEngine;
using System.Collections;

/// <summary>
/// シーン管理用クラス
/// </summary>

public class SceneManager : MonoBehaviour {

    /// <summary>
    /// 状態
    /// </summary>
    enum State
    {
        None,
        Change,
        Loading,
    }

    /// <summary>
    /// シーンのプレハブフォルダー名
    /// </summary>
    private const string m_scenePrefabFolder = "ScenePrefabs/";

    /// <summary>
    /// ローディングのフォルダー名
    /// </summary>
    private const string m_loadingFolder = "Loading";

    private static SceneManager m_instance = null;
    public static SceneManager Instance
    {
        get
        {
            if(m_instance == null)
            {
                //Debug.Log("Create SceneManager!!");
                var obj = new GameObject("SceneMangaer");
                m_instance = obj.AddComponent<SceneManager>();
                m_instance.GetComponent<SceneManager>().enabled = true;
                m_instance.gameObject.AddComponent<FadeManager>();
            }
            return m_instance;
        }
    }

    /// <summary>
    /// フェードマネージャー
    /// </summary>
    private FadeManager m_fadeManager = null;


    /// <summary>
    /// フェードタイム
    /// </summary>
    private FadeTimeData m_fadeTime = new FadeTimeData(1, 1);

    /// <summary>
    /// 現在のシーンオブジェクト
    /// </summary>
    private GameObject m_scene = null;

    /// <summary>
    /// ローディングインスタンス
    /// </summary>
    private GameObject m_loadingInstance = null;

    /// <summary>
    /// ローディング管理
    /// </summary>
    private LoadingManager m_loadingManager = null;

    /// <summary>
    /// 現在のシーン
    /// </summary>
    private SceneNameManager.Scene m_nowScene = SceneNameManager.Scene.TitleScene;

    /// <summary>
    /// 次のシーン
    /// </summary>
    private SceneNameManager.Scene m_nextScene = SceneNameManager.Scene.TitleScene;

    /// <summary>
    /// シーン管理の状態
    /// </summary>
    [SerializeField]
    private State state = State.None;

    /// <summary>
    /// ローディングするか判定
    /// </summary>
    private bool isLoading = false;

    /// <summary>
    /// ステージ番号の管理用だったけどロード画面のスプライトナンバーになってる
    /// </summary>
    public static int gameStage = 0;

    /// <summary>
    /// ロード画面用
    /// </summary>
    public static float WaitTimeData = 2.5f;
    
    
    /// <summary>
    /// 初期化するよ
    /// </summary>
    void Start()
    {
        m_fadeManager = GetComponent<FadeManager>();
        m_fadeManager.StartFadeIn(m_fadeTime.inTime);

        m_scene = GameObject.Find(m_nowScene.ToString());
    }

    /// <summary>
    /// いろいろ監視しなきゃいけない。面倒である
    /// </summary>
    void Update()
    {
        //Debug.Log("SceneManager Update!!");
        ChangeScene();
        LoadingFade();
    }

    /// <summary>
    /// シーンを切り替える準備
    /// </summary>
    /// <param name="scene">次に切り替えるシーンの名前</param>
    /// <param name="isLoading">ローディングするかどうか</param>
    public void ChangeSceneLoad(SceneNameManager.Scene scene,bool isLoading)
    {
        if (state != State.None) return;

        m_fadeManager.StartFadeOut(m_fadeTime.outTime);
        m_nextScene = scene;
        state = State.Change;
        this.isLoading = isLoading;
    }

    /// <summary>
    /// ローディングのフェードイン
    /// </summary>
    private void LoadingFade()
    {
        if (state != State.Loading) return;
        if (GameObject.Find(m_nowScene.ToString()) == null) return;

        m_loadingManager.StartFade();
        isLoading = false;
        state = State.None;
    }

    /// <summary>
    /// シーンの切り替え
    /// </summary>
    private void ChangeScene()
    {
        if (state != State.Change) return;
        if (m_fadeManager.IsFading) return;

        UnLoadDestroy();
        m_fadeManager.StartFadeIn(m_fadeTime.inTime);

        state = CheckLoadingScene();
        m_nowScene = m_nextScene;
    }


    /// <summary>
    /// ローディングかどうかのチェック
    /// ローディングしている場合は、ローディング画面を生成->次のシーンの生成
    /// ローディングしてない場合は、そのまま次のシーンの生成
    /// </summary>
    /// <returns></returns>
    private State CheckLoadingScene()
    {
        if(isLoading)
        {
            m_loadingInstance = Instantiate(Resources.Load(m_scenePrefabFolder + m_loadingFolder)) as GameObject;
            m_loadingManager = m_loadingInstance.GetComponent<LoadingManager>();
            m_loadingManager.SetLoadingData(m_fadeManager, m_fadeTime);
            StartCoroutine("WaitCreateScene");
            return State.Loading;
        }
        else
        {
            CreateScene();
            return State.None;
        }
    }

    /// <summary>
    /// データの削除
    /// </summary>
    private void UnLoadDestroy()
    {
        Destroy(m_scene);
        Resources.UnloadUnusedAssets();
    }

    /// <summary>
    /// シーンの生成
    /// </summary>
    private void CreateScene()
    {
        Debug.Log("---Called CreateScene!! " + m_scenePrefabFolder + m_nextScene.ToString());
        m_scene = Instantiate(Resources.Load(m_scenePrefabFolder + m_nextScene.ToString()), Vector3.zero, Quaternion.identity) as GameObject;
        m_scene.name = m_nextScene.ToString();
    }

    IEnumerator WaitCreateScene()
    {
        yield return new WaitForSeconds(WaitTimeData);
        CreateScene();
    }
}
