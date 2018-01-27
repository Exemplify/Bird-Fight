using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreController : MonoBehaviour {

	private List<int> _score= new List<int>(4);

	public List<int> getScores()
	{
		return _score;
	}

	public void AddScore(Players player)
	{
		_score[(int)player]++;

        if (_score[(int)player] > 2)
        {
            GetComponent<GameController>().winningPlayerNum = (int)player+1;
            GetComponent<GameController>().gamestate = Gamestate.Gameover;
        }        
    }

}
