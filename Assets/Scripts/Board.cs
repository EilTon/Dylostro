using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
	public GameObject _prefabCase;
	public List<Case> _cases;
	public List<ChallengeScriptableObject> _challenges;
	public float _offsetX = 1.5f;

	private void Awake()
	{
		FindObjectOfType<GameManager>().generateBoard += GenerateBoardEventHandler;
	}

	void GenerateBoardEventHandler(object sender, GameManager.GenerateBoardEventArgs e) // fonction de l'event GenerateBoard
	{
		for (int i = 0; i < e.caseToGenerate; ++i)
		{
			Instantiate(_prefabCase);
			_prefabCase.transform.position = new Vector2(0+(i*_offsetX),0);
			Case cell = _prefabCase.GetComponent<Case>();
			cell._challenge = RandomChallenge();
			_cases.Add(cell);
		}
		Debug.Log(_cases[2]._challenge._description);
	}

	ChallengeScriptableObject RandomChallenge()
	{
		int randomNumber = UnityEngine.Random.Range(0, _challenges.Count);
		return _challenges[randomNumber];
	}
}
