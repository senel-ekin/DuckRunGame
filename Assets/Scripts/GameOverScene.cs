using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Level01");
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Oyun kapatýldý!");
    }
}
