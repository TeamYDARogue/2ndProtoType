using UnityEngine;
using System.Collections;

public class Layer2D
{

    private int width; // 幅
    private int height; // 高さ
    private int[] _vals = null; // マップデータ

    public int Width
    {
        get
        {
            return width;
        }
    }
    public int Height
    {
        get
        {
            return height;
        }
    }

    // 作成
    public void Create(int width, int height)
    {
        this.width = width;
        this.height = height;
        _vals = new int[width * height];
    }

    // 値の取得
    // @param x X座標
    // @param y Y座標
    // @return 指定の座標の値 (領域外を指定したら-1)
    public int Get(int x, int y)
    {
        if (x < 0 || x >= width) { return -1; }
        if (y < 0 || y >= height) { return -1; }
        return _vals[y * width + x];
    }

    // 値の設定
    // @param x X座標
    // @param y Y座標
    // @param val 設定する値
    public void Set(int x, int y, int val)
    {
        if (x < 0 || x >= width) { return; }
        if (y < 0 || y >= height) { return; }
        _vals[y * width + x] = val;
    }

    // デバッグ出力
    public void Dump()
    {
        Debug.Log("[Layer2D] (w,h)=(" + width + "," + height + ")");
        for (int y = 0; y < height; y++)
        {
            string s = "";
            for (int x = 0; x < width; x++)
            {
                s += Get(x, y) + ",";
            }
            Debug.Log(s);
        }
    }

    private void Getfirstgid()
    {
        throw new System.NotImplementedException();
    }
}