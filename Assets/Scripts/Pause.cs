using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    private GameObject[] guardList;

	// Use this for initialization
	void Start () {

	}
	
	// Pauses the game when the player picks up an object, so the player has time to look at the
    // popup window describing the object.
	void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                guardList = GameObject.FindGameObjectsWithTag("Guard");
                foreach (GameObject currentGuard in guardList) {
                    (currentGuard.GetComponent("Ai_movement") as MonoBehaviour).enabled = false;
                }

                (GameObject.FindWithTag("Player").GetComponent("Movement") as MonoBehaviour).enabled = false;
            }
        }
    }
}
