using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBox : MonoBehaviour {

    public GameObject controller;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Mail")
        {
            Debug.Log("Player " + other.gameObject.GetComponent<Letter>().Owner.GetComponent<BirdController>().playerNumber + " has scored!");
            controller.GetComponent<ScoreController>().AddScore(other.gameObject.GetComponent<Letter>().Owner.GetComponent<BirdController>().playerNumber);

            //Respawn letter
            other.gameObject.GetComponent<LetterController>().LetterRespawn();
        }
    }
}
