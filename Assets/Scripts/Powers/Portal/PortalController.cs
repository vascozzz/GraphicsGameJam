using UnityEngine;
using System.Collections;

public class PortalController : MonoBehaviour 
{
    public PortalPower portalPower;

    void OnTriggerEnter2D(Collider2D hit)
    {
        portalPower.OnPortalEnter(this.gameObject, hit.gameObject);
    }
}
