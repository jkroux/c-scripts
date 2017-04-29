using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
// movement to player solved using unity tutorial
//http://answers.unity3d.com/questions/32618/changing-box-collider-size.html

public class Ai_movement : MonoBehaviour
{   public GameObject player;
	public float chasingSpeed = 0.15f;
	public int pauseTime=9;

	private float visionAngle = 360;
	private float stepCount = 180;
	private bool chase = false;	  
	private int timeOfOutSight = 0;
	private float accel = 0;	
	private Color originalGaurd;
	private LayerMask mask;	
	private float hasPaused=0;

	// Use this for initialization
	void Start()
	{
		SpriteRenderer gaurdchange = (SpriteRenderer)gameObject.GetComponent<Renderer>();
		originalGaurd = gaurdchange.color;
		mask = 1 << 2; 
		mask = ~mask;
	}
		


	//track chasing mode
	void FixedUpdate(){
		foreach (Vector2 sightLine in Sightlines ()) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, sightLine, 2, mask);

			if (hit.collider != null && hit.collider.tag == "Player") {
				playerInSight ();
			} else {
				playerOutOfSight ();
			}
		}
	}
		
	//the angle that the guard can see
	private List<Vector2> Sightlines(){
		float stepAngleSize = visionAngle / stepCount;
		List<Vector2> viewPoint = new List<Vector2> ();
		for (int i=0; i<= stepCount; i++){
			float angle = transform.eulerAngles.y - visionAngle / 2 + stepAngleSize*i;
			Vector2 vAnglev = new Vector2 (Mathf.Sin (angle * Mathf.Deg2Rad) ,(Mathf.Cos (angle * Mathf.Deg2Rad)));
			viewPoint.Add (vAnglev);	
		}
		return viewPoint;
	}
		
	private void playerInSight() {
		chase = true;
		timeOfOutSight = 0;
	}

	private void playerOutOfSight() {
		if (chase == true) { 
			timeOfOutSight++; 
			if (timeOfOutSight >= 900 * 15) {
				transform.Translate (Vector2.zero);
				if (hasPaused < pauseTime * 1500) {
					hasPaused++;
				} else {
					stopChasing ();
				}
			}
		}
	}

	private void stopChasing() {
		SpriteRenderer gaurdchange = (SpriteRenderer)gameObject.GetComponent<Renderer> ();
		gaurdchange.color = originalGaurd;
		chase = false;
		hasPaused = 0;
		accel = 0;
	}



	//See whether the guard catch the player
	public void ChasingMovement(){
		float offsetX = (transform.position.x - player.transform.position.x);
		float offsetY = (transform.position.y - player.transform.position.y);
		float distance = Mathf.Sqrt(Mathf.Pow(offsetX, 2) + Mathf.Pow(offsetY, 2));
		if (distance <= 0.5)
		{
			CatchPlayer();
		}
		else
		{
			ChasePlayer(offsetX, offsetY, distance); 
		}
	}

	private void CatchPlayer(){			
		SpriteRenderer render2 = (SpriteRenderer)player.GetComponent<Renderer>();
		render2.color = new Color(.5f, .2f, 1f, 1f);
		Movement playerMovement = player.GetComponent<Movement>();
		playerMovement.enabled = false;
		StartCoroutine(ChangeToCaught());
	}
		
	private void ChasePlayer(float x, float y, float d) {
		SpriteRenderer gaurdchange = (SpriteRenderer)gameObject.GetComponent<Renderer>();
		gaurdchange.color = new Color (.8F, .3F, .4F);

		Vector2 unitVector = new Vector2 (x/d, y/d);
		if (accel <1) {
			transform.Translate (unitVector * (-chasingSpeed*accel));
			accel = accel + 0.01f; 
		}

		else{
			transform.Translate (unitVector * -chasingSpeed);
		}
	}
		
	IEnumerator ChangeToCaught(){
		yield return new WaitForSeconds (1.0f);
		float fadeTime = GameObject.Find("UIManager").GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene ("Caught");
	}



	public bool GetChase(){
		return chase;
	}

}