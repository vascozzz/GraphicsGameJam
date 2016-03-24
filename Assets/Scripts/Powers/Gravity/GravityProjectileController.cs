using UnityEngine;
using System.Collections;

public class GravityProjectileController : MonoBehaviour {

    [SerializeField]
    private float gravityZoneForce = 10f;
    [SerializeField]
    private GameObject gravityZone;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        this.rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        Explode();
    }


    public void Explode()
    {
        //Get gravity vector
        Vector2 gravityVector = rb.velocity.normalized * gravityZoneForce;


        GameObject gravityZoneObj = GameObject.Instantiate(gravityZone, transform.position, Quaternion.identity) as GameObject;
        Vector3 eulerAngles = gravityZoneObj.transform.eulerAngles;

        float angleValue = Vector3.Angle(gravityVector, Vector3.right);
        Vector3 crossValue = Vector3.Cross(gravityVector, Vector3.right);
        if (crossValue.z > 0) angleValue = -angleValue;

        eulerAngles.z = angleValue;
        gravityZoneObj.transform.eulerAngles = eulerAngles;

        gravityZoneObj.GetComponent<GravityZoneController>().gravityVector = gravityVector;

        Destroy(this.gameObject);
    }
}
