using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public BirdController[] birds;

    public enum Gamestate { Playing, Reset, Gameover};

    public LetterController letter;

    Gamestate gamestate = Gamestate.Reset;

    private float timerLevelStart;
    private float levelStartPeriod;

    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gamestate == Gamestate.Playing)
        {
            timerLevelStart += Time.deltaTime;
            if (timerLevelStart > levelStartPeriod)
            {
                foreach (BirdController bird in birds)
                {
                    if (!bird.IsPlaying)
                        bird.IsPlaying = true;
                }
            }
        }
        else if (gamestate == Gamestate.Reset)
        {
            letter.ResetLetter();
            gamestate = Gamestate.Playing;
        }
        else if (gamestate == Gamestate.Gameover)
        {

        }
	}
}
