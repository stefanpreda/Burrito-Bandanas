using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FocusSetter : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InputField input = gameObject.GetComponent<InputField>();
        input.ActivateInputField();
    }

}
