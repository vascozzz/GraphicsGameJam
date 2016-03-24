using UnityEngine;
using System.Collections;

public class PortalController : MonoBehaviour 
{
    public PortalPower portalPower;

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.isTrigger) return;

        GetComponent<AudioSource>().Play();

        portalPower.OnPortalEnter(this.gameObject, hit.gameObject);
    }
}
