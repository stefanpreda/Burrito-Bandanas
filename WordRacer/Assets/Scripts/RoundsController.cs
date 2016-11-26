using UnityEngine;
using System.Collections;

public class RoundsController : MonoBehaviour {

    public int current_round = 0;
    public float start_seconds = 10.0f;

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

    //TODO: Add score support
    public void winCurrentRound()
    {
        increaseRound();
    }

    //TODO: Add lives
    public void loseCurrentRound()
    {
        increaseRound();
    }
}
