using System.Collections;
using System.Collections.Generic; //do we need this? It doesn't look like we use it
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {

    public GameObject secdoor; 
    public float playerSpeed;
    public int numOfArt;

    private int artCollected;
	private bool playSound;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt("previousScene", Application.loadedLevel); //find the index of current scene
		Rigidbody2D playerBody = (Rigidbody2D) gameObject.GetComponent<Rigidbody2D> ();
		playerBody.freezeRotation = true;
       		artCollected = 0;
		playSound=false;

	}
		

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate () {
		PlayerMovement ();
    }


	//Player's movement controlled by user.
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
			other.gameObject.SetActive(false);
			artCollected++;
		}
	
		else if (other.gameObject.CompareTag("Door"))
		{
			if (artCollected == numOfArt) {
			    GameObject[] guards = GameObject.FindGameObjectsWithTag ("Guard");
				foreach(GameObject g in guards){
					Ai_movement guardMovement1 = g.GetComponent<Ai_movement> ();
					guardMovement1.enabled = false;
					GeneralizedMovement guardMovement2 = g.GetComponent<GeneralizedMovement> ();
					guardMovement2.enabled = false;
				}

				SpriteRenderer playerSprite = (SpriteRenderer) gameObject.GetComponent<Renderer> ();
				playerSprite.enabled = false;
				StartCoroutine(changeToTransition());
			}
		}

		else if (other.gameObject.CompareTag("KeyCard"))
		{
			print("card obtained");
			playSound = true;
			PolygonCollider2D offSwitch = (PolygonCollider2D) other.gameObject.GetComponent<Collider2D> ();
			SpriteRenderer spriteOffSwitch = (SpriteRenderer)other.gameObject.GetComponent<Renderer> ();
			Destroy (offSwitch);
			Destroy (spriteOffSwitch);
			secdoor.gameObject.SetActive (false);
		}
	}


	IEnumerator changeToTransition(){
		yield return new WaitForSeconds (1.0f);
		float fadeTime = GameObject.Find("UIManager").GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene ("Transition");
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
