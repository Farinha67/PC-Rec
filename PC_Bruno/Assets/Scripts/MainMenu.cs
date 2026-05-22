using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Começar jogo
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    // Sair do jogo
    public void QuitGame()
    {
        Debug.Log("Saiu do jogo");

        Application.Quit();
    }
}