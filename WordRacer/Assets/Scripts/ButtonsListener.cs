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
        Debug.Log("Restart pressed");
        SceneManager.LoadScene("GameScene");
    }


    //TODO: We don't have menu yet
    public void ReturnToMenuButtonPressed()
    {
        Debug.Log("Return pressed");
        SceneManager.LoadScene("StartScene");
    }

    public void NewGrameButtonPressed()
    {
        Debug.Log("New Game pressed");
        SceneManager.LoadScene("GameScene");
    }

    public void SettingsButtonPressed()
    {
        Debug.Log("Settings pressed");
    }
}
