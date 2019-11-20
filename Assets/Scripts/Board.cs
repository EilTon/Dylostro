using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
	
	private void Awake()
	{
		FindObjectOfType<GameManager>().generateBoard += GenerateBoardEventHandler;
	}
	public List<Case> _cases;

	void GenerateBoardEventHandler(object sender, GameManager.GenerateBoardEventArgs e) // fonction de l'event GenerateBoard
	{
		for (int i = 0; i < e.caseToGenerate; ++i)
		{
			Debug.Log(i);
		}
	}
}
