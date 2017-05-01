using System.Collections;
using System.Collections.Generic; //do we need this? It doesn't look like we use it
using UnityEngine;


public class Movement : MonoBehaviour {

    public GameObject secdoor; 	
    public float playerSpeed;

	// Use this for initialization
	void Start () {
		Rigidbody2D playerBody = (Rigidbody2D) gameObject.GetComponent<Rigidbody2D> ();
		playerBody.freezeRotation = true;

	}
		


    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate () {
		PlayerMovement ();
    }
		
	void PlayerMovement(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		transform.Translate (movement * playerSpeed);
	}


}
