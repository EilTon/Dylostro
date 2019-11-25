using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEventManager : MonoBehaviour
{
	public event EventHandler<EventArgs> throwDice;
	public event EventHandler<EventArgs> yesButton;
	public event EventHandler<EventArgs> noButton;

	public void ThrowDice()
	{
		if (throwDice != null)
		{
			throwDice?.Invoke(this, new EventArgs());
		}
	}
}
