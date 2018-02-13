using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    Vector2 move = Vector2.zero;

    protected bool dash = false;
    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    protected override void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dash == false)
        {
            dash = true;
            targetVelocity = move * maxSpeed * 20;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
            dash = false;
    }
    protected override void ComputeVelocity()
    {

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }
        
        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        
        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }
    /*
    void OnCollisionEnter2D(Collider2D collider)
    {
        if (collider.tag == "wall")
        {
            if (Input.GetButtonDown("Jump") && !grounded)
            {

            }
        }
    }*/
}