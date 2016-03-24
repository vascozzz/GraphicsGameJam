using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShockwavePower : Power 
{
    [SerializeField] private LayerMask shockwaveMask;
    [SerializeField] private float shockForce;
    [SerializeField] private float cooldown;

    [SerializeField] private SpriteRenderer shockwaveSprite;
    [SerializeField] private float shockwaveSpriteTime = 0.3f;

    private float nextTime;
    private List<Rigidbody2D> contained;

    protected override void Start()
    {
        base.Start();

        contained = new List<Rigidbody2D>();
        nextTime = 0;
    }

    public override void Step()
    {
        if (Time.time > nextTime)
        {
            foreach (Rigidbody2D rb in contained)
            {
                rb.AddForce((rb.gameObject.transform.position - transform.position).normalized * shockForce);
            }

            StartCoroutine(DisplayShockWave());
            nextTime = Time.time + cooldown;
        }
    }

    void OnTriggerStay2D(Collider2D hit)
    {
        if (shockwaveMask == (shockwaveMask | 1 << hit.gameObject.layer))
        {
            Rigidbody2D rb = hit.gameObject.GetComponent<Rigidbody2D>();

            if (!contained.Contains(rb))
            {
                contained.Add(rb);
            }
        }
    }

    void OnTriggerExit2D(Collider2D hit)
    {
        if (shockwaveMask == (shockwaveMask | 1 << hit.gameObject.layer))
        {
            Rigidbody2D rb = hit.gameObject.GetComponent<Rigidbody2D>();

            if (contained.Contains(rb))
            {
                contained.Remove(rb);
            }
        }
    }

    IEnumerator DisplayShockWave()
    {
        shockwaveSprite.enabled = true;
        yield return new WaitForSeconds(shockwaveSpriteTime);
        shockwaveSprite.enabled = false;
    }
}