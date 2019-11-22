using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public void Show()
	{
		GetComponent<GameObject>().SetActive(true);
	}
}
