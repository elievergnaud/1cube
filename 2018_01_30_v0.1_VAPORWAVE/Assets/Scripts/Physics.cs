using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour {
    protected bool grounded;
    protected Rigidbody2D rgbd2d;
    protected Vector2 velocity;
	
	void Start () {
        grounded = false;
        rgbd2d = GetComponent<Rigidbody2D>();

	}
    private void FixedUpdate()
    {
        gravite();
    }

    void gravite()
    {
        if (grounded == false)
        {
            velocity += Physics2D.gravity * Time.deltaTime;
            Vector2 deltaPosition = velocity * Time.deltaTime;

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

}
