using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreController : MonoBehaviour {

    public Text[] Scores;
	private int[] _score = new int[4];

	public int[] getScores()
	{
		return _score;
	}

	public void AddScore(Players player)
	{
        print((int)player);
		_score[(int)player]++;
        //print("Player " + (int)player + " score: " + _score[(int)player]);

        Scores[(int)player].text = _score[(int)player].ToString();

        if (_score[(int)player] > 2)
        {
            GetComponent<GameController>().winningPlayerNum = (int)player+1;
            GetComponent<GameController>().gamestate = Gamestate.Gameover;
        }        
    }

}
