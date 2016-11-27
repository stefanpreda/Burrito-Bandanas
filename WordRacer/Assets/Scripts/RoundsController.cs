using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RoundsController : MonoBehaviour {

    public int starting_round = 0;
    int current_round = 0;
    public float start_seconds = 20.0f;
    float current_seconds = 20.0f;

    public void Start()
    {
        current_round = starting_round;
        current_seconds = start_seconds;
    }

    public int getCurrentRound()
    {
        return current_round;
    }

    public float getSecondsForRound(int round)
    {
        float time_reduction = 5;
        if (round != 0)
            time_reduction = time_reduction / round;
        current_seconds = current_seconds - time_reduction;
        return current_seconds;
    }

    public void increaseRound()
    {
        //Clear input
        GameObject.FindGameObjectWithTag("TextController").GetComponent<TextController>().clearInputField();

        //Increase round
        current_round++;

        //Build a new word
        GameObject.FindGameObjectWithTag("WordBuilder").GetComponent<WordBuilder>().ChangeWord();

        //Restart timer
        GameObject.FindGameObjectWithTag("TimerController").GetComponent<TimerController>().stopTimer();
        float seconds = getSecondsForRound(current_round);
        GameObject.FindGameObjectWithTag("TimerController").GetComponent<TimerController>().resetTimer(seconds);
        GameObject.FindGameObjectWithTag("TimerController").GetComponent<TimerController>().startTimer();

    }

    public void winCurrentRound()
    {
        //Get remaining percentage of time
        float remaining_fill = GameObject.FindGameObjectWithTag("TimerController").GetComponent<TimerController>().getRemainingFill();

        //Compute and update score
        int score_gained = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreController>().getScoreForFillRemaining(remaining_fill);
        GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreController>().increaseScore(score_gained);
        GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreController>().updateScore();

        //Initiate next round
        increaseRound();
    }

    public void loseCurrentRound()
    {
        //Stop timer
        GameObject.FindGameObjectWithTag("TimerController").GetComponent<TimerController>().stopTimer();

        //Apply score penalty
        GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreController>().applyPenalty();
    
        increaseRound();
    }

    public void applyMistake()
    {
        //Adjust health
        GameObject.FindGameObjectWithTag("HealthController").GetComponent<HealthController>().loseHealth();
        if (GameObject.FindGameObjectWithTag("HealthController").GetComponent<HealthController>().getCurrentHealth() == 0)
        {
            //Stop timer
            GameObject.FindGameObjectWithTag("TimerController").GetComponent<TimerController>().stopTimer();

            //Save score
            GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreController>().saveScore("Scores.txt");

            SceneManager.LoadScene("EndScene");
        }
    }
}
