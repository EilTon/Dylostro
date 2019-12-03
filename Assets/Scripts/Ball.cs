using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	static string text;
	static ChallengeScriptableObject _challenge;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		try
		{
			_challenge = collision.GetComponent<Case>()._challenge;
		}
		catch(Exception ex)
		{

		}
		
	}

	public string getTExt()
	{
		return _challenge._description;
	}
}
