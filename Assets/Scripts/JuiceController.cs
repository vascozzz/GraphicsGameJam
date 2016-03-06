using UnityEngine;
using System.Collections;

public class JuiceController : MonoBehaviour 
{
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.isTrigger) return;

        PowerController powerController = coll.gameObject.GetComponent<PowerController>();

        if (powerController != null)
        {
            powerController.RefillJuice(gameObject.name);
            Destroy(this.gameObject);
        }
    }
}
