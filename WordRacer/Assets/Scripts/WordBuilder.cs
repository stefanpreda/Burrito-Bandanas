using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordBuilder : MonoBehaviour {

    List<GameObject> currentWord;

    public float start_positionX = 0.5f;
    public float start_positionY = 0.4f;

	void Start () {
        currentWord = new List<GameObject>();
        ChangeWord();   
	}

    void Update()
    {
        //ChangeWord();
    }

    void ChangeWord()
    {
        foreach (GameObject o in currentWord)
            Destroy(o);
        currentWord.Clear();

        string new_word = getWord();
        float spacing = 0;

        foreach (char c in new_word)
        {
            GameObject obj = new GameObject();
            obj.AddComponent<GUIText>();
            obj.GetComponent<GUIText>().text = c.ToString();
            obj = Instantiate(obj, new Vector3(start_positionX + spacing, start_positionY, 0), Quaternion.identity, transform) as GameObject;
            spacing += 0.01f;
            currentWord.Add(obj);
        }
    }
	
    string getWord()
    {
        return "Dynamic Current Phrase";
    }
}
