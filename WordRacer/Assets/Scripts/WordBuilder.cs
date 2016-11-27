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

        //List of indices which will have certain transformations applied on
        List<int> selected_indices = new List<int>();

        //Select some random indices
        for (int i = 0; i < 3 * new_word.Length / 4; i++)
            selected_indices.Add(Random.Range(0, new_word.Length));
 
        float spacing = 0;
        int index = 0;
        foreach (char c in new_word)
        {
            GameObject obj = new GameObject();
            Quaternion rotation = Quaternion.identity;
            obj.AddComponent<GUIText>();
            obj.GetComponent<GUIText>().text = c.ToString();

            //Scale to screen size
            obj.GetComponent<GUIText>().fontSize = Screen.width / text_size_divisor;

            //Apply color changes
            if (round > 0)
            {     
                obj.GetComponent<GUIText>().color = new Color(Random.Range(0, 255) / 255.0f, Random.Range(0, 255) / 255.0f, Random.Range(0, 255) / 255.0f, 1.0f);
            }

            //Apply random scales
            if (round > 1)
            {
                obj.GetComponent<GUIText>().fontSize = Screen.width / Random.Range(text_size_divisor - 10, text_size_divisor + 10);
            }

            //Apply flips
            //FIXME: Rotations don't work...
            if (round == 0)
            {
                if (selected_indices.Contains(index)) {
                    obj.GetComponent<GUIText>().transform.Rotate(new Vector3(0, 90, 0));
                }
            }

            //Apply rotations with fixed angle
            /*if (round == 3)
            {
                if (selected_indices.Contains(index))
                {
                    int rotation_angle_index = Random.Range(0, 8);
                    int angle = 0;
                    if (rotation_angle_index < 5)
                        angle = rotation_angle_index * 90;
                    else
                        angle = (rotation_angle_index - 4) * (-90);
                    Debug.Log("" + rotation_angle_index + " " + angle);
                    Vector3 newScale = obj.transform.localScale;
                    newScale.x *= -1;
                    obj.transform.localScale = newScale;
                }

            }*/

            obj = Instantiate(obj, new Vector3(start_positionX + spacing, start_positionY, 0), rotation) as GameObject;

            Rect r = obj.GetComponent<GUIText>().GetScreenRect();
            Vector3 posX = camera.ScreenToViewportPoint(new Vector3(r.x, r.y, 0f));
            Vector3 max_posX = camera.ScreenToViewportPoint(new Vector3(r.xMax, r.y, 0f));

            //Adding character lengths
            spacing += max_posX.x - posX.x;

            //Adding extra space between characters
            spacing += word_spacing;
            currentWord.Add(obj);

            index++;
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
        string[] word = { "Dynamically generated phrase 1", "Dynamically generated phrase 2", "Dynamically generated phrase 3" };
        round = round % 3;
        return word[round];
    }

    public string getCurrentWord()
    {
        return currentWord_string;
    }
}
