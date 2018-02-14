using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveX;
    public float jump;
    public float saut;
    public int speed;
    private float force;
    private float gravite;
    public float power;
    public int limite;
    public bool boole;
    public bool chute;
    public bool montee;
    private bool Bouge;
    
    private float posit;
    private Rigidbody2D rgbd2;
    private Animator amtr;

    // Use this for initialization
    private void Start()
    {
        rgbd2 = GetComponent<Rigidbody2D>();
        amtr = GetComponent<Animator>();
        force = 1f;
        gravite = -1f;
        boole = false;
        chute = false;
        montee = false;
        Bouge = false;

    }

    // Update is called once per frame
    private void Update()
    {
        deplacement();
       // sauter();
      
    }
    private void deplacement()
    {
        amtr.SetBool("isMoving", Bouge);
        moveX = Input.GetAxis("Horizontal");
        if (moveX*speed > 0.1)
        {
            Bouge = true;
        }
        else
        {
            Bouge = false;
        }
        
        rgbd2.velocity = new Vector2(moveX * speed, rgbd2.velocity.y);

    }

    private void sauter()
    {

        jump = Input.GetAxis("Jump");

        if (jump == 1)
        {
            boole = true;
        }
        if (boole == true)
        {
            if (force > 0.1f)
            {
                monter();
            }
            else
            {
                montee = false;
                chute = true;
                tomber();
            }      
        }
    }
    private void monter()
    {
        force *= 0.91f;
        power = force;
        montee = true;
        chute = false;
        saut += power;
        rgbd2.velocity = new Vector2(rgbd2.velocity.x, saut);
    }

    private void tomber()
    {
        if (chute == true)
        {
            gravite *= 1.00001f;
            power = gravite;
            chute = true;
            montee = false;
            saut += power;
            rgbd2.velocity = new Vector2(rgbd2.velocity.x, saut);
            print("TU TOMBES");
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            print("TU ES DESSUS");
                /*
                boole = false;
                chute = false;
                gravite = -1f;
                force = 1f;
                power = 0;
                saut = 0;
                rgbd2.velocity = new Vector2(rgbd2.velocity.x, 0);
                transform.position = new Vector2(transform.position.x, collision.gameObject.transform.position.y + transform.localScale.y);
                */
            
            
        }
        else
        {
            tomber();
            
        }
    }
    


}

