using UnityEngine;
using static Saving.SaveKeyData;

public class KeyManager : MonoBehaviour
{
    [SerializeField]
    Keys keys;

    public void ManagerSaveKeys()
    {
        SaveKeys(keys);
    }
}
