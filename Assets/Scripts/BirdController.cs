using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;


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
    public int playerId;
    private Player player;
    public GameObject featherPrefab;

    public bool isEnabled = false;

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

    private void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
    }
    // Use this for initialization
    void Start () {
        hatSpriteRen.sprite = cleanHat;
        isStunned = false;
        thisCollider = GetComponent<Collider>();
        gameObject.SetActive(isEnabled);
    }
	
    //Called upon collision
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Player") //Check if it is another bird
        {
           
            if (coll.relativeVelocity.magnitude > transferMagnitude) //If so, did you hit it hard enough to get the letter?
            {

                timeLeft = stunTime;
                if (GetComponent<Rigidbody>().velocity.magnitude > coll.gameObject.GetComponent<Rigidbody>().velocity.magnitude)
                {
                    ContactPoint contact = coll.contacts[0];
                    FeatherParticleEffect(contact.point);

                    if (coll.gameObject.GetComponent<BirdController>().hasMail()) //Does it have the mail?
                    {
                        coll.gameObject.GetComponent<BirdController>().dropMail();
                       
                        letter = coll.gameObject.GetComponent<BirdController>().letter;

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

            RightFoot.GetComponent<FootScript>().AttachedLetter = null;
            LeftFoot.GetComponent<FootScript>().AttachedLetter = null;

            RightFoot.GetComponent<FootScript>().timeLeft = 3;
            LeftFoot.GetComponent<FootScript>().timeLeft = 3;
        }
    }


    //Apply the stun
    public void applyStun()
    {
        isStunned = true;
        timeLeft = stunTime;
    }

#region Mechanics
    //Drop the bomb
    public void Shit()
    {
        shit = Instantiate(shitPrefab, this.transform.position + transform.up.normalized * -1.75f, Quaternion.identity);
        shit.GetComponent<Renderer>().material.color = Color.white;
        shit.GetComponent<Rigidbody>().AddForce(transform.up * -20, ForceMode.Impulse);
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
    #endregion

    #region Juice 
    private bool feathersExist;
    private float featherDelay;

    private void SetFeatherFalse()
    {
        feathersExist = false;
    }
    private void FeatherParticleEffect(Vector3 spawnPosition)
    {
        if (!feathersExist)
        {
            feathersExist = true;
            Instantiate(featherPrefab, spawnPosition, Quaternion.Euler(0, 180, 0));
            Invoke("SetFeatherFalse", 1);
        }
    }
    #endregion
}
