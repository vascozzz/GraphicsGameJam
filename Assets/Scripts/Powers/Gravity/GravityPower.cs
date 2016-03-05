using UnityEngine;
using System.Collections;

public class GravityPower : Power 
{
    [SerializeField] private GameObject gravityZone;
    [SerializeField] private float gravityZoneForce = 10f;
    private Vector2 clickDown;
    
    public override void Step() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickDown = GetMouseAsWorldCoords();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (((Vector2)GetMouseAsWorldCoords()).Equals(clickDown))
                return;

            //Get gravity vector
            Vector2 gravityVector = ((Vector2) GetMouseAsWorldCoords() - clickDown).normalized * gravityZoneForce;
            GameObject gravityZoneObj = GameObject.Instantiate(gravityZone, clickDown, Quaternion.identity) as GameObject;
            Vector3 eulerAngles = gravityZoneObj.transform.eulerAngles;

            float angleValue = Vector3.Angle(gravityVector, Vector3.right);
            Vector3 crossValue = Vector3.Cross(gravityVector, Vector3.right);
            if (crossValue.z > 0) angleValue = -angleValue;

            eulerAngles.z = angleValue;
            gravityZoneObj.transform.eulerAngles = eulerAngles;

            gravityZoneObj.GetComponent<GravityZoneController>().gravityVector = gravityVector;
        }
	}
}
