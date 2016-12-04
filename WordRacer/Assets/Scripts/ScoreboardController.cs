using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.IO;

public class ScoreboardController : MonoBehaviour {

    string scores = "1. Empty\n2. Empty\n3. Empty\n4. Empty\n5. Empty \n" +
                    "6. Empty\n7. Empty\n8. Empty\n9. Empty\n10. Empty\n";

    //public int text_size_divisor = 35;

	void Start () {
        gameObject.GetComponent<Text>().text = getMaxScores();
        //gameObject.GetComponent<Text>().fontSize = Screen.width / text_size_divisor;
    }

    public string getMaxScores()
    {
        string filename = PlayerNameAndScoreFile.GetScoreFileName();
        List<string> lines = new List<string>();
        string line;
        print(filename);
        if (!File.Exists(filename))
            return scores;
        StreamReader file = new StreamReader(filename);
        while ((line = file.ReadLine()) != null)
            lines.Add(line);

        lines.Sort(delegate (string s1, string s2)
        {
            char[] delimiterChars = { ' ' };
            string[] words1 = s1.Split(delimiterChars);
            string[] words2 = s2.Split(delimiterChars);

            if (words1[1] == null || words2[1] == null)
                return -1;
            int score1 = int.Parse(words1[1]);
            int score2 = int.Parse(words2[1]);
            return score2 - score1;
        });

        string res = "";
        if (lines.Count >= 10)
            for (int i = 0; i < 10; i++)
                res = res + (i + 1) + ". " + lines[i] + "\n";
        else
        {
            for (int i = 0; i < lines.Count; i++)
                res = res + (i + 1) + ". " + lines[i] + "\n";

            for (int i = lines.Count; i < 10; i++)
                res = res + (i + 1) + ". Empty\n";
        }

        file.Close();

        return res;
    }
}
