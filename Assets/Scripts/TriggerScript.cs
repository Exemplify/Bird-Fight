﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {

    public GameObject WingAnchor;
    public GameObject Body;
    public string PlayerNum;
    public string WingSide;

    private float TriggerValue;
    private float LastTriggerValue;
    private float BodyRotation;
    //private

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        TriggerValue = Input.GetAxis("P" + PlayerNum + WingSide);
        BodyRotation = Body.transform.rotation.eulerAngles.z;

        if (WingSide.Equals("Right"))
        {
            Vector3 offset = WingAnchor.transform.parent.position;
            offset += new Vector3(0.5f, 0, 0);

            WingAnchor.transform.rotation = Quaternion.Euler(0, 0, BodyRotation - 180 - 120 * (-0.5f + TriggerValue));

            if (LastTriggerValue + 0.1 < TriggerValue)
            {
                //Body.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(-2, 30, 0), Body.transform.position + new Vector3(0.5f, 0, 0));
                Vector3 ForceDirection = Body.transform.up;
                ForceDirection = ForceDirection.normalized * 40;
                Body.GetComponent<Rigidbody>().AddForce(ForceDirection);
                Body.GetComponent<RotateScript>().IncrementClockwise(-1f);
            }
        }
        else
        {
            WingAnchor.transform.rotation = Quaternion.Euler(0, 0, BodyRotation + 120 * (-0.5f + TriggerValue));
            if (LastTriggerValue + 0.1 < TriggerValue)
            {
                //WingAnchor.transform.parent.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(2, 30, 0), Body.transform.position + new Vector3(-0.5f, 0, 0));
                Vector3 ForceDirection = Body.transform.up;
                ForceDirection = ForceDirection.normalized * 40;
                Body.GetComponent<Rigidbody>().AddForce(ForceDirection);
                Body.GetComponent<RotateScript>().IncrementClockwise(1f);
            }
        }

        LastTriggerValue = TriggerValue;
    }
}