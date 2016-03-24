using UnityEngine;
using System.Collections;

public class KillOnTouchController : MonoBehaviour 
{
    [SerializeField] private int playerLayer = 8;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == playerLayer)
        {
            coll.gameObject.GetComponent<PlayerController>().KillPlayer();
        }
    }
}
