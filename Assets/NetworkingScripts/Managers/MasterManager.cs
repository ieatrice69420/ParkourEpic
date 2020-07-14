using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "SingletonS/MasterManager")]
public class MasterManager : SingletonScriptableObject<MasterManager>
{
    [SerializeField]
    private GameSettings _gameSettings;
    public static GameSettings GameSettings { get { return instance._gameSettings; } }

    private List<NetworkPrefab> _networkprefab = new List<NetworkPrefab>();
//dddddddddddddddd

    public static GameObject NetworkInstantiate(GameObject obj, Vector3 position, Quaternion rotation)
    {
        foreach (NetworkPrefab NetworkPrefab in instance._networkprefab)
        {
            if(NetworkPrefab.prefab == obj)
            {
                if (NetworkPrefab.Path != string.Empty)
                {
                    GameObject result = PhotonNetwork.Instantiate(NetworkPrefab.Path, position, rotation);
                    return result;
                }
                else
                    Debug.LogError("Path is empty " + NetworkPrefab.prefab);
                return null;
            }
        }
        return null;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void populateNetworkedPrefabs()
    {
        if (!Application.isEditor)
            return;
        GameObject[] result = Resources.LoadAll<GameObject>("");
        for (int i = 0; i < result.Length; i++)
        {
            if(result[i].GetComponent<PhotonView>() != null)
            {
                string path = AssetDatabase.GetAssetPath(result[i]);
                instance._networkprefab.Add(new NetworkPrefab(result[i],path));
            }
        }

        for (int i = 0; i < instance._networkprefab.Count; i++)
        {
            UnityEngine.Debug.Log(instance._networkprefab[i].prefab.name + "," + instance._networkprefab[i].Path);
        }
    }
}
