using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	private bool isShowingInstructions;
	private MeshRenderer ownRenderer, childsRenderer;
	public GameObject child;

	void Start ()
	{
		isShowingInstructions = false;
		ownRenderer = GetComponent<MeshRenderer> ();
		childsRenderer = child.GetComponentInChildren<MeshRenderer> ();

	}

	void Update ()
	{
		
		if (Input.anyKeyDown) {
			if (Input.GetKeyDown ("escape")) {
				Application.Quit ();
			} else {
				if (!isShowingInstructions) {
					//this.gameObject.SetActive (true);
					ownRenderer.enabled = true;
					childsRenderer.enabled = true;
					isShowingInstructions = true;
				} else {
					SceneManager.LoadScene ("Main");
					//load next scene
				}
			}
		}
	}

	private void turnVisible ()
	{

	}

	private void turnInvisible ()
	{

	}
}
