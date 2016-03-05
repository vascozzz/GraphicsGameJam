using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RewindPower : Power
{
    [SerializeField] private int positionsToRecord = 10;
    [SerializeField] private float recordInterval = 0.1f;
    [SerializeField] private float rewindSpeed = 25f;

    private PlayerController playerController;
    private GravityController gravityController;
    private Rigidbody2D rigidBody;
    private CircleCollider2D circleCollider;

    private Queue previousPos;
    private Queue previousRot;

    private float nextRecordTime;
    private bool rewinding;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        gravityController = GetComponent<GravityController>();
        rigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();

        previousPos = new Queue();
        previousRot = new Queue();
        nextRecordTime = 0f;
        rewinding = false;
    }

    public override void Step()
    {
        if (!rewinding && Input.GetMouseButton(0))
        {
            Record();
        }

        if (Input.GetMouseButtonUp(0))
        {
            rewinding = true;
            StartCoroutine(Rewind());
        }
    }

    void Record()
    {
        if (!rewinding)
        {
            if (Time.time > nextRecordTime)
            {
                if (previousPos.Count >= positionsToRecord)
                {
                    previousPos.Dequeue();
                    previousRot.Dequeue();
                }

                previousPos.Enqueue(transform.position);
                previousRot.Enqueue(transform.rotation);

                nextRecordTime = Time.time + recordInterval;
            }
        }
    }

    IEnumerator Rewind()
    {
        List<object> positions = previousPos.ToArray().ToList();
        List<object> rotations = previousRot.ToArray().ToList();
        Vector3 nextPos = Vector3.zero;
        Quaternion nextRot = Quaternion.identity;
        float originalGravityScale = rigidBody.gravityScale;

        // TEMP
        Color uh = this.GetComponent<SpriteRenderer>().color;
        this.GetComponent<SpriteRenderer>().color = Color.green;

        // reverse queues
        positions.Reverse();
        rotations.Reverse();

        // temporarily disable controls and physics
        playerController.enabled = false;
        gravityController.enabled = false;
        circleCollider.enabled = false;
        rigidBody.velocity = Vector2.zero;
        rigidBody.gravityScale = 0f;

        // iterate over each previous position
        for (int i = 0; i < positions.Count; i++)
        {
            nextPos = (Vector3)positions[i];
            nextRot = (Quaternion)rotations[i];

            while (Vector3.Distance(transform.position, nextPos) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPos, Time.deltaTime * rewindSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, nextRot, Time.deltaTime * rewindSpeed);

                yield return null;
            }
        }

        // snap to last position
        transform.position = nextPos;
        transform.rotation = nextRot;

        // re-enable everything
        playerController.enabled = true;
        gravityController.enabled = true;
        circleCollider.enabled = true;
        rigidBody.gravityScale = originalGravityScale;
        previousPos.Clear();

        rewinding = false;

        // TEMP
        this.GetComponent<SpriteRenderer>().color = uh;
    }
}
