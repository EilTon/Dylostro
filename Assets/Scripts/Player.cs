using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{

	#region Declarations publique
	public string _pseudo;
	public int _score;
	public List<string> _constraints;

	#endregion

	#region Declarations prive

	bool _isConstraint;

	#endregion

	public void MovePlayer(float numberOfDice)
	{
		Vector3 move = new Vector3((transform.position.x + numberOfDice) * 1.5f, 1, 0);
		transform.position = move;
	}

}
