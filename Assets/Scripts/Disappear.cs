using System.Collections;
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
