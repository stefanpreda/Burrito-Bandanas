using UnityEngine;
using System.Collections;

public class RoundsController : MonoBehaviour {

    public int current_round = 0;

    public int getCurrentRound()
    {
        return current_round;
    }

    //TODO: Add score support
    public void increaseRound()
    {
        GameObject.FindGameObjectWithTag("TextController").GetComponent<TextController>().clearInputField();
        current_round++;
        GameObject.FindGameObjectWithTag("WordBuilder").GetComponent<WordBuilder>().ChangeWord();
    }
}
