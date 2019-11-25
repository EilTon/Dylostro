using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
	public Text _textShow; 
	public void Show(string text)
	{
		gameObject.SetActive(true);
		_textShow.text = text;
	}
}
