using UnityEngine;
using System.Collections;
using System.IO;

public class ScoreController : MonoBehaviour {

    public int starting_score = 0;
    public int score_delta = 10;
    public int score_penalty = 10;
    int current_score = 0;
    
	// Use this for initialization
	void Start()
    {
        Debug.Log("PlayerName: " + PlayerNameAndScoreFile.GetPlayerName() + "FileName :" + PlayerNameAndScoreFile.GetScoreFileName());
        current_score = starting_score;
	}
	
    public void updateScore()
    {
        gameObject.GetComponent<GUIText>().text = "Score: " + current_score;
    }

    public void increaseScore(int amount)
    {
        current_score += amount;
    }

    public void applyPenalty()
    {
        int current_round = GameObject.FindGameObjectWithTag("RoundsController").GetComponent<RoundsController>().getCurrentRound();
        current_score -= (score_penalty * (current_round + 1) / 2);
        if (current_score < 0)
            current_score = 0;
        updateScore();
    }

    //TODO: Get player name as well
    public void saveScore(string filename)
    {
        string playerName;
        if (PlayerNameAndScoreFile.GetPlayerName() == "")
            playerName = "Unknown";
        else
            playerName = PlayerNameAndScoreFile.GetPlayerName();

        string s =  playerName + " " + current_score;
        if (!File.Exists(filename))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(filename))
                sw.WriteLine(s);
            return;

        }
        using (StreamWriter sw = File.AppendText(filename))
            sw.WriteLine(s);   
    }
    
    public int getScoreForFillRemaining(float fill)
    {
        int current_round = GameObject.FindGameObjectWithTag("RoundsController").GetComponent<RoundsController>().getCurrentRound();
        int score_bonus = (int)(fill * 10);
        int round_modifier = (current_round + 1) % 10;
        return (score_delta + score_bonus) * round_modifier;
    }
}
