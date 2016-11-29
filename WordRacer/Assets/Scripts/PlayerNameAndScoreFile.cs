using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerNameAndScoreFile : MonoBehaviour {
    static protected string PlayerName;
    static protected string ScoreFileName;

    public void GetInputPlayerName(string name)
    {
        PlayerName = name;
        Debug.Log(PlayerName);
    }

    public void GetInputScoreFileName(string fileName)
    {
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
