using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    public float acceleration;
    public int sens=1;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    Vector2 move = Vector2.zero;
    public float hSliderValueX = 1.0f;
    public float hSliderValueY = 1.0f;
    public float hSliderValueZ = 1.0f;
    float charJumpPos;
    public float wJumpL;
    public int sensDep = 1;

    public Collider2D grab;
    public Collider2D grab_left;
    public Collider2D grab_right;
    public ParticleSystem ps;
    public bool jumping = false;

    public float dash_time = 2f;
    protected bool dash = false;
    //public Collider2D groundVerif;
    public Collider2D grabZones;

    float time = 0;
    // Use this for initialization
    void Awake()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    /*
IEnumerator WaitAndPrint(float waitTime)
{
        dash = true;
        targetVelocity = move * maxSpeed * 20;

    yield return new WaitForSeconds(waitTime);
    print("WaitAndPrint " + Time.time);
}

protected override IEnumerator Dash()
{

    if (Input.GetButtonDown("Dash_"+ this.gameObject.name) && dash == false)
    {
        yield return StartCoroutine(WaitAndPrint(2.0F));
        print("Dash ?");
    }
    if (Input.GetButtonUp("Dash_" + this.gameObject.name))
        dash = false;

    print("Dash ?");
}
*/
    protected override void Dash()
    {
        //print("Dash ?");
        if (Input.GetButtonDown("Dash_" + this.gameObject.name) && dash == false)
        {
                dash = true;



                targetVelocity = move * maxSpeed * 20;
                print("Dash");
        }
        if (Input.GetButtonUp("Dash_" + this.gameObject.name))
            dash = false;


    }
    
    /*protected override void WallGrab()
    {

        if (jumping == true && move.x != 0)
        {
            grab_col.gameObject.SetActive(true);
            print("collider actif");
            ComputeVelocity();
        }
        else
        {
            grab_col.gameObject.SetActive(false);
        }

    }*/

    protected override void ComputeVelocity()
    {
        // print(jumping);
        move.x = Input.GetAxis("Horizontal_" + this.gameObject.name) * sens ;

        if (Input.GetButtonDown("Jump_" + this.gameObject.name)&&grounded)
        {
            velocity.y = jumpTakeOffSpeed;
            //print("LA VELOCITE DU SAUT " + velocity.x);
            jumping = true;

        }
        else if (Input.GetButtonUp("Jump_" + this.gameObject.name)&&grounded)
        {
         
            if (velocity.y > 0)
            {
                //print("QUOI");
                velocity.y = velocity.y * 0.5f;
                jumping = false;
            }
        }

       
        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0f) : (move.x < 0f));
        
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            sensDep *= -1;
        }
        
        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed * acceleration;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Mur")
        {
            print("enter");
            frottement = 0.1f;
            mur = true;
            acceleration = 1;
            
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Mur")
        {
            print("exit");
            frottement = 1f;
            maxSpeed = 22;
            mur = false;
            acceleration = 1;

            if(sensDep > 0)
            {
                if (this.gameObject.transform.position.x > charJumpPos + (wJumpL))
                {
                    print(this.gameObject.transform.position.x);
                    print(charJumpPos);
                    print("ça passe");
                    sens = 1;
                }
            }
            else
            {
                if (this.gameObject.transform.position.x < charJumpPos + (wJumpL * -1))
                {
                    print(this.gameObject.transform.position.x);
                    print(charJumpPos);
                    print("ça passe");
                    sens = 1;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mur")
        {
            //print(velocity.y);

            if (Input.GetButtonDown("Jump_" + this.gameObject.name))            
            {
                charJumpPos = this.gameObject.transform.position.x;
                //print (charJumpPos);
                maxSpeed = 44;
                sens  *= - 1;    
            }

        }

        if (collision.gameObject.tag == "Ground")
        {
            if(jumping == true && move.x < 0)
            {
                grab_left.isTrigger = false;
            }
            else if(jumping == true && move.x > 0)
            {
                grab_right.isTrigger  = false;
            }

            if (grab_left.isTrigger == false && move.x > 0)
            {
                grab_left.isTrigger = true;
            }
            else if (grab_right.isTrigger == false && move.x < 0)
            {
                grab_right.isTrigger = true;
            }
        }
    }
}