using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

    public int starting_score = 0;
    public int score_delta = 10;
    int current_score = 0;

	// Use this for initialization
	void Start()
    {
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

    //TODO: Save player name + score
    public void saveScore(string filename)
    {

    }
    
    public int getScoreForFillRemaining(float fill)
    {
        int score_bonus = (int)(fill * 10);
        return score_delta + score_bonus;
    }
}
