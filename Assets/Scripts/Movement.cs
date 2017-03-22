using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	public static float playerHp;
    public float speed;
    public int artInRoom;
    private int artCollected;
	static public bool cardObtained;
	public GameObject door;
	public GameObject TripWire;
	public GameObject Gaurd;


	// Use this for initialization
	void Start () {
		playerHp=1;
		Rigidbody2D playerBody = (Rigidbody2D) gameObject.GetComponent<Rigidbody2D> ();
		playerBody.freezeRotation = true;
        artCollected = 0;
		cardObtained=false;
		print ("press E to pick up objects");
	}

	public bool cardObtainedget(){
		return(cardObtained);
	}

	void Update(){
		if (playerHp==0){
			SpriteRenderer render2 = (SpriteRenderer) gameObject.GetComponent<Renderer>();
			render2.color = new Color(.5f, .2f, 1f, 1f);
			gameObject.SetActive(false);
			print("you have been caught");
		}
	}

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate () {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Actually move the player's icon
        transform.Translate(movement * speed);
    }

	//OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
	void OnTriggerStay2D(Collider2D other)
	{
		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		if (other.gameObject.CompareTag("Pickup") && Input.GetKeyDown(KeyCode.E))
		{
			other.gameObject.SetActive(false);
			artCollected++;
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
	void OnTriggerEnter2D(Collider2D other){
//		print("register");
//		if (other.gameObject.CompareTag("Triggered"))
//		{
//			SpriteRenderer render = (SpriteRenderer)TripWire.gameObject.GetComponent<Renderer>();
//			render.color = new Color(0f, 0f, 0f, 1f);
//			Ai_movement comp = Gaurd.GetComponent("Ai_movement") as Ai_movement;
//			comp.speed = 3f;
//			print("got em");
//		}
			
    }
}
