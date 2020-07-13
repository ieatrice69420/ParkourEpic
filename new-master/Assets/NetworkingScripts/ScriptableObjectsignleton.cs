using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance = null;

    public static T instance
    {
        get
        {
            if(_instance == null)
            {
                T[] results = Resources.FindObjectsOfTypeAll<T>();
                if(results.Length == 0)
                {
                    Debug.LogError("ScriptableObjectsignleton -> result type is 0 for type" + typeof(T).ToString() + ".");
                    return null;
                }
                if (results.Length > 1)
                {
                    Debug.LogError("ScriptableObjectsignleton -> result type grather than 1 for type" + typeof(T).ToString() + ".");
                    return null;

                }
                _instance = results[0];
            }
            return _instance;
        }
    }
}
