    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class TheGroundISLava : MonoBehaviour
    {
        [SerializeField]
        int index;

        public void OnTriggerEnter(Collider other)
        {
            SceneManager.LoadSceneAsync(index);
        }
    }
