using System.Collections;
using System.Collections.Generic; //do we need this? It doesn't look like we use it
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {

    public GameObject secdoor; 
    public float playerSpeed;
    public int numOfArt;

    private int artCollected = 0;
	private bool playSound;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt("previousScene", Application.loadedLevel); //find the index of current scene
		Rigidbody2D playerBody = (Rigidbody2D) gameObject.GetComponent<Rigidbody2D> ();
		playerBody.freezeRotation = true;
		playSound=false;

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



	//OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Pickup"))
		{
			CollidePickUp(other);
		}
	
		else if (other.gameObject.CompareTag("Door"))
		{
			if (artCollected == numOfArt) {
				CollideDoor ();
			}
		}

		else if (other.gameObject.CompareTag("KeyCard"))
		{
			CollideKeyCard (other);

		}
	}
		
	void CollidePickUp(Collider2D pickUp){
		pickUp.gameObject.SetActive(false);
		artCollected++;
	}

	void CollideDoor(){
		GuardStop ();
		PlayerDisappear ();
		StartCoroutine(changeToTransition());
	}

	void GuardStop(){
		GameObject[] guards = GameObject.FindGameObjectsWithTag ("Guard");
		foreach(GameObject g in guards){
			Ai_movement guardMovement1 = g.GetComponent<Ai_movement> ();
			guardMovement1.enabled = false;
			GeneralizedMovement guardMovement2 = g.GetComponent<GeneralizedMovement> ();
			guardMovement2.enabled = false;
		}
	}

	void PlayerDisappear(){
		Movement playerMovement = gameObject.GetComponent<Movement> ();
		playerMovement.enabled = false;
		SpriteRenderer playerSprite = (SpriteRenderer) gameObject.GetComponent<Renderer> ();
		playerSprite.enabled = false;
	}

	IEnumerator changeToTransition(){
		yield return new WaitForSeconds (1.0f);
		float fadeTime = GameObject.Find("UIManager").GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene ("Transition");
	}

	void CollideKeyCard(Collider2D keyCard){
		print("card obtained");
		playSound = true;
		PolygonCollider2D offSwitch = (PolygonCollider2D) keyCard.gameObject.GetComponent<Collider2D> ();
		SpriteRenderer spriteOffSwitch = (SpriteRenderer)keyCard.gameObject.GetComponent<Renderer> ();
		Destroy (offSwitch);
		Destroy (spriteOffSwitch);
		secdoor.gameObject.SetActive (false);
	}



	public bool getPlaySound(){
		return playSound;
	}
		
	public void setPlaySound(bool value){
		playSound = value;
	}
		
	public int getArtCollected(){
		return artCollected;
	}

	public int getNumOfArt()
	{
		return numOfArt;
	}
}
