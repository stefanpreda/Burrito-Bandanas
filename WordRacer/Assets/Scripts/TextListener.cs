using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextListener : MonoBehaviour {

    public void TextChanged(string newText)
    {
        string reference_string = GameObject.FindGameObjectWithTag("WordBuilder").GetComponent<WordBuilder>().getCurrentWord();
        if (newText.Equals(reference_string))
            clearInputField();
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
}
