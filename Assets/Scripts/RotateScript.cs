﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour {

    public GameObject Body;
    public float RotationAmount;


    // Update is called once per frame
    void Update()
    {
        Body.transform.Rotate(new Vector3(0, 0, 1), RotationAmount);


        if (RotationAmount < 0)
        {
            IncrementCounterClockwise(0.1f);
        }
        else if (RotationAmount > 0)
        {
            IncrementClockwise(0.1f);
        }

        if (((Body.transform.eulerAngles.z < 1 && Body.transform.eulerAngles.z > -1) || Body.transform.eulerAngles.z > 359) && RotationAmount < 2 && RotationAmount > -2)
        {
            Body.transform.rotation.eulerAngles.Set(0, 0, 0);
            RotationAmount = 0;
        }
        else
        {

            if (Body.transform.rotation.eulerAngles.z <= 180 && Body.transform.rotation.eulerAngles.z > 0)
            {
                Body.transform.Rotate(0, 0, -1);
            }
            else
            {
                Body.transform.Rotate(0, 0, 1);
            }
        }
    }

    public void IncrementClockwise(float Degrees)
    {
        RotationAmount -= Degrees;
    }

    public void IncrementCounterClockwise(float Degrees)
    {
        RotationAmount += Degrees;
    }
}
