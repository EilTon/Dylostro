using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
	int _CaseToGenerate;


	public BoardGenerator(int caseToGenerate)
	{
		_CaseToGenerate = caseToGenerate;
	}

	public void GenerateBoard()
	{
		for(int i =0;i<_CaseToGenerate;++i)
		{
			Debug.Log("Case Generate");
		}
	}
}
