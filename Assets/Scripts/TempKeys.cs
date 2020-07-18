using UnityEngine;
using static Saving.SaveKeyData;

public static class TempKeys
{
    public static KeyCode[] keyCodes;

    public static void SetKey(int keyIndex, KeyCode key) => keyCodes[keyIndex] = key;

    public static KeyCode GetKey(int keyIndex)
    {
        return keyCodes[keyIndex];
    }

    public static void OnLoadKeys()
    {
        KeyData data = LoadKeys();
    }
}
