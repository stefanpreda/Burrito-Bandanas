using UnityEngine;
using System.Collections;

public class RoundsController : MonoBehaviour {

    public int starting_round = 0;
    int current_round = 0;
    public float start_seconds = 10.0f;

    public void Start()
    {
        current_round = starting_round;
    }

    public int getCurrentRound()
    {
        return current_round;
    }

    //TODO: Reduce seconds after each round + hard/soft cap
    public float getSecondsForRound(int round)
    {
        return start_seconds;
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
        float remaining_fill = GameObject.FindGameObjectWithTag("TimerController").GetComponent<TimerController>().getRemainingFill();
        Debug.Log(remaining_fill);
        int score_gained = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreController>().getScoreForFillRemaining(remaining_fill);
        GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreController>().increaseScore(score_gained);
        GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreController>().updateScore();
        increaseRound();
    }

    //TODO: Add lives
    public void loseCurrentRound()
    {
        increaseRound();
    }
}
