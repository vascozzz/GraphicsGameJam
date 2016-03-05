using UnityEngine;
using System.Collections;

public class GravityController : MonoBehaviour {

    [SerializeField] private float gravity = -70f;
    [SerializeField] private Transform defaultAttractor;

    //Utility
    private Vector3 gravityUp;
    private Vector3 bodyUp;

    private bool inWarpZone = false;

    // Components
    private Rigidbody2D rb;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.gravityScale = 0;

        if (defaultAttractor == null)
        {
            defaultAttractor = GameObject.FindGameObjectWithTag("Planet").transform;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        //Update vector values
        gravityUp = (transform.position - defaultAttractor.position).normalized;
        bodyUp = transform.up;

        //Apply gravity
        rb.AddForce(getGravity());

        //Rotate correctly
        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50 * Time.deltaTime);
    }

    public Vector2 getGravity()
    {
        if (!inWarpZone) //default gravity
        {
            return gravityUp * gravity;
        }
        else
        {
            //TODO
            return gravityUp * gravity;
        }
    }
       
}
