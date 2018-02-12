using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feubleu : MonoBehaviour {
    public float moveX;
    public bool isMoving;
    public float moveA;
    private Animator anmtr;
    

	// Use this for initialization
	void Start () {
        anmtr = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        moveX = Input.GetAxis("Horizontal");
        anmtr.SetFloat("moveA", moveX);

	}
}
