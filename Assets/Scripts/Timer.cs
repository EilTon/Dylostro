using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	public float _time = 10;
	
	public Text _timerText;
    void Start()
    {
		_time = _time * 60;
    }

    void Update()
    {
		int time=(int)_time;
		if(_time<60)
		{
			_timerText.text = (time + " secondes");
		}
		else
		{
			_timerText.text = (time / 60 + " minutes");
		}
        if(_time >=0)
		{
			_time -= Time.deltaTime;
		}
		else
		{
			//fin de partie;
		}
    }
}
