using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class TriggerScript : MonoBehaviour {

    public GameObject WingAnchor;
    public GameObject Body;
    public string WingSide;
    public float Lift;

    private int playerId = 0; // The Rewired player id of this character
    private Player player;

    private float TriggerValue = 0;
    private float LastTriggerValue = 0;
    private float BodyRotation = 0;
    private BirdController birdCon;


    void Awake ()
    {
        birdCon = Body.GetComponent<BirdController>();
        playerId = birdCon.playerId;
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
    }

    private void Start()
    {
        birdCon = Body.GetComponent<BirdController>();
        playerId = birdCon.playerId;
    }
    // Update is called once per frame
    void Update()
    {
        TriggerValue = 0;

        if (!birdCon.isStunned)
        {
            BodyRotation = Body.transform.rotation.eulerAngles.z;

            if (WingSide.Equals("Right"))
            {
                TriggerValue = player.GetAxis("Flap Right");

                WingAnchor.transform.rotation = Quaternion.Euler(0, 0, BodyRotation - 180 - 60 * (-0.5f + TriggerValue));

                if (LastTriggerValue + 0.1 < TriggerValue)
                {

                    Vector3 ForceDirection = Body.transform.up;
                    ForceDirection = ForceDirection.normalized * Lift;
                    Body.GetComponent<Rigidbody>().AddForce(ForceDirection);
                    Body.GetComponent<RotateScript>().IncrementClockwise(-1f);

                    if ((Body.transform.eulerAngles.z < 1 && Body.transform.eulerAngles.z > -1) || Body.transform.eulerAngles.z > 359)
                    {
                        Body.GetComponent<RotateScript>().IncrementClockwise(-1f);
                    }
                }
            }
            
            if(WingSide.Equals("Left"))
            {
                TriggerValue = player.GetAxis("Flap Left");

                WingAnchor.transform.rotation = Quaternion.Euler(0, 0, BodyRotation + 60 * (-0.5f + TriggerValue));
                if (LastTriggerValue + 0.1 < TriggerValue)
                {

                    Vector3 ForceDirection = Body.transform.up;
                    ForceDirection = ForceDirection.normalized * Lift;
                    Body.GetComponent<Rigidbody>().AddForce(ForceDirection);
                    Body.GetComponent<RotateScript>().IncrementClockwise(1f);

                    if ((Body.transform.eulerAngles.z < 1 && Body.transform.eulerAngles.z > -1) || Body.transform.eulerAngles.z > 359)
                    {
                        Body.GetComponent<RotateScript>().IncrementClockwise(1f);
                    }
                }
            }

            LastTriggerValue = TriggerValue;
        }
    }
}
