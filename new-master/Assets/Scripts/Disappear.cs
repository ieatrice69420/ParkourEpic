using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    void OnEnable() => StartCoroutine(Disable());

    IEnumerator Disable()
    {
    	yield return new WaitForSeconds(2f);
    	gameObject.SetActive(false);
    }
}
