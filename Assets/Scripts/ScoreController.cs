using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreController : MonoBehaviour {

	private List<int> _score;

	public List<int> getScores()
	{
		return _score;
	}

	public void AddScore(Players player)
	{
		_score[(int)player]++;
	}

}
