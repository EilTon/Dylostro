using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
	public float _speed = 0;
	public float _acceleration = -1;
	bool _isTrigger = false;

	GameObject _ball;
	private void Update()
	{
		if (Input.GetKey(KeyCode.Mouse0))
		{
			this._speed = 20;
		}



		if (Input.GetKey(KeyCode.Mouse1))
		{
			_isTrigger = true;
		}

		if (_isTrigger == true)
		{
			if (this._speed > 0)
			{
				this._speed += _acceleration * Time.deltaTime;
			}
			else
			{
				this._speed = 0;
			}

		}

		if(_speed == 0 && _isTrigger == true)
		{
			_isTrigger = false;
			string text = _ball.GetComponent<Ball>().getTExt();
			Debug.Log(text);
		}

		transform.Rotate(new Vector3(0, 0, this._speed), Space.Self);
	}

	public void setBall(GameObject ball)
	{
		_ball = ball;
	}


}
