using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	private MeshRenderer thisMesh;
	private PlayerSpellchecker spellManager;

	void Start ()
	{
		spellManager = GetComponent<PlayerSpellchecker> ();
		spellManager.reset ();
	}

	void Update ()
	{
		if (Input.GetKey ("escape")) {
			Application.Quit ();
		}
		if (!spellManager.wasListCompleted ()) {
			spellManager.asyncUpdate ();
		}

	}
}

