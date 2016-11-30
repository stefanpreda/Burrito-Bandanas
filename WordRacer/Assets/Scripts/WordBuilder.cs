using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class TranslateObject
{
    private GameObject obj;
    private Vector3 starting_position;
    private Vector3 ending_position;
    private float speed = 1f;
    private float startTime;
    private int direction = 0;

    public TranslateObject(GameObject obj, Vector3 starting_position, Vector3 ending_position)
    {
        this.obj = obj;
        this.starting_position = starting_position;
        this.ending_position = ending_position;
    }

    public void setStartingPosition(Vector3 starting_position)
    {
        this.starting_position = starting_position;
    }

    public void setEndingPosition(Vector3 ending_position)
    {
        this.ending_position = ending_position;
    }

    public void setObject(GameObject obj)
    {
        this.obj = obj;
    }

    public void setStartTime(float time)
    {
        startTime = time;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
    
    public void setDirection(int direction)
    {
        this.direction = direction;
    }

    public Vector3 getStartingPosition()
    {
        return starting_position;
    }

    public Vector3 getEndingPosition()
    {
        return ending_position;
    }
    
    public GameObject getObject()
    {
        return obj;
    }

    public float getStartTime()
    {
        return startTime;
    }

    public float getSpeed()
    {
        return speed;
    }

    public int getDirection()
    {
        return direction;
    }
}
public class WordBuilder : MonoBehaviour {

    List<GameObject> currentWord;
    string currentWord_string;

    public float start_positionX = 0.5f;
    public float start_positionY = 0.4f;
    public float word_spacing = 0.005f;
    public int text_size_divisor = 25;

    public string sortingLayerName = "Default";
    public int sortingOrder = 0;

    private bool translate_animations_activated = false;
    private List<TranslateObject> translate_objects = null;

    private string[] files = { "easy words", "hard words", "master words" };
    public Camera camera = null;

    void Start () {
        currentWord = new List<GameObject>();
        ChangeWord();
    }

    void Update()
    {
        if (translate_animations_activated)
        {
            if (translate_objects == null)
                return;
            foreach (TranslateObject obj in translate_objects)
            {
                float distCovered = (Time.time - obj.getStartTime()) * obj.getSpeed();
                float fracJourney = distCovered / Vector3.Distance(obj.getStartingPosition(), obj.getEndingPosition());

                if (obj.getDirection() == 0)
                {
                    obj.getObject().transform.position = Vector3.Lerp(obj.getStartingPosition(), obj.getEndingPosition(), fracJourney);
                    if (obj.getObject().transform.position.y == obj.getEndingPosition().y)
                    {
                        obj.setDirection(1);
                        obj.setStartTime(Time.time);
                    }
                }

                else
                {
                    obj.getObject().transform.position = Vector3.Lerp(obj.getEndingPosition(), obj.getStartingPosition(), fracJourney);
                    if (obj.getObject().transform.position.y == obj.getStartingPosition().y)
                    {
                        obj.setDirection(0);
                        obj.setStartTime(Time.time);
                    }

                }
            }
        }
    }

    private string Load(string fileContent) {
        string result = "";
        string[] words = fileContent.Split('\n');
        int number;
        for (int i = 0; i < 2; i++) {

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

        if (round > 3)
        {
            translate_objects = new List<TranslateObject>();
            translate_animations_activated = true;
        }

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
            //TODO: This works but the text is very hard to read
            /*if (round == 0)
            {
                if (selected_indices.Contains(index))
                {
                    obj.transform.localScale = new Vector3((-1) * obj.transform.localScale.x, obj.transform.localScale.y, obj.transform.localScale.z);
                }
            }*/

            //Apply rotations with fixed angle
            //TODO Not sure if this is correct, looks horrible
            /*if (round == 0)
            {
                if (selected_indices.Contains(index))
                {
                    int rotation_angle_index = Random.Range(0, 8);
                    int angle = 0;
                    if (rotation_angle_index < 5)
                        angle = rotation_angle_index * 90;
                    else
                        angle = (rotation_angle_index - 4) * (-90);
                    Quaternion rotation = Quaternion.identity;
                    rotation.eulerAngles = new Vector3(0, 0, angle);
                    //obj.transform.Rotate(Vector3.forward * angle);
                    //obj.transform.rotation = rotation;
                    obj.transform.RotateAround(obj.transform.position, new Vector3(0, 0, 1), angle);
                }

            }*/

            Vector3 pos = camera.ViewportToWorldPoint(new Vector3(start_positionX + spacing, start_positionY, 0.0f));
            pos.z = 0.0f;
            obj.transform.position = pos;

            //Apply tranlations on Y axis
            if (round == 2)
            {
                if (selected_indices.Contains(index))
                {
                    float deltaY = Random.Range(-0.05f, 0.05f);
                    var position = camera.ViewportToWorldPoint(new Vector3(start_positionX + spacing, start_positionY + deltaY, 0.0f));
                    position.z = 0.0f;
                    obj.transform.position = position;
                }
            }

            //Apply translation animations
            if (translate_animations_activated)
            {
                //Get the distance of the animation
                float DeltaDistance_viewport = Random.Range(-0.05f, 0.05f);
                float DeltaDistance_world = camera.ViewportToWorldPoint(new Vector3(0, DeltaDistance_viewport, 0)).y -
                    camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

                //Compute starting and ending positions
                Vector3 starting_position = obj.transform.position;
                starting_position.y -= DeltaDistance_world;
                starting_position.z = 0;
                Vector3 ending_position = obj.transform.position;
                ending_position.y += DeltaDistance_world;
                ending_position.z = 0;

                //Create a new translate object
                TranslateObject to = new TranslateObject(obj, starting_position, ending_position);
                to.setStartTime(Time.time);
                to.setDirection(Random.Range(0, 2));

                translate_objects.Add(to);
            }

            var width = obj.GetComponent<MeshRenderer>().bounds.size.x;
            Vector3 posX = camera.WorldToViewportPoint(new Vector3(pos.x, pos.y, 0f));
            Vector3 max_posX = camera.WorldToViewportPoint(new Vector3(pos.x + width, pos.y, 0f));

            //Adding character lengths
            spacing += max_posX.x - posX.x;

            //Adding extra space between characters
            spacing += word_spacing;

            if (c == ' ')
                spacing += 0.05f;
            currentWord.Add(obj);

            index++;
        }
    }

    public void ClearWord()
    {
        foreach (GameObject o in currentWord)
            Destroy(o);
        currentWord.Clear();

        translate_animations_activated = false;
        if (translate_objects != null)
            translate_objects.Clear();
    }
	
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
