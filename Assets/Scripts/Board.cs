using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class Board : MonoBehaviour
{
	#region declarations public
	public GameObject _prefabCase;
	public GameObject _prefabPlayer;
	public GameObject _popup;
	public float _offsetX = 1.5f;
	public List<ChallengeScriptableObject> _challenges;
	public List<ConstraintScriptableObject> _constraints;
	[HideInInspector]
	public List<GameObject> _cases;

	public Text _textPseudo;
	public Text _textScore;
	#endregion

	#region declarations private
	List<GameObject> _players;
	int _order = 0;
	Camera _camera;
	Vector3 _finalCase;
	GameObject _diceButton;
	
	#endregion

	private void Awake()
	{
		FindObjectOfType<GameManager>().generateBoard += GenerateBoardEventHandler;
		FindObjectOfType<ButtonEventManager>().throwDice += ThrowDiceEventHandler;
		setChallengeFromResources("Alcool");
		setChallengeFromResources("Chant");
		setChallengeFromResources("Imitation");
		setChallengeFromResources("Sport");
		setChallengeFromResources("Subir");
		_camera = Camera.main;
	}
	#region Event Handler
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
		SetTextScoreAndPseudo();
	}

	void ThrowDiceEventHandler(object sender, ButtonEventManager.ThrowDiceEventArgs e)
	{
		_diceButton = e.buttonDice;
		float numberOfDice = 0;
		for (int i = 0; i < e.numberThrowDice; ++i)
		{
			numberOfDice += UnityEngine.Random.Range(1, 7);
		}
		_players[_order].GetComponent<Player>().MovePlayer(numberOfDice, _finalCase, _offsetX);
		_camera.transform.position = new Vector3(_players[_order].transform.position.x, _players[_order].transform.position.y, _camera.transform.position.z);
		CheckCase(_players[_order]);
	}
	#endregion

	#region Helper
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
		ChallengeScriptableObject challenge = null;
		string textPopUp = "";
		List<Player> players = new List<Player>();
		challenge = _cases.Where(x => x.transform.position.x == player.transform.position.x).FirstOrDefault().GetComponent<Case>()._challenge; // probleme quand on a 2 dé ou plus
		players.Add(player.GetComponent<Player>());
		textPopUp = CheckChallenge(challenge, textPopUp, player, players);
		Popup popup = _popup.GetComponent<Popup>();
		popup.SetConstraints(_constraints);
		popup.SetChallenge(challenge);
		popup.SetPlayers(players);
		popup.SetDiceButton(_diceButton);
		popup.SetBoard(GetComponent<Board>());
		popup.Show(textPopUp, challenge._needToDrink.ToString(), challenge._points.ToString());
	}

	ChallengeScriptableObject RandomChallenge()
	{
		bool isGood = true;
		int randomNumber = UnityEngine.Random.Range(0, _challenges.Count);
		while (isGood == true)
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

	string CheckChallenge(ChallengeScriptableObject challenge, string textPopUp, GameObject player, List<Player> players)
	{
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
		return textPopUp;
	}
	public void SetTextScoreAndPseudo()
	{
		_textPseudo.text = _players[_order].GetComponent<Player>()._pseudo;
		_textScore.text = _players[_order].GetComponent<Player>()._score.ToString() + " points";
	}
	#endregion

	void setChallengeFromResources(string folder)
	{
		Object[] test;
		test = Resources.LoadAll(folder, typeof(ChallengeScriptableObject));
		foreach(Object obj in test)
		{
			_challenges.Add((ChallengeScriptableObject)obj);
		}
	}
}