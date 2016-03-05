using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //Params
    [SerializeField] private LayerMask jumpRayMask;
    [SerializeField] private float moveSpeed = 10000f;
    [SerializeField] private float maxVelocity = 10f;
    [SerializeField] private float jumpForce = 1000f;

    //Utility
    private float moveAxis;
    private float jumpAxis;
    private Vector3 moveVec;
    private Vector2 moveDir;

    //Components
    private Rigidbody2D rb;

	
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
	}

    void FixedUpdate()
    {
        moveAxis = Input.GetAxisRaw("Horizontal");
        jumpAxis = Input.GetAxisRaw("Vertical");

        //Handle side movement
        moveDir = transform.right * moveAxis * moveSpeed * Time.deltaTime;

        float moveSign = transform.InverseTransformDirection(moveDir).x;
        float velSign = transform.InverseTransformDirection(rb.velocity).x;
        bool moveDiff = Mathf.Sign(moveSign) != Mathf.Sign(velSign);

        if (rb.velocity.magnitude < maxVelocity || moveDiff)
        {
            rb.AddForce(moveDir);
        }

        //Handle jump
        if (jumpAxis > 0 && isGrounded())
        {
            rb.AddForce(transform.up * jumpForce);
        }
         
    }

    private bool isGrounded()
    {
        return Physics2D.Raycast(transform.position, -transform.up, 2.3f, jumpRayMask);
    }
}
