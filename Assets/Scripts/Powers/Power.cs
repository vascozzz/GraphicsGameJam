using UnityEngine;
using System.Collections;

public abstract class Power : MonoBehaviour 
{
    public abstract void Step();

    protected Vector3 GetMouseAsWorldCoords()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;

        return mousePos;
    }
}
