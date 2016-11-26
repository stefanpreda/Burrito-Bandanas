using UnityEngine;
using System.Collections;

public class ViewportAdjust : MonoBehaviour {

    public new Camera camera = null;
    public float viewport_positionX = 0.3f;
    public float viewport_positionY = 0.2f;

	// Use this for initialization
	void Start () {
        RectTransform targetTransform = gameObject.GetComponent<RectTransform>();
        if (targetTransform != null)
            targetTransform.position = camera.ViewportToScreenPoint(new Vector3(viewport_positionX, viewport_positionY, 0.0f));
        else
        {
            var pos = camera.ViewportToWorldPoint(new Vector3(viewport_positionX, viewport_positionY, 0.0f));
            transform.position = new Vector3(pos.x, pos.y, 0.0f);
        }
    }

}
