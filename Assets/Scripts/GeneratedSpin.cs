using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratedSpin : MonoBehaviour
{
	public GameObject _prefab;
	public GameObject _ball;
	public Text _textPseudo;
	public Text _textScore;

	List<string> _players;
	int _case;
	List<ChallengeScriptableObject> _challenges = new List<ChallengeScriptableObject>();
	private void Start()
	{
		setChallengeFromResources("Alcool");
		setChallengeFromResources("Chant");
		setChallengeFromResources("Imitation");
		setChallengeFromResources("Sport");
		setChallengeFromResources("Subir");
		FindObjectOfType<GameManager>().generatedSpin += GenerateSpinEventHandler;
	}

	void GenerateSpinEventHandler(object sender, GameManager.GenerateBoardEventArgs e)
	{
		_case = e.caseToGenerate;
		_players = e.pseudosPlayer;
		CreateCaseAndBall();
	}

	void CreateCaseAndBall()
	{
		CircleCollider2D collider = GetComponent<CircleCollider2D>();
		float radius = collider.radius;
		for (int i = 0; i < _case; i++)
		{
			float angle = i * Mathf.PI * 2f / _case;
			Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
			GameObject go = Instantiate(_prefab, newPos, Quaternion.identity);
			Case cell = go.GetComponent<Case>();
			cell._challenge = RandomChallenge();
		}
		GameObject goSphere = Instantiate(_ball);
		_ball.transform.position = new Vector2(0, radius);
		goSphere.transform.parent = gameObject.transform;
		GetComponent<Spin>().setBall(_ball);
	}

	void setChallengeFromResources(string folder)
	{
		Object[] test;
		test = Resources.LoadAll(folder, typeof(ChallengeScriptableObject));
		foreach (Object obj in test)
		{
			_challenges.Add((ChallengeScriptableObject)obj);
		}
	}

	ChallengeScriptableObject RandomChallenge()
	{
		bool isGood = true;
		int randomNumber = UnityEngine.Random.Range(0, _challenges.Count);
		while (isGood == true)
		{
			isGood = true;
			if (_challenges[randomNumber]._numberPlayer > _players.Count)
			{
				randomNumber = UnityEngine.Random.Range(0, _challenges.Count);
			}
			else
			{
				isGood = false;
			}
		}
		return _challenges[randomNumber];
	}

}
