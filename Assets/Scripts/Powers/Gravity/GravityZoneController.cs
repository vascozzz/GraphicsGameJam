using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityZoneController : MonoBehaviour {

    public Vector2 gravityVector;
    private List<GravityController> contained;

	// Use this for initialization
	void Start () {
        contained = new List<GravityController>();
        StartCoroutine(WaitAndDestroy(5));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.isTrigger) return;

        GravityController gravityCtrl = coll.gameObject.GetComponent<GravityController>();

        if (!contained.Contains(gravityCtrl))
        {
            gravityCtrl.AddGravityZone(this);
            contained.Add(gravityCtrl);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.isTrigger) return;

        GravityController gravityCtrl = coll.gameObject.GetComponent<GravityController>();

        if (contained.Contains(gravityCtrl))
        {
            gravityCtrl.RemoveGravityZone(this);
            contained.Remove(gravityCtrl);
        }
    }

    IEnumerator WaitAndDestroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        foreach (GravityController gravityCtrl in contained)
        {
            gravityCtrl.RemoveGravityZone(this);
        }

        Destroy(this.gameObject);
    }
}
