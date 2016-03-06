using UnityEngine;
using System.Collections;

public class GunPointerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mouseCoords = GetMouseAsWorldCoords();

        Vector2 dir = mouseCoords - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

    protected Vector3 GetMouseAsWorldCoords()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;

        return mousePos;
    }
}
