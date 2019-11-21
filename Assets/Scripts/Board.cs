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

		for (int i = 0; i < e.pseudosPlayer.Count; ++i)
		{
			CreatePlayer(e.pseudosPlayer[i], i);
		}
		_order = _players.Count - 1;
	}

	public void ThrowDiceEventHandler(object sender, EventArgs e)
	{
		int numberOfDice = UnityEngine.Random.Range(1,7);
		Vector3 move = new Vector3(numberOfDice, _players[_order].transform.position.y);
		_players[_order].transform.Translate(move);
		if(_order == 0)
		{
			_order = _players.Count - 1;
		}
		else
		{
			--_order;
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
		Instantiate(_prefabPlayer);
		_prefabPlayer.transform.position = new Vector2(0 - (i * _offsetX), 1);
		Player player = _prefabPlayer.GetComponent<Player>();
		player._pseudo = pseudo;
		_players.Add(_prefabPlayer);
		_cases[0]._players.Add(_prefabPlayer);
	}

	ChallengeScriptableObject RandomChallenge()
	{
		int randomNumber = UnityEngine.Random.Range(0, _challenges.Count);
		return _challenges[randomNumber];
	}
}
