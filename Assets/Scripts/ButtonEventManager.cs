using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEventManager : MonoBehaviour
{
	public event EventHandler<EventArgs> throwDice;

	public void ThrowDice()
	{
		if (throwDice != null)
		{
			throwDice?.Invoke(this, new EventArgs());
		}
	}
}
