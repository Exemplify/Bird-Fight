using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMenuController : MonoBehaviour
{
    public bool WillPlay;

    public enum Gamestate { Playing, Active, NotActive };

    public LetterController letter;

    Gamestate gamestate = Gamestate.NotActive;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gamestate == Gamestate.Playing)
            WillPlay = true;
    }
}
