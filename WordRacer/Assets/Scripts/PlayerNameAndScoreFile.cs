using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerNameAndScoreFile : MonoBehaviour {
    static protected string PlayerName = "Unknown";
    static protected string ScoreFileName = "Scores";

    void Start()
    {
        GameObject.FindGameObjectWithTag("InputName").GetComponent<InputField>().text = PlayerName;
        GameObject.FindGameObjectWithTag("InputScore").GetComponent<InputField>().text = ScoreFileName;
    }

    public void GetInputPlayerName(string name)
    {
        if (name.Equals(""))
        {
            PlayerName = "Unknown";
        }
        else
            PlayerName = name;

       Debug.Log(PlayerName);
    }

    public void GetInputScoreFileName(string fileName)
    {
        if (fileName.Length == 0)
            ScoreFileName = "Scores";
        else
            ScoreFileName = fileName;

        Debug.Log(ScoreFileName);
    }

    public void OnDestroy()
    {
        Debug.Log("name and score were destroyed");
    }

    static public string GetPlayerName()
    {
        return PlayerName;
    }

    static public string GetScoreFileName()
    {
        return ScoreFileName;
    }

}
