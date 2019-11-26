using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
	public GameObject _prefabCase;
	public GameObject _prefabPlayer;
	public GameObject _popup;
	public List<ChallengeScriptableObject> _challenges;
	public List<GameObject> _players;
	public List<GameObject> _cases;
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
		CheckCase(_players[_order]);
	}

	void CreateCase(int i)
	{
		GameObject cellGo = Instantiate(_prefabCase);
		cellGo.transform.position = new Vector3(0 + (i * _offsetX), 0,91);
		Case cell = cellGo.GetComponent<Case>();
		cell._challenge = RandomChallenge();
		_cases.Add(cellGo);
	}

	void CreatePlayer(string pseudo, int i)
	{
		GameObject playerGo = Instantiate(_prefabPlayer);
		playerGo.transform.position = new Vector3(0, 1,91);
		Player player = playerGo.GetComponent<Player>();
		player._pseudo = pseudo;
		_players.Add(playerGo);
	}

	void CheckCase(GameObject player)
	{
		string textPopUp;
		List<Player> players = new List<Player>();
		ChallengeScriptableObject challenge = _cases.Where(x => x.transform.position.x == player.transform.position.x).FirstOrDefault().GetComponent<Case>()._challenge;
		players.Add(player.GetComponent<Player>());
		if (challenge._numberPlayer>1)
		{
			if (_order == _players.Count - 1)
			{
				textPopUp= player.GetComponent<Player>()._pseudo + ", " + _players[0].GetComponent<Player>()._pseudo + "" + challenge._description;
				players.Add(_players[0].GetComponent<Player>());
			}
			else
			{
				textPopUp=player.GetComponent<Player>()._pseudo + ", " + _players[_order + 1].GetComponent<Player>()._pseudo + "" + challenge._description;
				players.Add(_players[_order + 1].GetComponent<Player>());
			}
			
		}
		else
		{
			textPopUp=player.GetComponent<Player>()._pseudo+""+challenge._description;
		}
		if (_order >= _players.Count - 1)
		{
			_order = 0;
		}
		else
		{
			++_order;
		}
		Popup popup = _popup.GetComponent<Popup>();
		popup.SetChallenge(challenge);
		popup.SetPlayers(players);
		popup.Show(textPopUp);
	}

	ChallengeScriptableObject RandomChallenge()
	{
		int randomNumber = UnityEngine.Random.Range(0, _challenges.Count);
		return _challenges[randomNumber];
	}
}
