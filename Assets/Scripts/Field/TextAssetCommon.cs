using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
/// <summary>
/// テキストアセットのユーティリティクラス
/// パスを設定し、Resources フォルダーにあるアセットをロードします
/// typeを見つけた場合は、アセットを返す。
/// それ以外の場合は null を返します。 このパラメーターがある場合、type に一致するオブジェクトのみ返す。
/// path はプロジェクトの Assets フォルダー内にある Resources フォルダーから相対的なパスとなり、拡張子は省略。
/// </summary>
public sealed class TextAssetCommon
{ 
    /**
    * @brief コンストラクタ
    */
    private TextAssetCommon() { }  
    /**
    * @brief テキストデータの読み込みを行う関数
    * @param string テキストデータのファイルパス
    * @return string テキストデータ
    */
    public static string ReadText(string folderPath="RB01")
    {
        if (folderPath != null)
        {
            TextAsset textAsset = Resources.Load(folderPath) as TextAsset;
            string text = textAsset.text;
            return text;
        }
        else
        {
            Debug.Log("TextAssetCommon.ReadText : filePath is null .");
            return null;
        }
    }
    
    /**
    * @brief 改行を区切り文字として分割したテキストデータを取得する関数
    * @param string テキストデータ
    * @return string[] 分割したテキストデータ
    */
    public static string[] GetTextLines(string t_text)
    {
        if (t_text != null)
        {
            // OS環境ごとに適切な改行コードをCR(=キャリッジリターン)に置換.
            string text = t_text.Replace(Environment.NewLine, "\r");
            // テキストデータの前後からCRを取り除く.
            text = text.Trim('\r');
            // CRを区切り文字として分割して配列に変換.
            string[] textLines = text.Split('\r');
            return textLines;
        }
        else
        {
            Debug.Log("TextAssetCommon.GetTextLines : text is null .");
            return null;
        }
    }
   
}
