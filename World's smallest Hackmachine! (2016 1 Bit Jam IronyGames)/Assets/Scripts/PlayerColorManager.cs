using UnityEngine;
using System.Collections;

public class PlayerColorManager : MonoBehaviour
{

	public Color activeColor, inactiveColor;
	public float secondsActive, secondsInactive;
	private MeshRenderer thisMesh;
	private int clicksLeft;
	private float lastActiveTime, lastInactiveTime;
	private bool isActive;

	void Start ()
	{
		thisMesh = GetComponent<MeshRenderer> ();
		reset ();
	}

	public void reset ()
	{
		clicksLeft = 0;
		lastInactiveTime = lastActiveTime = 0.0f;
		turnUnclicked ();
	}

	public void asyncUpdate ()
	{
		processUpdate ();
	}

	private void processUpdate ()
	{
		if (clicksLeft == 0) {
			turnUnclicked ();
		} else {
			if (isActive) {
				if (lastActiveTime == 0.0f) {
					lastActiveTime = Time.time;
				} else if (lastActiveTime + secondsActive < Time.time) {
					turnUnclicked ();
					lastActiveTime = 0.0f;
					clicksLeft--;
				}
			} else {
				if (lastInactiveTime == 0.0f) {
					lastInactiveTime = Time.time;
				} else if (lastInactiveTime + secondsInactive < Time.time) {
					turnClicked ();
					lastInactiveTime = 0.0f;
				}
			}
		}
	}

	public void addLetterClicks ()
	{
		addClick ();
	}

	public void addWordClicks ()
	{
		addClick ();
		addClick ();
		addClick ();
		addClick ();
		addClick ();
	}

	public void addFailClicks ()
	{
		addClick ();
		addClick ();
		addClick ();
	}

	public bool isCurrentlyClicking ()
	{
		return clicksLeft != 0;
	}

	private void addClick ()
	{
		if (clicksLeft == 0) {
			turnClicked ();
		}
		clicksLeft++;
	}

	private void turnClicked ()
	{
		thisMesh.material.color = activeColor;
		GetComponent<AudioSource> ().Play ();
		isActive = true;
	}

	private void turnUnclicked ()
	{
		thisMesh.material.color = inactiveColor;
		isActive = false;
	}

	
}
