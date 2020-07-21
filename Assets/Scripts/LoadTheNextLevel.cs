using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadTheNextLevel : MonoBehaviour
{
    public void LoadNextLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}
