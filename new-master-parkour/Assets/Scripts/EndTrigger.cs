using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public GameObject CompletLevelUi;

    public void CompletLevel() => CompletLevelUi.SetActive(true);

    private void OnTriggerEnter(Collider other) => CompletLevel();
}
