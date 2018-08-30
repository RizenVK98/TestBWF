using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtons : MonoBehaviour {

    public GameObject PauseMenu;

    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("main_menu");
    }
}
