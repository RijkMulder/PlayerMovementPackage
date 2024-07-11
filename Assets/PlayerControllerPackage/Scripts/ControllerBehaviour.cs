using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ControllerBehaviour : MonoBehaviour
{
    private string path = "ComponentSaves";
    public void Load()
    {
        // try to find file
        string filePath = "Assets/PlayerControllerPackage" + "/" + path + "/" + this.GetType().Name + ".json";
        TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>(filePath);
        if (file)
        {
            string json = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(json, this);
        }

        // Save instead if no files are found
        Save();
    }

    public void Save()
    {
        // delete old file and make new
        string json = JsonUtility.ToJson(this, true);
        string filePath = "Assets/PlayerControllerPackage" + "/" + path + "/" + this.GetType().Name + ".json";

        TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>(filePath);
        if (file)
        {
            File.Delete(filePath);
        }
        File.WriteAllText(filePath, json);
    }
}
