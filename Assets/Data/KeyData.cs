using System;
using UnityEngine;

[Serializable]
public class KeyData
{
	public KeyCode[] keyCodes = new KeyCode[1];

    public KeyData(Keys keyManager)
    {
        for (int i = 0; i < keyManager.keys.Length - 1; i++)
        {
            keyCodes[i] = keyManager.keys[i];
            TempKeys.SetKey(i, keyManager.keys[i]);
        }
    }
}