using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public int _numberCase;

	List<string> _cases;
	BoardGenerator _boardGenerator;

	private void Start()
	{
		_boardGenerator = new BoardGenerator(_numberCase);
		_boardGenerator.GenerateBoard();
	}
}
