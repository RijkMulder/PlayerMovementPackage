using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ControllerBehaviour : MonoBehaviour
{
    private string path = "ComponentSaves";
    public void Load()
    {
        // try to find file
        string filePath = "Assets/Resources" + "/" + path + "/" + this.GetType().Name + ".json";
        if (File.Exists(filePath))
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
        string json = JsonUtility.ToJson(this);
        string filePath = "Assets/Resources" + "/" + path + "/" + this.GetType().Name + ".json";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        File.WriteAllText(filePath, json);
    }
}
