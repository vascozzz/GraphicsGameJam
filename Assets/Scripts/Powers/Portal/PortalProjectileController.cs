using UnityEngine;
using System.Collections;

public class PortalProjectileController : MonoBehaviour 
{
    public PortalPower portalPower;
    public GameObject portal;

    void OnCollisionEnter2D(Collision2D coll)
    {
        portalPower.OnProjectileCollision(portal, coll.contacts[0].point);
        Destroy(this.gameObject);
    }
}
