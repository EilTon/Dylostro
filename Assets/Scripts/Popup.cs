using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
	public Text _textShow;
	public Text _textDrink;
	public Text _textPoint;

	public event EventHandler<EventArgs> yesButton;
	public event EventHandler<EventArgs> noButton;

	bool _isWin;

	static GameObject _diceButton;
	static GameObject _popup;
	static List<Player> _players;
	static List<ConstraintScriptableObject> _constraints;
	static ChallengeScriptableObject _challenge;
	static Board _board;
	private void Awake()
	{
		yesButton += YesButtonEventHandler;
		noButton += NoButtonEventHandler;
	}

	public void Show(string textChallenge, string textDrink, string textPoint)
	{
		_popup = gameObject;
		_popup.SetActive(true);
		_textShow.text = textChallenge;
		_textDrink.text = "A boire: " + textDrink;
		_textPoint.text = "Point: "+ textPoint;
	}

	void YesButtonEventHandler(object sender, EventArgs e)
	{
		_isWin = true;
		YesNoButton();
	}

	void NoButtonEventHandler(object sender, EventArgs e)
	{
		_isWin = false;
		YesNoButton();
	}

	public void YesButton()
	{
		if (yesButton != null)
		{
			yesButton?.Invoke(this, new EventArgs());
		}
	}

	public void NoButton()
	{
		if (noButton != null)
		{
			noButton?.Invoke(this, new EventArgs());
		}
	}

	public void YesNoButton()
	{
		_popup.SetActive(false);
		foreach (Player player in _players)
		{
			if (_isWin)
			{
				player._score = player._score + _challenge._points;
				Debug.Log(player._score);
			}
			else
			{
				player._score = player._score - _challenge._points;
				Debug.Log(player._score);
			}
		}
		_diceButton.SetActive(true);
		_board.SetTextScoreAndPseudo();
	}

	public void SetPlayers(List<Player> players)
	{
		_players = players;
	}

	public void SetChallenge(ChallengeScriptableObject challenge)
	{
		_challenge = challenge;
	}

	public void SetConstraints(List<ConstraintScriptableObject> constraints)
	{
		_constraints = constraints;
	}

	public void SetDiceButton(GameObject diceButton)
	{
		_diceButton = diceButton;
	}

	public void SetBoard(Board board)
	{
		_board = board;
	}
}
