using System;
using UnityEngine;

[Serializable]
public class KeyData
{
	public KeyCode[] keyCodes;

    public KeyData(Keys keyManager)
    {
        for (int i = 0; i < keyManager.keys.Length; i++)
        {
            keyCodes[i] = keyManager.keys[i];
            TempKeys.SetKey(i, keyManager.keys[i]);
        }
    }
}
