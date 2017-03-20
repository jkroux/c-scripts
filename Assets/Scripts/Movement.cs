using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public float speed;
    public int artInRoom;
    private int artCollected;
	static public bool cardObtained;
	public GameObject door;
	public GameObject guard;
    public GameObject pauseHandler;

	// Use this for initialization
	void Start () {
        artCollected = 0;
		cardObtained=false;
		print ("press E to pick up objects");
	}

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate () { 
		Ai_movement guardMovement = guard.GetComponent<Ai_movement>();
		bool caught = guardMovement.getCaught();
		if (!caught) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
			transform.Translate (movement * speed);
		}
    }

	//OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
	void OnTriggerStay2D(Collider2D other)
	{
		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		if (other.gameObject.CompareTag("Pickup") && Input.GetKeyDown(KeyCode.E))
		{
			other.gameObject.SetActive(false);
			artCollected++;

            // Pauses the game when the player picks up a pickup. This will be useful later, when
            // popups are implemented, but if it's happening right now and it's inconvenient,
            // just comment these two lines out.
            Pause pauseScript = pauseHandler.GetComponent<Pause>();
            pauseScript.PauseGame();
		}

		if (other.gameObject.CompareTag("Door"))
		{
			if (artCollected == artInRoom)
			{
				gameObject.SetActive(false);
			}
		}
		if (other.gameObject.CompareTag("KeyCard") && Input.GetKeyDown(KeyCode.E))
		{
			cardObtained = true;
			print("card obtained");
			other.gameObject.SetActive(false);
			// code taken in part from unity 3d https://forum.unity3d.com/threads/how-do-you-change-a-color-in-spriterenderer.211003/
			SpriteRenderer renderer = (SpriteRenderer)door.GetComponent<Renderer>();
			renderer.color = new Color32(17, 161, 54, 255);
			BoxCollider2D comp = door.GetComponent("BoxCollider2D") as BoxCollider2D;
			comp.enabled = false;
		}
	}

//	void OnTriggerEnter2D(Collider2D other){
//		print("register");
//		if (other.gameObject.CompareTag("Triggered"))
//		{
//			SpriteRenderer render = (SpriteRenderer)TripWire.gameObject.GetComponent<Renderer>();
//			render.color = new Color(0f, 0f, 0f, 1f);
//			Ai_movement comp = guard.GetComponent("Ai_movement") as Ai_movement;
//			comp.speed = 3f;
//			print("got em");
//		}
			
//   }
}
