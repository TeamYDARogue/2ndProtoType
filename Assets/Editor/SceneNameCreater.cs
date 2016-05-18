using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

public class SceneNameCreater : MonoBehaviour {


    /// <summary>
    /// 無効な文字列を指定
    /// </summary>
    private static readonly string[] INVALUD_CHARS =
    {
        " ", "!", "\"", "#", "$",
        "%", "&", "\'", "(", ")",
        "-", "=", "^",  "~", "\\",
        "|", "[", "{",  "@", "`",
        "]", "}", ":",  "*", ";",
        "+", "/", "?",  ".", ">",
        ",", "<"
    };


    /// コマンド名
    private const string ITEM_NAME = "Tools/Create/Scene Name";
    
    /// ファイルのパス
    private const string PATH = "Assets/Script/Scene/SceneNameManager.cs";

    /// ファイルの名前(拡張子なし)
    private static readonly string FILENAME = Path.GetFileName(PATH);
    
    /// ファイルの名前(拡張子あり)
    private static readonly string FILENAME_WITHOUT_EXTENSION = Path.GetFileNameWithoutExtension(PATH);

    

    /// <summary>
    /// 実際にSceneNameManagerを作る
    /// </summary>
    [MenuItem(ITEM_NAME)]
    public static void Create()
    {
        if(!CanCreate())
        {
            EditorUtility.DisplayDialog(FILENAME, "作成失敗", "OK");
            return;
        }

        CreateScript();
        /// メッセージダイアログを表示
        EditorUtility.DisplayDialog(FILENAME, "作成完了", "OK");
    }

    /// <summary>
    /// スクリプトの中身を作成
    /// </summary>
    public static void CreateScript()
    {
        var builder = new StringBuilder();

        builder.AppendFormat("public class {0}", FILENAME_WITHOUT_EXTENSION).AppendLine();
        builder.AppendLine("{");

        builder.AppendLine("    public enum Scene");
        builder.AppendLine("    {");


        foreach (var num in EditorBuildSettings.scenes
            .Select(c => Path.GetFileNameWithoutExtension(c.path))
            .Distinct()
            .Select(c => new { var = RemoveInvalidChars(c), val = c }))
        {
            builder.Append("\t").AppendFormat(@"    {0},", num.var).AppendLine();
        }

        builder.AppendLine("    }");
        builder.AppendLine("}");

        var directoryName = Path.GetDirectoryName(PATH);
        if(!Directory.Exists(directoryName))
        {
            Directory.CreateDirectory(directoryName);
        }

        File.WriteAllText(PATH, builder.ToString(), Encoding.UTF8);
        AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
    }


    /// <summary>
    /// SceneNameManagerクラスが生成できるかを判定
    /// </summary>
    /// <returns>作業状態</returns>
    [MenuItem(ITEM_NAME,true)]
    public static bool CanCreate()
    {
        return !EditorApplication.isPlaying && !Application.isPlaying && !EditorApplication.isCompiling;
    }


    /// <summary>
    /// 無効な文字列の削除
    /// </summary>
    /// <param name="str">文字</param>
    /// <returns>削除後の文字</returns>
    public static string RemoveInvalidChars(string str)
    {
        System.Array.ForEach(INVALUD_CHARS, c => str = str.Replace(c, string.Empty));
        return str;
    }
}
