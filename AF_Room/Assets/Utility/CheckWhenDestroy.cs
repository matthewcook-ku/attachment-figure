using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWhenDestroy : MonoBehaviour
{
	// Start is called before the first frame update
	private void OnDisable()
	{
		Debug.Log("Disabling Camera: " + gameObject.name);
	}
	private void OnDestroy()
	{
		Debug.Log("Destroying Camera: " + gameObject.name);
	}
}
