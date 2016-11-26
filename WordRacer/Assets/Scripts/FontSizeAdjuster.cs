using UnityEngine;
using System.Collections;

public class FontSizeAdjuster : MonoBehaviour {

    public int text_size_divisor = 25;

    void Start () {
        gameObject.GetComponent<GUIText>().fontSize = Screen.width / text_size_divisor;
    }
}
