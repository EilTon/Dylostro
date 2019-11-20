using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
	public List<Case> _cases;

	public Board()
	{ }

	public void GenerateBoard(int caseToGenerate)
	{
		for(int i =0;i<caseToGenerate;++i)
		{
			Debug.Log("Case Generate");
		}
	}
}
