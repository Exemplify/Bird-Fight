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
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player") //Check if it is with a bird
        {
            Debug.Log("Collision with player");
            Debug.Log("Someone got shat on!");
            if (coll.gameObject.GetComponent<BirdController>().hasMail()) //Does it have the mail?
            {
               coll.gameObject.GetComponent<BirdController>().dropMail();
            Debug.Log("Bitch had my mail!");
            }
            coll.gameObject.GetComponent<BirdController>().applyStun();            
        }    

    }

    //ADD IN DESPAWN CRITERIA
}
