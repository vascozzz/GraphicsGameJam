using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    //Params
    [SerializeField] private LayerMask jumpRayMask;
    [SerializeField] private float moveSpeed = 10000f;
    [SerializeField] private float maxVelocity = 10f;
    [SerializeField] private float jumpForce = 1000f;

    [SerializeField] private Animator spriteAnimator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    //Utility
    private float moveAxis;
    private float jumpAxis;
    private Vector3 moveVec;
    private Vector2 moveDir;
    public int playerNum;
    public bool isDead;
    private bool grounded = false;

    //Components
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;

    //Sound
    [SerializeField] private AudioSource floorSound;
    [SerializeField] private AudioSource jumpSound;

	void Start () 
	{
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        isDead = false;
	}
	
	void Update () 
	{
        UpdateAnimation();
	}

    void FixedUpdate()
    {
        moveAxis = Input.GetAxisRaw("Horizontal" + playerNum);
        jumpAxis = Input.GetAxisRaw("Vertical" + playerNum);

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
            jumpSound.Play();
            
            rb.AddForce(transform.up * jumpForce);
        }
    }

    private bool isGrounded()
    {
        return Physics2D.Raycast(transform.position, -transform.up, circleCollider.radius + 0.7f, jumpRayMask);
    }

    private void UpdateAnimation()
    {
        bool right = moveAxis > 0;
        bool left = moveAxis < 0;
        bool onGround = isGrounded();

        if (!grounded && onGround)
        {
            floorSound.Play();
        }

        grounded = onGround;

        spriteAnimator.SetBool("Right", right);
        spriteAnimator.SetBool("Left", left);
        spriteAnimator.SetBool("Jump", !onGround);

        if (right)
        {
            spriteRenderer.flipX = false;
        }
            
        if (left)
        {
            spriteRenderer.flipX = true;
        } 
    }

    public void SetPlayer(int playerNum)
    {
        this.playerNum = playerNum;
    }

    public int GetPlayer()
    {
        return playerNum;
    }

    public Vector2 GetShotDirection()
    {
        // if not moving, get perpendicular 
        if (moveAxis == 0 && jumpAxis == 0)
        {
            if (!spriteRenderer.flipX)
            {
                Vector3 dir = transform.right;
                dir.z = 0f;
                return (Vector2)dir;
            }
            else
            {
                Vector3 dir = -transform.right;
                dir.z = 0f;
                return (Vector2)dir;
            }
        }
        else 
        {
            return transform.right * moveAxis + transform.up * jumpAxis;
        }
    }

    public void KillPlayer()
    {
        isDead = true;
        gameObject.SetActive(false);

        GameManager.instance.OnPlayerDeath();
    }
}
