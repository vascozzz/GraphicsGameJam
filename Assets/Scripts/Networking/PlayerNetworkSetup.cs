using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerNetworkSetup : NetworkBehaviour 
{
    [SerializeField] private GameObject[] skins;

	void Start()
    {
        GameObject skin = Instantiate(skins[this.netId.Value - 1], Vector3.zero, Quaternion.identity) as GameObject;
        skin.transform.parent = transform;
        skin.transform.localPosition = Vector3.zero;
        transform.position = new Vector3(0, 18, 0);

	    if (isLocalPlayer)
        {
            //colliders
            foreach (CircleCollider2D coll in GetComponentsInChildren<CircleCollider2D>())
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

        GetComponent<PlayerNetworkController>().enabled = true;
        GetComponent<PlayerSyncPosition>().enabled = true;
        GetComponent<PlayerSyncRotation>().enabled = true;
	}
}
