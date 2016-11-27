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
    public string sortingLayerName = "Default";
    public int sortingOrder = 0;
    private string[] files = { "easy words", "hard words", "master words" };
    public Camera camera = null;

    void Start () {
        currentWord = new List<GameObject>();
        ChangeWord();   
    }

    private string Load(string fileContent) {
        string result = "";
        string[] words = fileContent.Split('\n');
        int number;
        for (int i = 0; i < 3; i++) {

            number = UnityEngine.Random.Range(1, 800);
            result += words[number].Substring(0,words[number].Length - 1) + " ";
        }
        number = UnityEngine.Random.Range(1, 800);
        result += words[number].Substring(0, words[number].Length - 1);
        return result;
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
            obj.AddComponent<TextMesh>();
            obj.GetComponent<TextMesh>().text = c.ToString();
            obj.GetComponent<TextMesh>().characterSize = 0.1f;

            //Draw this on top of other components
            obj.GetComponent<MeshRenderer>().sortingLayerName = sortingLayerName;
            obj.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;

            //Scale to screen size
            obj.GetComponent<TextMesh>().fontSize = Screen.width / text_size_divisor;

            //Apply color changes
            if (round > 0)
            {     
                obj.GetComponent<TextMesh>().color = new Color(Random.Range(0, 255) / 255.0f, Random.Range(0, 255) / 255.0f, Random.Range(0, 255) / 255.0f, 1.0f);
            }

            //Apply random scales
            if (round > 1)
            {
                obj.GetComponent<TextMesh>().fontSize = Screen.width / Random.Range(text_size_divisor - 10, text_size_divisor + 10);
            }

            //Apply flips
            //FIXME: Rotations don't work...
            if (round == 0)
            {
                //obj.transform.Rotate(0.0f, 0.0f, 90.0f);
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
            var pos = camera.ViewportToWorldPoint(new Vector3(start_positionX + spacing, start_positionY, 0.0f));
            pos.z = 0.0f;
            obj.transform.position = pos;

            var width = obj.GetComponent<MeshRenderer>().bounds.size.x;
            Vector3 posX = camera.WorldToViewportPoint(new Vector3(pos.x, pos.y, 0f));
            Vector3 max_posX = camera.WorldToViewportPoint(new Vector3(pos.x + width, pos.y, 0f));

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
        string wordFile;
        if (round < 10)
            wordFile = files[0];
        else if (round < 20)
            wordFile = files[1];
        else
            wordFile = files[2];
        TextAsset txt = (TextAsset)Resources.Load(wordFile, typeof(TextAsset));
        string content = txt.text;
        return Load(content);
    }

    public string getCurrentWord()
    {
        return currentWord_string;
    }
}
