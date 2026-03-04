using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MapSelect");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void SettingsMenu()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene("SettingsMenu");
    }

    public void CreditMenu()
    { 
        Time.timeScale = 1f;
        SceneManager.LoadScene("CreditMenu");
    }

    // all the levels

    public void Level1()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2");
    }

    public void Level3()
    { 
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level3");
    }
}
