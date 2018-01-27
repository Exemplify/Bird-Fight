using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreController : MonoBehaviour {

	private static List<int> _score= new List<int>(4);

	public static List<int> getScores()
	{
		return _score;
	}

	public static void AddScore(Players player)
	{
		_score[(int)player]++;
	}

}
