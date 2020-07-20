using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public KeyCode pauseButton = KeyCode.Escape;
    bool isPaused;
    [SerializeField]
    GameObject pauseScreen, options;
    void Update()
    {
        if (Input.GetKeyDown(pauseButton))
        {
            isPaused = !isPaused;
            TogglePause();
        }

        Debug.LogWarning(Time.timeScale);

        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    void TogglePause()
    {
        if (isPaused)
        {
            pauseScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
            options.SetActive(false);
        }
    }

    public void Unpause()
    {
        isPaused = false;
        TogglePause();
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Options()
    {
        options.SetActive(true);
        pauseScreen.SetActive(false);
    }

    public void PauseMenu()
    {
        pauseScreen.SetActive(true);
        options.SetActive(false);
    }
}