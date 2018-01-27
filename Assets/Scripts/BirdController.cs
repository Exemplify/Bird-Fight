using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Primary controller for the bird. Contains states for if it has the letter, etc.
public class BirdController : MonoBehaviour {

    public Collider thisCollider; //Reference to this bird's collider
    public float movementPowerX; //The force of horizontal movement applied by each wing
    public float movementPowerY;//The force of vertical movement applied by each wing
    public float transferMagnitude; //The power threshold for releasing the letter
    public float stunTime; //The amount of time bird is stunned for
    public float knockbackForce;
    public bool IsPlaying=false;

    private bool hasLetter; //If this bird has the letter
    private GameObject letter; //The letter game object
    private float timeLeft; //Stun time remaining
    private bool isStunned; //Status effect for being unable to move

    // Use this for initialization
    void Start () {
        thisCollider = GetComponent<Collider>();
        timeLeft = stunTime;
    }
	
	// Update is called once per frame
	void Update () {
        if (IsPlaying)
        {
            if (isStunned)
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)
                {
                    isStunned = false;
                }
            }
        }        
	}

    //Called upon collision
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Player") //Check if it is another bird
        {
            Debug.Log("Collision with player");
            if (coll.relativeVelocity.magnitude > transferMagnitude) //If so, did you hit it hard enough to get the letter?
            {
                Debug.Log("Hard hitting stuff!");
                if (coll.gameObject.GetComponent<BirdController>().hasMail()) //Does it have the mail?
                {
                    coll.gameObject.GetComponent<BirdController>().dropMail();
                    Debug.Log("Bitch had my mail!");
                }
                coll.gameObject.GetComponent<BirdController>().applyStun();
                Vector2 knockback = -GetComponent<Rigidbody>().velocity.normalized * knockbackForce; //Calculate knockback
                coll.gameObject.GetComponent<Rigidbody>().AddForce(knockback, ForceMode.Impulse); //Change from impulse to force to test effects
            }            
        }   
        if (coll.gameObject.tag == "Mail")
        {
            this.hasLetter = true;
            letter = coll.gameObject;
            //letter.GetComponent<Letter>().setOwner(gameObject);            
        }     

    }

    //Check if this bird has the letter
    public bool hasMail() {
        return hasLetter;
    }

    //Dropping the letter
    public void dropMail()
    {
        hasLetter = false;
        //letter.GetComponent<Letter>().Dropped();
    }

    //Apply the stun
    public void applyStun()
    {
        isStunned = true;
    }

    //Movement functions

    public void rightWing()
    {
        if (!isStunned)
        {
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(movementPowerX, movementPowerY));
            //GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(movementPowerX, movementPowerY),transform.position);
            //GetComponent<Rigidbody2D>().AddTorque(movementPowerY);
        }
    }

    public void leftWing()
    {
        if (!isStunned)
        {
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(-movementPowerX, movementPowerY));
            //GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(-movementPowerX, movementPowerY), transform.position);
            //GetComponent<Rigidbody2D>().AddTorque(movementPowerY);
        }
    }

    //Mechanics

    //Mad dash
    public void dash()
    {

    }

    //Drop the bomb
    public void shit()
    {

    }
}
