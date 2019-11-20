using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour
{
	public ChallengeScriptableObject _challenge;
	public List<Player> _players;

	private void Start()
	{
		ChallengeToPlayer();
	}

	void ChallengeToPlayer()
	{
		if (_challenge._numberPlayer > 1)
		{
			string names ="";
			foreach(var player in _players)
			{
				names = names + player.name + " ";
			}
			Debug.Log(names + _challenge._description.ToString());
		}
		else
		{
			Debug.Log(_players[0].name+ " " + _challenge._description.ToString());
		}
	}
}
