using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerPickups : MonoBehaviour {
	public int numOfArt;
	private Movement playermovement;
	private int artCollected = 0;
	private bool playSound;
	private GameObject secdoor;
	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt("previousScene", SceneManager.GetActiveScene().buildIndex);
		playSound = false;
		playermovement = gameObject.GetComponent < Movement > ();
		secdoor = playermovement.secdoor; 
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Pickup"))
		{
			CollidePickUp(other);
		}

		else if (other.gameObject.CompareTag("Door"))
		{
			if (artCollected == numOfArt)
			{
				CollideDoor();
			}
		}

		else if (other.gameObject.CompareTag("KeyCard"))
		{
			CollideKeyCard(other);

		}
	}
		void CollidePickUp(Collider2D pickUp)
	{
		pickUp.gameObject.SetActive(false);
		artCollected++;
	}
		void GuardStop()
	{
		GameObject[] guards = GameObject.FindGameObjectsWithTag("Guard");
		foreach (GameObject g in guards)
		{
			Ai_Chase guardMovement1 = g.GetComponent<Ai_Chase>();
			guardMovement1.enabled = false;
			GeneralizedMovement guardMovement2 = g.GetComponent<GeneralizedMovement>();
			guardMovement2.enabled = false;
		}
	}

	void CollideDoor()
	{
		GuardStop();
		PlayerDisappear();
		StartCoroutine(changeToTransition());
	}

	void CollideKeyCard(Collider2D keyCard)
	{
		print("card obtained");
		playSound = true;
		PolygonCollider2D offSwitch = (PolygonCollider2D)keyCard.gameObject.GetComponent<Collider2D>();
		SpriteRenderer spriteOffSwitch = (SpriteRenderer)keyCard.gameObject.GetComponent<Renderer>();
		Destroy(offSwitch);
		Destroy(spriteOffSwitch);
		secdoor.gameObject.SetActive(false);
	}
		void PlayerDisappear(){
		Movement playerMovement = gameObject.GetComponent<Movement>();
		playerMovement.enabled = false;
		SpriteRenderer playerSprite = (SpriteRenderer)gameObject.GetComponent<Renderer>();
		playerSprite.enabled = false;
	}

	IEnumerator changeToTransition(){
		yield return new WaitForSeconds(1.0f);
		float fadeTime = GameObject.Find("UIManager").GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		SceneManager.LoadScene("Transition");
	}
		public bool getPlaySound()
	{
		return playSound;
	}
	public void setPlaySound(bool value){
		playSound = value;
	}
		
	public int getArtCollected()
	{
		return artCollected;
	}

	public int getNumOfArt()
	{
		return numOfArt;
	}
}
