using UnityEngine;
using System.Collections;

public class FieldManager : MonoBehaviour
{
    /*
    FiledManager:フィールド管理
    MapNumericValue:外部テキスト読み込み
    Layer2D:データを格納しとく場所
　　シングルトン化
    */
    private object mapData;
    private MapNumericValue mapNumricValue;
    private Layer2D layer;
    private static FieldManager instance = null;
    public static FieldManager Instance
    {
        get
        {
            if (instance == null)
            {
                /*
                compornet 　スクリプトからGameObjectにアタッチするため
                */
                var obj = new GameObject("FieldManager!!");
                instance = obj.AddComponent<FieldManager>();
                instance.GetComponent<FieldManager>().enabled = true;
            }
            return instance;
        }
    }


    void Start()
    {
        ResourceManager.Instance.ResourcesLoad("Game");
        mapNumricValue = new MapNumericValue();
        layer = new Layer2D();
        /*

        //読み込みテスト完了なデータ

        LoadStage("Stage02_Ground");
        LoadStage("Stage02_Main");
        */

        LoadStage("RB01_Ground");
        LoadStage("RB01_Main");
    }

    public Layer2D Getlayer()
    {
        return layer;
    }

    public void LoadStage(string mapName)
    {
        mapNumricValue.Create(mapName);
        layer = mapNumricValue.GetLayer();
        StartCoroutine(CreateStage());

    }


    IEnumerator CreateStage()
    {
        GameObject wall = new GameObject("WallParent");
        GameObject ground = new GameObject("GroundParent");
        for (int x = 0; x < layer.Width; x++)
        {
            for (int y = 0; y < layer.Height; y++)
            {
                switch (layer.Get(x, y))
                {
                    case 0:
                        break;
                    case 1:
                        //壁
                        GameObject wallObj = Instantiate(ResourceManager.Instance.GetResourceScene("Wall"), new Vector3(x, 1, y), Quaternion.identity) as GameObject ;
                        wallObj.transform.parent = wall.transform;
                        break;
                    case 2:
                        //床
                        GameObject groundObj = Instantiate(ResourceManager.Instance.GetResourceScene("Ground"), new Vector3(x, 0, y), Quaternion.identity) as GameObject;
                        groundObj.transform.parent = ground.transform;
                        break;
                    case 3:
                        //プレイヤー
                        Instantiate(ResourceManager.Instance.GetResourceScene("Player"), new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case 4:
                        //敵A
                        Instantiate(ResourceManager.Instance.GetResourceScene("Enemy_a"), new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case 5:
                        //出口
                        Instantiate(ResourceManager.Instance.GetResourceScene("Exit"), new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case 6:
                        //透明な壁
                        Instantiate(ResourceManager.Instance.GetResourceScene("CleannessWall"), new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case 7:
                        //壺
                        Instantiate(ResourceManager.Instance.GetResourceScene("Vase"), new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case 8:
                        //武器
                        Instantiate(ResourceManager.Instance.GetResourceScene("Weapon"), new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case 9:
                        //槍
                        Instantiate(ResourceManager.Instance.GetResourceScene("Spear"), new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case 10:
                        //スイッチボタン
                        Instantiate(ResourceManager.Instance.GetResourceScene("Switch"), new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case 11:
                        //敵B
                        Instantiate(ResourceManager.Instance.GetResourceScene("Enemy_b"), new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case 12:
                        //セーブ
                        Instantiate(ResourceManager.Instance.GetResourceScene("Save"), new Vector3(x, 1, y), Quaternion.identity);
                        break;

                }
            }
        }
        yield return null;
    }
}
