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
                        Debug.Log(" ");
                        break;
                    case 1:
                        /*
                        //時間で表示
                        Invoke("",5);
                        */
                        Debug.Log("W");
                        break;
                    case 2:
                        Debug.Log("G");
                        break;
                    case 3:
                        Debug.Log("P");
                        break;
                    case 4:
                        Debug.Log("EN");
                        break;
                    case 5:
                        Debug.Log("EX");
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
