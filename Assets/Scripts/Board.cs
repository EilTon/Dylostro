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
	Camera _camera;
	Vector3 _finalCase;

	private void Awake()
	{
		FindObjectOfType<GameManager>().generateBoard += GenerateBoardEventHandler;
		FindObjectOfType<ButtonEventManager>().throwDice += ThrowDiceEventHandler;
		_camera = Camera.main;
	}

	void GenerateBoardEventHandler(object sender, GameManager.GenerateBoardEventArgs e) // fonction de l'event GenerateBoard
	{
		_players = new List<GameObject>();
		for (int i = 0; i < e.pseudosPlayer.Count; ++i)
		{
			CreatePlayer(e.pseudosPlayer[i], i);
		}
		for (int i = 0; i < e.caseToGenerate; ++i)
		{
			CreateCase(i);
		}
		_finalCase = _cases.Last().transform.position;
		_order = 0;
	}

	void ThrowDiceEventHandler(object sender, EventArgs e)
	{
		float numberOfDice = UnityEngine.Random.Range(1, 7);
		_players[_order].GetComponent<Player>().MovePlayer(numberOfDice,_finalCase);
		_camera.transform.position = new Vector3(_players[_order].transform.position.x, _players[_order].transform.position.y, _camera.transform.position.z);
		CheckCase(_players[_order]);
	}

	void CreateCase(int i)
	{
		GameObject cellGo = Instantiate(_prefabCase);
		cellGo.transform.position = new Vector3(0 + (i * _offsetX), 0, 91);
		Case cell = cellGo.GetComponent<Case>();
		cell._challenge = RandomChallenge();
		_cases.Add(cellGo);
	}

	void CreatePlayer(string pseudo, int i)
	{
		GameObject playerGo = Instantiate(_prefabPlayer);
		playerGo.transform.position = new Vector3(0, 1, 91);
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
		if (challenge._numberPlayer > 1)
		{
			if (_order == _players.Count - 1)
			{
				textPopUp = player.GetComponent<Player>()._pseudo + ", " + _players[0].GetComponent<Player>()._pseudo + "" + challenge._description;
				players.Add(_players[0].GetComponent<Player>());
			}
			else
			{
				textPopUp = player.GetComponent<Player>()._pseudo + ", " + _players[_order + 1].GetComponent<Player>()._pseudo + "" + challenge._description;
				players.Add(_players[_order + 1].GetComponent<Player>());
			}
		}
		else
		{
			textPopUp = player.GetComponent<Player>()._pseudo + "" + challenge._description;
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
		bool isGood = true;
		int randomNumber = UnityEngine.Random.Range(0, _challenges.Count);
		while (isGood==true)
		{
			if (_challenges[randomNumber]._numberPlayer > _players.Count)
			{
				randomNumber = UnityEngine.Random.Range(0, _challenges.Count);
			}
			else
			{
				isGood = false;
			}
		}
		return _challenges[randomNumber];
	}
}
