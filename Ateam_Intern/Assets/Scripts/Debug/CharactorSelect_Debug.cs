using UnityEngine;
using System.Collections;

public class CharactorSelect_Debug : MonoBehaviour 
{
    string path = "";
    public void Save()
    {
        var list = CharactorData_Debug.GetList();

        JsonManager.Save(new Serialization<CharactorData>(list), "SelectedCharactor.json");

        path = JsonManager.GetFilePath("SelectedCharactor.json");
    }

    void OnGUI()
    {
        GUILayout.Label(path);
    }

}
