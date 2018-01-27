using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootScript : MonoBehaviour
{

    public GameObject Body;
    public GameObject AttachedLetter;
    private Vector3 Offset;
    public float timeLeft;

    // Use this for initialization
    void Start()
    {
        timeLeft = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(AttachedLetter != null)
        {
            AttachedLetter.transform.position = transform.position + Offset;
            //print(AttachedLetter.transform.position);
            //print(transform.position);
        }

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        //print("Collision");
        if (coll.gameObject.tag == "Mail" && timeLeft <= 0)
        {
            //print("Mail");
            Body.GetComponent<BirdController>().hasLetter = true;
            Body.GetComponent<BirdController>().letter = coll.gameObject;

            Body.GetComponent<BirdController>().letter.GetComponent<Letter>().setOwner(gameObject);
            
            //coll.gameObject.transform.position = transform.position;
            coll.gameObject.GetComponent<Rigidbody>().useGravity = false;

            //coll.gameObject.transform.parent = transform;
            AttachedLetter = coll.gameObject;
            Offset = AttachedLetter.transform.position - transform.position;
        }
    }
}
