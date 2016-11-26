using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

    public int starting_health = 5;
    int current_health;

	// Use this for initialization
	void Start () {
        current_health = starting_health;
	}

    public int getCurrentHealth()
    {
        return current_health;
    }

    public void loseHealth()
    {
        if (current_health == 0)
            return;
        int index = starting_health - current_health + 1;
        string tag = "Heart" + index;
        Destroy(GameObject.FindGameObjectWithTag(tag));
        current_health--;
    }
}
