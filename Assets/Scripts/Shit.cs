using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shit : MonoBehaviour {
   

	// Use this for initialization
	void Start () {
		
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Called upon collision
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Player") //Check if it is with a bird
        {
            if (coll.gameObject.GetComponent<BirdController>().hasMail()) //Does it have the mail?
            {
               coll.gameObject.GetComponent<BirdController>().dropMail();
            }
            coll.gameObject.GetComponent<BirdController>().applyStun();
			coll.gameObject.GetComponent<BirdController>().PoopedSprite();
			Destroy(gameObject);         
        }
        else
        {
            Destroy(gameObject);
        }

    }

    
}
