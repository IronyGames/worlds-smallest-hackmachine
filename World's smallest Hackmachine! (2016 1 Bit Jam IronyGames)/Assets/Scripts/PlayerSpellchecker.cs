using UnityEngine;
using System.Collections;

public class PlayerSpellchecker : MonoBehaviour
{
	private bool allWordsGuessed;
	private string[] usablePasswords;
	public string[] passwords;

	private char[] currentWord;
	private int wordIndex;
	private int letterIndex;
	private PlayerColorManager colorManager;


	void Start ()
	{
		colorManager = GetComponent<PlayerColorManager> ();
		reset ();
	}

	public void reset ()
	{
		usablePasswords = passwords; //TODO: randomize
		allWordsGuessed = false;
		wordIndex = 0;
		resetWord ();
		setCurrentWord ();
	}

	public void checkInput ()
	{
		if (!colorManager.isCurrentlyClicking ()) {
			if (Input.anyKeyDown) {
				string c = getCurrentLetter ().ToString ();
				if (Input.GetKeyDown (c.ToLower ())) {
					setNextLetter ();
				} else {
					resetWord ();
					colorManager.addFailClicks ();
				}
			}
		} else {
			colorManager.asyncUpdate ();
		}
	}

	public void asyncUpdate ()
	{
		checkInput ();

	}

	public void getResults ()
	{

	}

	private void setCurrentWord ()
	{
		currentWord = usablePasswords [wordIndex].ToCharArray ();
	}

	private void setNextLetter ()
	{
		letterIndex++;
		if (letterIndex >= currentWord.Length) {
			wordIndex++;
			if (wordIndex >= usablePasswords.Length) {//won the game
				finishedWorldList ();
				colorManager.addWordClicks ();
			} else {//won this word
				letterIndex = 0;
				setCurrentWord ();
				colorManager.addWordClicks ();
			}
		} else {
			//won this letter
			colorManager.addLetterClicks ();
		}
	}

	private void finishedWorldList ()
	{
		allWordsGuessed = true;
	}

	public bool wasListCompleted ()
	{
		return allWordsGuessed;
	}

	private char getCurrentLetter ()
	{
		return currentWord [letterIndex];
	}

	private void resetWord ()
	{
		letterIndex = 0;
	}
}
