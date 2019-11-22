using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
	public GameObject _prefabCase;
	public GameObject _prefabPlayer;
	public List<Case> _cases;
	public List<ChallengeScriptableObject> _challenges;
	public List<GameObject> _players;
	public float _offsetX = 1.5f;
	int _order = 0;
	bool _move = false;

	private void Awake()
	{
		FindObjectOfType<GameManager>().generateBoard += GenerateBoardEventHandler;
		FindObjectOfType<ButtonEventManager>().throwDice += ThrowDiceEventHandler;
	}


	void GenerateBoardEventHandler(object sender, GameManager.GenerateBoardEventArgs e) // fonction de l'event GenerateBoard
	{
		for (int i = 0; i < e.caseToGenerate; ++i)
		{
			CreateCase(i);
		}
		_players = new List<GameObject>();
		for (int i = 0; i < e.pseudosPlayer.Count; ++i)
		{
			CreatePlayer(e.pseudosPlayer[i], i);
		}
		_order = 0;
	}

	void ThrowDiceEventHandler(object sender, EventArgs e)
	{
		float numberOfDice = UnityEngine.Random.Range(1, 7);
		_players[_order].GetComponent<Player>().MovePlayer(numberOfDice);
		if (_order >= _players.Count -1)
		{
			_order = 0;
		}
		else
		{
			++_order;
		}
		
	}


	void CreateCase(int i)
	{
		Instantiate(_prefabCase);
		_prefabCase.transform.position = new Vector2(0 + (i * _offsetX), 0);
		Case cell = _prefabCase.GetComponent<Case>();
		cell._challenge = RandomChallenge();
		_cases.Add(cell);

	}

	void CreatePlayer(string pseudo, int i)
	{
		GameObject playerGo = Instantiate(_prefabPlayer);
		playerGo.transform.position = new Vector2(0, 1);
		Player player = playerGo.GetComponent<Player>();
		player._pseudo = pseudo;
		_players.Add(playerGo);
		_cases[0]._players.Add(playerGo);
	}

	ChallengeScriptableObject RandomChallenge()
	{
		int randomNumber = UnityEngine.Random.Range(0, _challenges.Count);
		return _challenges[randomNumber];
	}
}
