using UnityEngine;
using System.Collections;

public class RotationController : MonoBehaviour
{
    [SerializeField, Range(0f, 120f)] private float speed = 60f;

    void Update()
    {
        Vector3 rotation = transform.localEulerAngles;
        rotation.z += speed * Time.deltaTime;

        transform.localEulerAngles = rotation;
    }
}
