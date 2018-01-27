using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {
    public BirdMenuController[] birds;

    public int playersPlaying = 0;
    public int previousPlayersPlaying = 0;

    private float levelStartTimer = 0;
    private float levelStartPeriod = 10;

    private bool starting = false;

    

    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        playersPlaying = 0;
        for (int i = 0;i<4;i++)
        {
            if (birds[i])
                playersPlaying ++;
        }
        if (playersPlaying > 1)
            starting = true;
        if (starting)
        {
            levelStartTimer += Time.deltaTime;
            if (playersPlaying != previousPlayersPlaying)
                levelStartTimer = 0;
            if (levelStartTimer > levelStartPeriod)
                GoToGame();
        }
        previousPlayersPlaying = playersPlaying;

	}

    public void GoToGame()
    {

    }

}
