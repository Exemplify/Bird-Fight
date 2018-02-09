using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Rewired;

public class GameController : MonoBehaviour {

    public BirdController[] birds;


    public GameObject[] players = new GameObject[4];
    public Player[] playerCon = new Player[4];

    public LetterController letter;

    private bool isInMenu = true;
    public Gamestate gamestate = Gamestate.Reset;

    private float timerLevelStart;
    private float levelStartPeriod;
    public int winningPlayerNum;
    void Awake()
    {
        for (var i = 0; i < playerCon.Length; ++i)
        {
            playerCon[i] = ReInput.players.GetPlayer(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CloseMenu();

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

        if (!isInMenu)
        {
            for (var i = 0; i < playerCon.Length; ++i)
            {
                if (playerCon[i].GetButtonDown("Fire"))
                {
                    if (!players[i].activeSelf)
                    {
                        players[i].SetActive(true);
                        Scores[i].SetActive(true);
                        Tooltips[i].SetActive(false);
                        if (!playerInGame)
                        {
                            musicSource.clip = gameMusic;
                            musicSource.Play();
                            playerInGame = true;
                        }

                    }
                }
            }
        }

    }

    #region Sound and GUI Control

    [Header("Background Music")]
    public AudioSource musicSource;
    public AudioClip gameMusic;
    [Header("GUI Objects")]
    public GameObject menuPanel;
    public GameObject[] Scores;
    public GameObject[] Tooltips;

    //private variables
    private bool playerInGame = false;

    public void CloseMenu()
    {

        if (isInMenu && playerCon[0].GetButtonDown("Start") )
        {
            isInMenu = false;
            menuPanel.SetActive(false);
            GetComponent<LetterController>().LetterSpawn();
            foreach (GameObject GO in Tooltips)
                GO.SetActive(true);
        }
    }
#endregion
}
