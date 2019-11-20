using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Challenge",menuName ="Challenge/New Challenge")]


public class ChallengeScriptableObject : ScriptableObject
{
	public string _category;
	public string _description;
	public int _points;
	public bool _validation;
	public int _numberPlayer;
}
