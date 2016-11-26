using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    public void TextChanged(string newText)
    {
        string reference_string = GameObject.FindGameObjectWithTag("WordBuilder").GetComponent<WordBuilder>().getCurrentWord();
        if (!reference_string.StartsWith(newText))
        {
            GameObject.FindGameObjectWithTag("RoundsController").GetComponent<RoundsController>().applyMistake();
            string s = GameObject.FindGameObjectWithTag("Input").GetComponent<InputField>().text;
            GameObject.FindGameObjectWithTag("Input").GetComponent<InputField>().text = s.Substring(0, s.Length - 1);
        }
        if (newText.Equals(reference_string))
            triggerNextRound();
    }

    public void setInputFieldText(string text)
    {
        InputField field = GameObject.FindGameObjectWithTag("Input").GetComponent<InputField>();
        field.text = text;
    }

    public void clearInputField()
    {
        InputField field = GameObject.FindGameObjectWithTag("Input").GetComponent<InputField>();
        field.text = "";
    }

    public void triggerNextRound()
    {
        GameObject.FindGameObjectWithTag("RoundsController").GetComponent<RoundsController>().winCurrentRound();
    }
}
