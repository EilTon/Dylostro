using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	#region Declarations public
	[Range(40,200)]
	public int _numberCase;
	public List<string> _pseudos;

	#endregion

	#region Declarations private

	Board _board;
	GeneratedSpin _spin;

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
	public event EventHandler<GenerateBoardEventArgs> generatedSpin; 

	#endregion

	#region Event Call

	public void GenerateBoard(GenerateBoardEventArgs e) //Invoke Event (permet l'execution)
	{
		if (generateBoard != null)
		{
			generateBoard?.Invoke(this, e);
		}
	}

	public void GeneratedSpin(GenerateBoardEventArgs e) 
	{
		if (generatedSpin != null)
		{
			generatedSpin?.Invoke(this, e);
		}
	}

	#endregion

	private void Start()
	{
		if (_pseudos.Count < 2 || _pseudos.Count > 9)
		{
			_pseudos = new List<string>();
			for (int i = 0; i< 8; i++)
			{
				string pseudo = "Joueur " + (i + 1);
				_pseudos.Add(pseudo);
			}
		}

		GeneratedSpin(new GenerateBoardEventArgs() { caseToGenerate = _numberCase, pseudosPlayer = _pseudos });
	}
}
