using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerNetworkSetup : NetworkBehaviour 
{
	void Start()
    {
	    if (isLocalPlayer)
        {
            //colliders
            foreach (CircleCollider2D coll in GetComponents<CircleCollider2D>())
            {
                coll.enabled = true;
            }

            GetComponent<Rigidbody2D>().gravityScale = 1;
            GetComponent<PlayerController>().enabled = true;
            GetComponent<GravityController>().enabled = true;
            GetComponent<PowerController>().enabled = true;
            GetComponent<GravityPower>().enabled = true;
            GetComponent<PortalPower>().enabled = true;
            GetComponent<RewindPower>().enabled = true;
            GetComponent<ShockwavePower>().enabled = true;
            GetComponentInChildren<GunPointerController>().enabled = true;
        }
	}
}
