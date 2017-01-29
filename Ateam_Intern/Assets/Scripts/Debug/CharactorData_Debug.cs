using UnityEngine;
using System.Collections.Generic;

public static class CharactorData_Debug 
{
    private static List<CharactorData> charactorList = new List<CharactorData>();

    public static void AddData(CharactorData charactorData)
    {
        string json = JsonUtility.ToJson(charactorData, true);
        //Debug.Log(json);
        charactorList.Add(charactorData);
    }

    public static List<CharactorData> GetList() { return charactorList;}
}
