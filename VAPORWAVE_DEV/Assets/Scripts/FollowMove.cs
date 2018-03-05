using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMove : MonoBehaviour {

    public GameObject player;

	
	void Update () {
        print("this x" +this.transform.position.x);
        print("this y "+this.transform.position.y);
        print("player x "+player.transform.position.x);
        print("player y "+player.transform.position.y);
        this.transform.position = player.transform.position;
    }
}
