using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    private GameObject[] guardList;
    public GameObject player; //this variable is public so that it can be set from the Unity editor,
                              //instead of this script searching the game for objects tagged "Player"

	// Use this for initialization
	void Start() {
        guardList = GameObject.FindGameObjectsWithTag("Guard");
	}
	
	public void PauseGame() {
		foreach (GameObject currGuard in guardList)
        {
            (currGuard.GetComponent<Ai_movement>()).enabled = false;
        }
        (player.GetComponent<Movement>()).enabled = false;
	}

    /// <summary>
    /// This method is the same as PauseGame(), but with the booleans reversed. Normally I would
    /// combine them into one method and take the boolean as an input, but in this case that would
    /// mean calling it SetPaused(bool paused) or something, which seems like it would be a lot
    /// less self-explanatory to use in other parts of this code than if we just had two names
    /// for pausing and resuming. We can change this later if we want.
    /// </summary>
    public void ResumeGame() {
        foreach(GameObject currGuard in guardList)
        {
            (currGuard.GetComponent<Ai_movement>()).enabled = true;
        }
        (player.GetComponent<Movement>()).enabled = true;
    }
}
