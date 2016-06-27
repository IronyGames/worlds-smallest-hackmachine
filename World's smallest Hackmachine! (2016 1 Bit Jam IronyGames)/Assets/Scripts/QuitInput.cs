using UnityEngine;
using System.Collections;

public class QuitInput : MonoBehaviour
{
	private const string buttonToExit = "escape";

	void Update ()
	{
		if (Input.GetKeyDown ("escape")) {
			Application.Quit ();
		}
	}
}
