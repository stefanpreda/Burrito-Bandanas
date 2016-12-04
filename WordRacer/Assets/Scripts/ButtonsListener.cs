using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonsListener : MonoBehaviour {

    public void ExitButtonPressed()
    {
        Application.Quit();
    }

    public void RestartButtonPressed()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ReturnToMenuButtonPressed()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void NewGrameButtonPressed()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void SettingsButtonPressed()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void ScoreboardButtonPressed()
    {
        SceneManager.LoadScene("ScoreboardScene");
    }
}
