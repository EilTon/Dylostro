using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	public int _numberCase;
	Board _board;
	
	#region Event Args
	public class GenerateBoardEventArgs : EventArgs // parameter Event
	{
		public int caseToGenerate;
	}
	#endregion

	#region Event Handler
	public event EventHandler<GenerateBoardEventArgs> generateBoard; // call event (utiliser dans un autre script pour appel)
	#endregion

	public void GenerateBoard(GenerateBoardEventArgs e) //Invoke Event (permet l'execution)
	{
		if (generateBoard != null)
		{
			generateBoard?.Invoke(this, e);

		}
	}

	private void Start()
	{
		GenerateBoard(new GenerateBoardEventArgs(){ caseToGenerate = _numberCase });
	}
}
