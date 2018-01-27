using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour 
{
	public 	GameObject	Owner;
	public 	bool		hasOwner = false;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void setOwner(GameObject newOwner)
	{
		Owner = newOwner;
		hasOwner = true;
		GetComponent<Rigidbody> ().detectCollisions = false;
		GetComponent<Collider> ().enabled = false;
	}

	public void Dropped()
	{
		hasOwner = false;
		GetComponent<Rigidbody> ().detectCollisions = true;
		GetComponent<Collider> ().enabled = true;
	}
}
