using UnityEngine;
using static Saving.SaveKeyData;

public class KeyManager : MonoBehaviour
{
    public static KeyManager instance;
    [SerializeField]
    Keys keys;

    public void ManagerSaveKeys()
    {
        KeyData data = LoadKeys();
        SaveKeys(keys);
        Debug.Log(TempKeys.GetKey(0));
    }
}