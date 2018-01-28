using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public BirdController[] birds;

    public LetterController letter;

    public Gamestate gamestate = Gamestate.Reset;

    private float timerLevelStart;
    private float levelStartPeriod;
    public int winningPlayerNum;
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
            //letter.LetterRespawn();
            gamestate = Gamestate.Playing;
        }
        else if (gamestate == Gamestate.Gameover)
        {
            //WIN SCREEN
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }        
	}
}
