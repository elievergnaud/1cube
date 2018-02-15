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

        if (Input.GetButtonDown("Dash_"+ this.gameObject.name) && dash == false)
        {
            dash = true;
            targetVelocity = move * maxSpeed * 20;
        }
        if (Input.GetButtonUp("Dash_" + this.gameObject.name))
            dash = false;
    }


    protected override void ComputeVelocity()
    {
        move.x = Input.GetAxis("Horizontal_" + this.gameObject.name);

        if (Input.GetButtonDown("Jump_" + this.gameObject.name) && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump_" + this.gameObject.name))
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


    public float getVelocityX()
    {
        return velocity.x;
    }
    public float getMove()
    {
        return move.x;
    }

    protected override void ParticleModificator()
    {

        SpriteRenderer m_SpriteRenderer = GetComponent<SpriteRenderer>();
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var vel = ps.velocityOverLifetime;
        vel.enabled = true;
        vel.space = ParticleSystemSimulationSpace.Local;


        if (m_SpriteRenderer.flipX == false)
        {
            AnimationCurve curve = new AnimationCurve();
            curve.AddKey(0.0f, 0.5f);
            curve.AddKey(0.5f, 0.0f);
            ParticleSystem.MinMaxCurve minMaxCurve = new ParticleSystem.MinMaxCurve(5.0f, curve);
            vel.x = minMaxCurve;
        }
        else
        {
            AnimationCurve curve = new AnimationCurve();
            curve.AddKey(0.0f, -0.5f);
            curve.AddKey(-0.5f, 0.0f);
            ParticleSystem.MinMaxCurve minMaxCurve = new ParticleSystem.MinMaxCurve(-5.0f, curve);
            vel.x = minMaxCurve;

        }
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