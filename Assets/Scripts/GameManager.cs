using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public int _numberCase;

	List<string> _cases;
	Board _board;

	private void Start()
	{
		_board = new Board();
		_board.GenerateBoard(_numberCase);
	}
}
