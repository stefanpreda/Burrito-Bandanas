using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordBuilder : MonoBehaviour {

    List<GameObject> currentWord;
    string currentWord_string;

    public float start_positionX = 0.5f;
    public float start_positionY = 0.4f;
    public float word_spacing = 0.005f;
    public int text_size_divisor = 25;

    public Camera camera = null;

	void Start () {
        currentWord = new List<GameObject>();
        ChangeWord();   
	}

    //TODO: Change text fonts and stuff based on current round
    public void ChangeWord()
    {
        ClearWord();

        int round = GameObject.FindGameObjectWithTag("RoundsController").GetComponent<RoundsController>().getCurrentRound();
        string new_word = getWord(round);
        currentWord_string = new_word;
    
        float spacing = 0;
        foreach (char c in new_word)
        {
            GameObject obj = new GameObject();
            obj.AddComponent<GUIText>();
            obj.GetComponent<GUIText>().text = c.ToString();
            obj = Instantiate(obj, new Vector3(start_positionX + spacing, start_positionY, 0), Quaternion.identity, transform) as GameObject;

            //Scale to screen size
            obj.GetComponent<GUIText>().fontSize = Screen.width / text_size_divisor;

            Rect r = obj.GetComponent<GUIText>().GetScreenRect();
            Vector3 posX = camera.ScreenToViewportPoint(new Vector3(r.x, r.y, 0f));
            Vector3 max_posX = camera.ScreenToViewportPoint(new Vector3(r.xMax, r.y, 0f));

            //Adding character lengths
            spacing += max_posX.x - posX.x;

            //Adding extra space between characters
            spacing += word_spacing;
            currentWord.Add(obj);
        }
    }

    public void ClearWord()
    {
        foreach (GameObject o in currentWord)
            Destroy(o);
        currentWord.Clear();
    }
	
    //TODO: Get a words from a dictionary or something according to current round
    string getWord(int round)
    {
        string[] word = { "Dynamically generated phrase one", "Dynamically generated phrase 2", "Dynamically generated phrase 3" };
        round = round % 3;
        return word[round];
    }

    public string getCurrentWord()
    {
        return currentWord_string;
    }
}
