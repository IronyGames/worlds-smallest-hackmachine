using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	private MeshRenderer thisMesh;
	private PlayerSpellchecker spellManager;
	private PlayerColorManager colorManager;

	void Start ()
	{
		spellManager = GetComponent<PlayerSpellchecker> ();
		colorManager = GetComponent<PlayerColorManager> ();
		spellManager.reset ();
	}

	void Update ()
	{
		if (Input.GetKey ("escape")) {
			Application.Quit ();
		}

		if (spellManager.wasListCompleted () && !colorManager.isCurrentlyClicking ()) {
			SceneManager.LoadScene ("Ending");
		} 
		spellManager.asyncUpdate ();
	}
}

