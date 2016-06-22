using UnityEngine;
using System.Xml;
using System.Text;
using System.Collections;
using System.IO;

public class FieldManager : MonoBehaviour {

    /*
    FiledManager:フィールド管理
    MapNumericValue:外部テキスト読み込み
    Layer2D:データを格納しとく場所
　　シングルトン化
    */

    private object mapData;
    private MapNumericValue mapnumricvalue;
    private Layer2D layer;
    private static FieldManager m_instance = null;
    public static FieldManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                /*
                compornet 　スクリプトからGameObjectにアタッチするため

                */

                var obj = new GameObject("FieldManager!!");
                m_instance = obj.AddComponent<FieldManager>();
                m_instance.GetComponent<FieldManager>().enabled = true;

            }
            return m_instance;
        }
    }


    void Start()
    {
        mapnumricvalue = new MapNumericValue();
        layer = new Layer2D();
        Create();
        ResourceManager.Instance.ResourcesLoad("Game");
      
        for (int x=0;x<layer.Width;x++)
        {
            for (int y=0;y<layer.Height;y++)
            {

                /*
                位置指定
                動的にオブジェクトの生成
                */

                switch (layer.Get( x , y))
                {
                    case 0:
                        //Debug.Log(" ");
                        break;
                    case 1:
                        Instantiate(ResourceManager.Instance.GetResourceScene("Wall"), new Vector3(x, 1, y), Quaternion.identity);
                        //Debug.Log("W");
                        break;
                    case 2:
                        Instantiate(ResourceManager.Instance.GetResourceScene("Ground"), new Vector3(x, 0, y), Quaternion.identity);
                        //Debug.Log("G");
                        break;
                    case 3:
                        Instantiate(ResourceManager.Instance.GetResourceScene("Player"), new Vector3(x, 1, y), Quaternion.identity);
                        Instantiate(ResourceManager.Instance.GetResourceScene("Ground"), new Vector3(x, 0, y), Quaternion.identity);
                        //Debug.Log("P");
                        break;
                    case 4:
                        Instantiate(ResourceManager.Instance.GetResourceScene("Enemy_a"), new Vector3(x, 1, y), Quaternion.identity);
                        Instantiate(ResourceManager.Instance.GetResourceScene("Ground"), new Vector3(x, 0, y), Quaternion.identity);
                        //Debug.Log("EN");
                        break;
                    case 5:
                        //Debug.Log("EX");
                        Instantiate(ResourceManager.Instance.GetResourceScene("Exite"), new Vector3(x, 0, y), Quaternion.identity);
                        break;

                }
            }
        }
        
             
    }
   
    public Layer2D Getlayer()
    {
        return layer;
    }
    public void Create()
    {

        mapnumricvalue.Create("RB01");

        layer = mapnumricvalue.GetLayer();
    }
    
   
}
