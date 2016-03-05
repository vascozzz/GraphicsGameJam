using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityController : MonoBehaviour 
{
    [SerializeField] private float gravity = -70f;
    [SerializeField] private Transform defaultAttractor;

    //Utility
    private Vector3 gravityUp;
    private Vector3 bodyUp;

    //Gravity Zones
    private List<GravityZoneController> gravityZones;

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

        gravityZones = new List<GravityZoneController>();
    }

    // Update is called once per frame
    void Update ()
    {
        //Gravity is artificial
        if (gravityZones.Count > 0)
        {
            Vector2 finalGravityUp = new Vector2();

            foreach (GravityZoneController gravityZone in gravityZones)
            {
                finalGravityUp += gravityZone.gravityVector;
                rb.AddForce(gravityZone.gravityVector);
            }

            gravityUp = -finalGravityUp;
            bodyUp = transform.up;
        }
        else //Gravity is default
        {
            //Update vector values
            gravityUp = (transform.position - defaultAttractor.position).normalized;
            bodyUp = transform.up;

            //Apply gravity
            rb.AddForce(gravityUp * gravity);
        }

        //Rotate correctly
        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
    }

    public void AddGravityZone(GravityZoneController gravityZone)
    {
        gravityZones.Add(gravityZone);
    }

    public void RemoveGravityZone(GravityZoneController gravityZone)
    {
        gravityZones.Remove(gravityZone);
    }
}