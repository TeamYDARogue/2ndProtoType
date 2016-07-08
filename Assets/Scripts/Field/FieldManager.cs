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

    enum FieldType
    {
        None = 0,
        Wall = 1,
        CleannessWall = 2,
        Ground = 3,
        Vase = 4,
        SwitchButton = 5,
        Spear = 6,
        Save = 7,
        Exit = 8,
        Weapon = 9,
        Player = 10,
        Enemy_a = 11,
        Enemy_b = 12,
    }


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
        LoadStage("Stage01_Ground");
        LoadStage("Stage01_Main");
        */

        LoadStage("Stage02_Ground");
        LoadStage("Stage02_Main");
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
                    case (int)FieldType.None:
                        break;
                    case 1:
                        //壁
                        GameObject wallObj = Instantiate(ResourceManager.Instance.GetResourceScene("Wall"), new Vector3(x, 1, y), Quaternion.identity) as GameObject ;
                        wallObj.transform.parent = wall.transform;
                        break;
                    case (int)FieldType.CleannessWall:
                        //透明な壁
                        Instantiate(ResourceManager.Instance.GetResourceScene("CleannessWall"), new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case (int)FieldType.Ground:
                        //床
                        GameObject groundObj = Instantiate(ResourceManager.Instance.GetResourceScene("Ground"), new Vector3(x, 0, y), Quaternion.identity) as GameObject;
                        groundObj.transform.parent = ground.transform;
                        break;
                    case (int)FieldType.Vase:
                        //壺
                        Instantiate(ResourceManager.Instance.GetResourceScene("Vase"), new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case (int)FieldType.SwitchButton:
                        //スイッチボタン
                        Instantiate(ResourceManager.Instance.GetResourceScene("Switch"), new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case (int)FieldType.Spear:
                        //槍
                        Instantiate(ResourceManager.Instance.GetResourceScene("Spear"), new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case (int)FieldType.Save:
                        //セーブ
                        Instantiate(ResourceManager.Instance.GetResourceScene("Save"), new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case (int)FieldType.Exit:
                        //出口
                        Instantiate(ResourceManager.Instance.GetResourceScene("Exit"), new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case (int)FieldType.Weapon:
                        //武器
                        Instantiate(ResourceManager.Instance.GetResourceScene("Weapon"), new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case (int)FieldType.Player:
                        //プレイヤー
                        Instantiate(ResourceManager.Instance.GetResourceScene("Player"), new Vector3(x, 0.5f, y), Quaternion.identity);
                        break;
                    case (int)FieldType.Enemy_a:
                        //敵A
                        Instantiate(ResourceManager.Instance.GetResourceScene("Enemy_a"), new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case (int)FieldType.Enemy_b:
                        //敵B
                        Instantiate(ResourceManager.Instance.GetResourceScene("Enemy_b"), new Vector3(x, 1, y), Quaternion.identity);
                        break;
                }
            }
        }
        yield return null;
    }
}
