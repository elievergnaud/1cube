using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bougerbidon : MonoBehaviour
{
    
    private SpriteRenderer spriteRenderer;
    public GameObject pl;
    private PlayerPlatformerController p;
    private Animator anmtr;
    private float vel;
    private float move;
    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anmtr = GetComponent<Animator>();
        p = pl.GetComponent<PlayerPlatformerController>();
        
 
    }

    
    // Update is called once per frame
    void Update()
    {
        vel = p.getVelocityX();
        move = p.getMove();
        bool flipSprite = (spriteRenderer.flipX ? (move > 0f) : (move < 0f));
        anmtr.SetFloat("velocityX", Mathf.Abs(vel) / p.maxSpeed);
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

    }


}
