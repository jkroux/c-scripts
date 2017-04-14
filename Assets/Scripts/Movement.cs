using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {
    public float speed;
    public int artInRoom;
    private int artCollected;
	static public bool cardObtained;
	public GameObject door;
	public GameObject secdoor;
	public GameObject Guard;


	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt("previousScene", Application.loadedLevel);
		Rigidbody2D playerBody = (Rigidbody2D) gameObject.GetComponent<Rigidbody2D> ();
		playerBody.freezeRotation = true;
        artCollected = 0;
		cardObtained=false;
		print ("press E to pick up objects");
	}
		
    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate () {
		Ai_movement guardMovement = Guard.GetComponent<Ai_movement>();
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
		if (other.gameObject.CompareTag("Pickup"))
		{
			other.gameObject.SetActive(false);
			artCollected++;
			if (artCollected == artInRoom) {
				SpriteRenderer renderer = (SpriteRenderer)door.GetComponent<Renderer>();
				renderer.color = new Color32(25, 0, 5, 255);
			}
		}
		if (other.gameObject.CompareTag("Door"))
		{
			if (artCollected == artInRoom)
			{
				gameObject.SetActive(false);
//				StartCoroutine(wait());
				Invoke("transitionMenu",1);

			}
		}
		if (other.gameObject.CompareTag("KeyCard"))
		{
			cardObtained = true;
			print("card obtained");
			other.gameObject.SetActive(false);
			// code taken in part from unity 3d https://forum.unity3d.com/threads/how-do-you-change-a-color-in-spriterenderer.211003/
			SpriteRenderer renderer = (SpriteRenderer)secdoor.GetComponent<Renderer>();
			// we should probably change this so that the door the keycard opens is a variable
			// assigned to the keycard, not to the player
			renderer.color = new Color32(17, 161, 54, 255);
			BoxCollider2D comp = secdoor.GetComponent("BoxCollider2D") as BoxCollider2D;
			comp.enabled = false;
		}
			
	}

	void transitionMenu(){
		SceneManager.LoadScene("Transition");
	}


//	IEnumerator wait() {
//		yield return new WaitForSeconds(2f);
//		SceneManager.LoadScene("Transition");
//	}
}
