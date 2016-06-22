using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public Color inactive, active;
	public string[] words;


	private MeshRenderer thisMesh;

	private char[] currentWord;
	private int wordIndex;
	private int letterIndex;

	private bool wasGameWon;

	private int beeps, millisOn, millisOff;

	private float timeCounter;

	// Use this for initialization
	void Start ()
	{
		timeCounter = -1;

		thisMesh = GetComponent<MeshRenderer> ();
		turnBlue ();

		wasGameWon = false;

		if (words.Length == 0) { //failsafe
			words.SetValue ("hello", 0); //TODO: ensure at least 1 position
		}
		wordIndex = 0;
		letterIndex = 0;

		setCurrentWord ();

		beeps = 0;
		millisOn = 0;
		millisOff = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey ("escape")) {
			Application.Quit ();
		}
		if (wasGameWon) {
			return;
		}

		if (Input.anyKeyDown) {
			string c = getCurrentLetter ().ToString ();
			if (Input.GetKeyDown (c.ToLower ())) {
				setNextLetter ();
			} else {
				failWord ();
			}
		}

		evaluateColor ();

	}

	private void evaluateColor ()
	{
		//TODO: doesn't quite work

		if (beeps > 0) {
			//needs more beeping
			if (timeCounter == -1) { //haven't started beeping
				timeCounter = Time.realtimeSinceStartup;
				turnRed ();
			} else if (this.enabled == true && Time.realtimeSinceStartup - timeCounter >= (float)millisOn / 1000.0) {//enough turning it on
				turnBlue ();
				timeCounter = Time.realtimeSinceStartup;
			} else if (this.enabled == false && timeCounter != -1 && Time.realtimeSinceStartup - timeCounter >= (float)millisOff / 1000.0) {//enough turning it off
				timeCounter = -1;
				beeps--;
			}
		}
		
	}

	private void turnRed ()
	{
		thisMesh.enabled = true;
	}

	private void turnBlue ()
	{
		thisMesh.enabled = false;
	}

	private void addColor (int _beeps, int _millisOn, int _millisOff)
	{
		beeps = _beeps;
		millisOn = _millisOn;
		millisOff = _millisOff;
		timeCounter = -1;
	}

	private void setNextLetter ()
	{
		letterIndex++;
		if (letterIndex >= currentWord.Length) {
			
			
			wordIndex++;
			if (wordIndex >= words.Length) {
				//won the game
				winGame ();
				return;
			}
			letterIndex = 0;
			setCurrentWord ();

			//won this word
			addColor (2, 1000, 500);

		} else {
			//won this letter
			addColor (1, 1000, 1000);
		}
	}

	private void failWord ()
	{
		//animation of ending
		addColor (8, 500, 500);
		letterIndex = 0;
	}

	private void setCurrentWord ()
	{
		currentWord = words [wordIndex].ToCharArray ();
	}

	private void winGame ()
	{
		//turn on forever
		turnRed ();
		wasGameWon = true;
	}

	private char getCurrentLetter ()
	{
		return currentWord [letterIndex];
	}

	/*
	private void checkCharacter (char c)
	{


		if (Input.GetKeyDown (c.ToString ().ToUpper ()) || Input.GetKeyDown (c.ToString ().ToLower ())) {
			//if (wasCurrentLetter (c)) {
			setNextLetter ();
		} else {
			failWord ();
		}
	}

	private bool wasCurrentLetter (char c)
	{
		return currentWord [letterIndex] == c;
	}
*/
}

