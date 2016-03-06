﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    //Params
    [SerializeField] private LayerMask jumpRayMask;
    [SerializeField] private float moveSpeed = 10000f;
    [SerializeField] private float maxVelocity = 10f;
    [SerializeField] private float jumpForce = 1000f;

    [SerializeField] public Animator spriteAnimator;
    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] public SpriteRenderer armRenderer;

    //Utility
    private float moveAxis;
    private float jumpAxis;
    private Vector3 moveVec;
    private Vector2 moveDir;

    //Components
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private PlayerNetworkController networkController;

	void Start () 
	{
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        networkController = GetComponent<PlayerNetworkController>();
        spriteAnimator = GetComponentInChildren<Animator>();

        foreach (SpriteRenderer rend in GetComponentsInChildren<SpriteRenderer>())
        {
            if (rend.gameObject.name == "GunSprite")
            {
                armRenderer = rend;
            }
            else if (rend.gameObject.name == "Sprite")
            {
                spriteRenderer = rend;
            }
        }
	}
	
	void Update () 
	{
        UpdateAnimation();
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
        return Physics2D.Raycast(transform.position, -transform.up, circleCollider.radius + 0.7f, jumpRayMask);
    }

    private void UpdateAnimation()
    {
        bool right = moveAxis > 0;
        bool left = moveAxis < 0;
        bool grounded = isGrounded();

        spriteAnimator.SetBool("Right", right);
        spriteAnimator.SetBool("Left", left);
        spriteAnimator.SetBool("Jump", !grounded);

        if (right)
        {
            spriteRenderer.flipX = false;
            armRenderer.flipY = false;
        }
            
        if (left)
        {
            spriteRenderer.flipX = true;
            armRenderer.flipY = true;
        }

        networkController.NetworkAnimate(right, left, grounded);
    }

    public void NetworkAnimate(bool right, bool left, bool grounded)
    {
        // first time we receive an animation request, grab references to renderers
        if (spriteAnimator == null)
        {
            spriteAnimator = GetComponentInChildren<Animator>();

            foreach (SpriteRenderer rend in GetComponentsInChildren<SpriteRenderer>())
            {
                if (rend.gameObject.name == "GunSprite")
                {
                    armRenderer = rend;
                }
                else if (rend.gameObject.name == "Sprite")
                {
                    spriteRenderer = rend;
                }
            }
        }

        spriteAnimator.SetBool("Right", right);
        spriteAnimator.SetBool("Left", left);
        spriteAnimator.SetBool("Jump", !grounded);

        if (right)
        {
            spriteRenderer.flipX = false;
            armRenderer.flipY = false;
        }

        if (left)
        {
            spriteRenderer.flipX = true;
            armRenderer.flipY = true;
        }
    }
}
