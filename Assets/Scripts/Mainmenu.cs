using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public void PlaySinglePlayer() => SceneManager.LoadScene(3);

    public void PlayMultiPlayer() => SceneManager.LoadScene(1);

    public void QuitGame() => Application.Quit();
}
