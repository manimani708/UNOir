using System.Collections.Generic;
using System.IO;
using UnityEngine;


public static class JsonManager
{
    private const string directioryName = "Data";       //保存するディレクトリ名

    //ファイルのパスを取得
    public static string GetFilePath(string fileName)
    {
        string directoryPath = Application.persistentDataPath + "/" + directioryName;

        //ディレクトリが無ければ作成
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        //ファイル名は暗号化する
        //string encryptedFlieName = Encryption.EncryptString(fileName);

        string filePath = directoryPath + "/" + fileName;

        return filePath;
    }


    public static void Save(object obj, string fileName)
    {

        string jsonStr = JsonUtility.ToJson(obj, true);
        //Debug.Log("serialized text = " + jsonStr);

        //jsonを暗号化する
        jsonStr = Encryption.EncryptString(jsonStr);

        string filePath = GetFilePath(fileName);
        //File.WriteAllText(filePath, jsonStr);


       // Debug.Log("saveFilePath = " + filePath);
    }


    public static object Load<T>(string fileName)
    {

        string filePath = GetFilePath(fileName);
        if (!File.Exists(filePath))
        {
           // Debug.Log(fileName + "はありません！");
            return null;
        }

		string jsonStr = "";//= File.ReadAllText(filePath);


        //取得したファイルを複合化
        jsonStr = Encryption.DecryptString(jsonStr);
        //Debug.Log(jsonStr);


        object obj = JsonUtility.FromJson<Serialization<T>>(jsonStr).ToList();
        return obj;
    }

    public static string LoadStr(string fileName)
    {
        string filePath = GetFilePath(fileName);
        if (!File.Exists(filePath))
        {
            //Debug.Log(fileName + "はありません！");
            return null;
        }

		string jsonStr = "bora";// = File.ReadAllText(filePath);

        return jsonStr;
    }

}