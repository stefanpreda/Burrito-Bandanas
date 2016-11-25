using UnityEngine;
using System.Collections;

public class TextListener : MonoBehaviour {

    public void TextChanged(string newText)
    {
        Debug.Log(newText);
    }
}
