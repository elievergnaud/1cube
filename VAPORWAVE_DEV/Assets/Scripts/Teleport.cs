using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    public GameObject[] portail;
    public GameObject[] portails;
    // Use this for initialization
    void Start()
    {
        portail = GameObject.FindGameObjectsWithTag("Portals");
        portails = new GameObject[portail.Length];
        for(int i = 0; i < portail.Length; i++)
        {
            switch (portail[i].gameObject.name)
            {
                case "BGAUCHE" :
                    portails[0] = portail[i];
                    break;
                case "BDROITE":
                    portails[1] = portail[i];
                    break;
                case "HGAUCHE":
                    portails[2] = portail[i];
                    break;
                case "HDROITE":
                    portails[3] = portail[i];
                    break;
                case "BAS":
                    portails[4] = portail[i];
                    break;
                case "HAUT":
                    portails[5] = portail[i];
                    break;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {



        if (collision.gameObject.CompareTag("Portals"))
        {

            if (collision.gameObject.transform.position.x == portails[4].transform.position.x)
            {
           
                this.transform.position = new Vector2(this.transform.position.x, portails[5].transform.position.y - 5);

            }
            else if (collision.gameObject.transform.position.x == portails[2].transform.position.x)
            {
          
                this.transform.position = new Vector2(portails[3].transform.position.x - 5, this.transform.position.y);

            }
            else if (collision.gameObject.transform.position.x == portails[3].transform.position.x)
            {
            
                this.transform.position = new Vector2(portails[2].transform.position.x + 5, this.transform.position.y);

            }
            else if (collision.gameObject.transform.position.x == portails[5].transform.position.x)
            {
      
                this.transform.position = new Vector2(this.transform.position.x, portails[4].transform.position.y + 5);

            }
            else if (collision.gameObject.transform.position.x == portails[0].transform.position.x)
            {
      
                this.transform.position = new Vector2(portails[1].transform.position.x - 5, this.transform.position.y);

            }
            else if (collision.gameObject.transform.position.x == portails[1].transform.position.x)
            {
           
                this.transform.position = new Vector2(portails[0].transform.position.x + 5, this.transform.position.y);

            }


        }

    }


}