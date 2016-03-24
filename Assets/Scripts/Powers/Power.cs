using UnityEngine;
using System.Collections;

public abstract class Power : MonoBehaviour 
{
    [SerializeField, Range(0f, 100f)] public float juiceCost;

    protected PowerController powerController;
    protected PlayerController playerController;

    public abstract void Step();

    protected Vector3 GetMouseAsWorldCoords()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;

        return mousePos;
    }

    protected virtual void Start() 
    {
        powerController = GetComponent<PowerController>();
        playerController = GetComponent<PlayerController>();
    }
}
