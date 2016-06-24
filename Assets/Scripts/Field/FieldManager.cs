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

        LoadStage("RB01");
        StartCoroutine(CreateStage());
    }

    public Layer2D Getlayer()
    {
        return layer;
    }

    public void LoadStage(string mapName)
    {
        mapNumricValue.Create(mapName);
        layer = mapNumricValue.GetLayer();
    }

    IEnumerator CreateStage()
    {
        for (int x = 0; x < layer.Width; x++)
        {
            for (int y = 0; y < layer.Height; y++)
            {

                /*
                位置指定
                動的にオブジェクトの生成
                */

                switch (layer.Get(x, y))
                {
                    case 0:
                        break;
                    case 1:
                        Instantiate(ResourceManager.Instance.GetResourceScene("Wall"), new Vector3(x, 1, y), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(ResourceManager.Instance.GetResourceScene("Ground"), new Vector3(x, 0, y), Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(ResourceManager.Instance.GetResourceScene("Player"), new Vector3(x, 1, y), Quaternion.identity);
                        Instantiate(ResourceManager.Instance.GetResourceScene("Ground"), new Vector3(x, 0, y), Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(ResourceManager.Instance.GetResourceScene("Enemy_a"), new Vector3(x, 1, y), Quaternion.identity);
                        Instantiate(ResourceManager.Instance.GetResourceScene("Ground"), new Vector3(x, 0, y), Quaternion.identity);
                        break;
                    case 5:
                        Instantiate(ResourceManager.Instance.GetResourceScene("Exite"), new Vector3(x, 0, y), Quaternion.identity);
                        break;

                }
            }
        }
        yield return null;
    }

}
