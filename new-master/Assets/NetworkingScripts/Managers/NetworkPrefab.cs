using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class NetworkPrefab 
{
    public GameObject prefab;
    public string Path; 

    public NetworkPrefab (GameObject obj, string path)
    {
        prefab = obj;
        Path = ReturenPrefabPath(path);
    }

    private string ReturenPrefabPath(string path)
    {
        int extensionLenght = System.IO.Path.GetExtension(path).Length;
        int startindex = path.ToLower().IndexOf("resources");

        if (startindex == -1)
        {
            return string.Empty;
        }
        else
            return path.Substring(startindex, path.Length - (startindex + extensionLenght));
    }
}
