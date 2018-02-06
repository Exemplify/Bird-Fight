using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public BirdController[] birds;
    public GameObject[] Scores;

    public GameObject[] players=new GameObject[4];

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

        if (Input.GetButtonDown("P1Shit"))
        {
            if (!players[0].activeSelf)
            {
                players[0].SetActive(true);
                Scores[0].SetActive(true);
            }            
        }
        if (Input.GetButtonDown("P2Shit"))
        {
            if (!players[1].activeSelf)
            {
                players[1].SetActive(true);
                Scores[1].SetActive(true);
            }
        }
        if (Input.GetButtonDown("P3Shit"))
        {
            if (!players[2].activeSelf)
            {
                players[2].SetActive(true);
                Scores[2].SetActive(true);
            }
        }
        if (Input.GetButtonDown("P4Shit"))
        {
            if (!players[3].activeSelf)
            {
                players[3].SetActive(true);
                Scores[3].SetActive(true);
            }
        }

    }
}
