using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsMenu : MonoBehaviour
{
    // Voltar ao menu principal
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}