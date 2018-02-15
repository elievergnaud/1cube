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
    public float hSliderValueX = 1.0f;
    public float hSliderValueY = 1.0f;
    public float hSliderValueZ = 1.0f;

    public ParticleSystem ps;
    public bool jumping = false;

    protected bool dash = false;
    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    
        
    IEnumerator WaitAndPrint(float waitTime)
    {
        if (Input.GetButtonDown("Dash_" + this.gameObject.name) && dash == false)
        {
            dash = true;
            targetVelocity = move * maxSpeed * 20;
            dash = false;
        }
        if (Input.GetButtonUp("Dash_" + this.gameObject.name))
            dash = false;

        yield return new WaitForSeconds(waitTime);
        print("WaitAndPrint " + Time.time);
    }

    protected override IEnumerator Dash()
    {

        if (Input.GetButtonDown("Dash_"+ this.gameObject.name) && dash == false)
        {
            yield return StartCoroutine(WaitAndPrint(2.0F));
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
            jumping = true;

        }
        else if (Input.GetButtonUp("Jump_" + this.gameObject.name))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
                jumping = false;
            }
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0f) : (move.x < 0f));
        
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

        var velocityOverLifetime = ps.velocityOverLifetime;
        velocityOverLifetime.enabled = true;
        velocityOverLifetime.space = ParticleSystemSimulationSpace.World;


        if (m_SpriteRenderer.flipX == false)
        {
            AnimationCurve curve = new AnimationCurve();
            curve.AddKey(0.0f, 0.0f);
            curve.AddKey(1.0f, 1.0f);

            ParticleSystem.MinMaxCurve minMaxCurve = new ParticleSystem.MinMaxCurve(-5.0f, curve);

            velocityOverLifetime.x = minMaxCurve;
        }
        else
        {
            AnimationCurve curve = new AnimationCurve();
            curve.AddKey(0.0f, 1.0f);
            curve.AddKey(1.0f, 0.0f);

            ParticleSystem.MinMaxCurve minMaxCurve = new ParticleSystem.MinMaxCurve(5.0f, curve);

            velocityOverLifetime.x = minMaxCurve;

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