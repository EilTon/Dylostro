using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	#region Declarations public

	public int _numberCase;
	public List<string> _pseudos;

	#endregion

	#region Declarations private

	Board _board;

	#endregion
	
	#region Event Args

	public class GenerateBoardEventArgs : EventArgs // parameter Event
	{
		public int caseToGenerate;
		public List<string> pseudosPlayer;
	}

	#endregion

	#region Event Handler

	public event EventHandler<GenerateBoardEventArgs> generateBoard; // call event (utiliser dans un autre script pour appel)
	

	#endregion

	#region Event Call

	public void GenerateBoard(GenerateBoardEventArgs e) //Invoke Event (permet l'execution)
	{
		if (generateBoard != null)
		{
			generateBoard?.Invoke(this, e);
		}
	}
	
	#endregion

	private void Start()
	{
		GenerateBoard(new GenerateBoardEventArgs(){ caseToGenerate = _numberCase, pseudosPlayer = _pseudos });
	}
}
