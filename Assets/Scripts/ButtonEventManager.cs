using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEventManager : MonoBehaviour
{
	public event EventHandler<ThrowDiceEventArgs> throwDice;
	public int _howManyDice;

	public class ThrowDiceEventArgs : EventArgs // parameter Event
	{
		public int numberThrowDice;
	}

	public void ThrowDice()
	{
		if (throwDice != null)
		{
			throwDice?.Invoke(this, new ThrowDiceEventArgs() { numberThrowDice = _howManyDice});
		}
	}
}
