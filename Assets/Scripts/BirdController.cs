using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Primary controller for the bird. Contains states for if it has the letter, etc.
public class BirdController : MonoBehaviour {

    public Players playerNumber;
    public Collider thisCollider; //Reference to this bird's collider
    public float movementPowerX; //The force of horizontal movement applied by each wing
    public float movementPowerY;//The force of vertical movement applied by each wing
    public float transferMagnitude; //The power threshold for releasing the letter
    public float stunTime; //The amount of time bird is stunned for
    public float knockbackForce;
    public bool IsPlaying=false;
    public GameObject LeftFoot;
    public GameObject RightFoot;
    public int PlayerNumber;

	//Shit-related activities
	public GameObject shitPrefab;
    private GameObject shit;

	//poop animations
	public Sprite poopedHat;
	public Sprite cleanHat;
	public SpriteRenderer hatSpriteRen;
	public float PoopedSpriteTime = 3;

	public bool hasLetter; //If this bird has the letter
    public GameObject letter; //The letter game object
    private float timeLeft; //Stun time remaining
    private float shitTimeLeft; //Stun time remaining
    public bool isStunned; //Status effect for being unable to move

    // Use this for initialization
    void Start () {
        isStunned = false;
        thisCollider = GetComponent<Collider>();
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

            if (shitTimeLeft > 0)
            {
                shitTimeLeft -= Time.deltaTime;
            }
            else if (Input.GetButtonDown("P"+PlayerNumber+"Shit"))
            {
                print("Shit");
                shitTimeLeft = 3;
                Shit();
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
                timeLeft = stunTime;
                if (GetComponent<Rigidbody>().velocity.magnitude > coll.gameObject.GetComponent<Rigidbody>().velocity.magnitude)
                {
                    if (coll.gameObject.GetComponent<BirdController>().hasMail()) //Does it have the mail?
                    {
                        coll.gameObject.GetComponent<BirdController>().dropMail();
                        Debug.Log("Bitch had my mail!");

                        coll.gameObject.GetComponent<BirdController>().RightFoot.GetComponent<FootScript>().AttachedLetter = null;
                        coll.gameObject.GetComponent<BirdController>().LeftFoot.GetComponent<FootScript>().AttachedLetter = null;

                        coll.gameObject.GetComponent<BirdController>().RightFoot.GetComponent<FootScript>().timeLeft = 3;
                        coll.gameObject.GetComponent<BirdController>().LeftFoot.GetComponent<FootScript>().timeLeft = 3;

                        letter = coll.gameObject.GetComponent<BirdController>().letter;

                        if (Vector3.Distance(letter.transform.position, LeftFoot.transform.position) < Vector3.Distance(letter.transform.position, RightFoot.transform.position))
                        {
                            letter.GetComponent<Rigidbody>().AddForce((LeftFoot.transform.position - letter.transform.position).normalized * 20);
                        }
                        else
                        {
                            letter.GetComponent<Rigidbody>().AddForce((RightFoot.transform.position - letter.transform.position).normalized * 20);
                        }
                    }
                    coll.gameObject.GetComponent<BirdController>().applyStun();
                    Vector3 knockback = -GetComponent<Rigidbody>().velocity.normalized * knockbackForce; //Calculate knockback
                    coll.gameObject.GetComponent<Rigidbody>().AddForce(knockback, ForceMode.Impulse); //Change from impulse to force to test effects
                }
            }

        }
    }

    //Check if this bird has the letter
    public bool hasMail() {
        return hasLetter;
    }

    //Dropping the letter
    public void dropMail()
    {
		if (hasLetter)
		{
			hasLetter = false;
			letter.GetComponent<Letter>().Dropped();
		}
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
    public void Shit()
    {
        shit = Instantiate(shitPrefab, this.transform.position + transform.up.normalized * -1.75f, Quaternion.identity);
        shit.GetComponent<Renderer>().material.color = Color.white;
        shit.GetComponent<Rigidbody>().AddForce(transform.up * -30, ForceMode.Impulse);
    }

	public void PoopedSprite()
	{
		hatSpriteRen.sprite = poopedHat;
		Invoke("ChangeFromPoopedSprite", PoopedSpriteTime);
	}

	private void ChangeFromPoopedSprite()
	{
		hatSpriteRen.sprite = cleanHat;
	}
}
