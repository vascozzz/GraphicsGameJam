using UnityEngine;
using System.Collections;

public class RotationController : MonoBehaviour {

    [SerializeField] private float rotation = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, rotation * Time.deltaTime);
	}
}
