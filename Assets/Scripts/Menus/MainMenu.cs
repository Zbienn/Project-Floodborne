using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() => SceneManager.LoadScene("Level1");

    public void OpenMainMenu() => SceneManager.LoadScene("MainMenu");

    public void ResetTime() => Time.timeScale = 1f;

    public void QuitGame()
    {
        Debug.Log("SAIR DO JOGO");
        Application.Quit();
    }
}
