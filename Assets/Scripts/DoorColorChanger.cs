using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorColorChanger : MonoBehaviour {

	public GameObject player;
	public Color unlockedColor;

	private PlayerPickups playerScript;

	// Use this for initialization
	void Start () {
		playerScript = player.GetComponent<PlayerPickups>();
	}
	

	void FixedUpdate () {
		// It may not be efficient to check whether the player has collected all the art multiple
		// times every second. If anyone knows a way to make the door immediately respond when the
		// player has collected all the art more efficiently than this, while still having the code
		//about the door belong to the door object, please go ahead and change this method.
		int artCollected = playerScript.getArtCollected();
		int numOfArt = playerScript.getNumOfArt();
		if (artCollected == numOfArt)
		{
			SpriteRenderer rend = (SpriteRenderer) GetComponent<Renderer>();
			rend.color = unlockedColor;
		}
	}
}
