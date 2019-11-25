using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
	public Text _textShow;
	public Button _noButton;
	public Button _yesButton;

	public event EventHandler<EventArgs> yesButton;
	public event EventHandler<EventArgs> noButton;

	List<Player> _players;
	ChallengeScriptableObject _challenge;

	static GameObject _popup;

	private void Awake()
	{
		yesButton += YesButtonEventHandler;
		noButton += NoButtonEventHandler;
	}

	public void Show(string text)
	{
		_popup = gameObject;
		_popup.SetActive(true);
		_textShow.text = text;
	}

	void YesButtonEventHandler(object sender, EventArgs e)
	{
		YesNoButton();
	}

	void NoButtonEventHandler(object sender, EventArgs e)
	{
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
	}

	public void SetPlayers(List<Player>players)
	{
		_players = players;
	}

	public void SetChallenge(ChallengeScriptableObject challenge)
	{
		_challenge = challenge;
	}
}
