using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Ai_Chase : MonoBehaviour {
	// movement to player solved using unity tutorial
	// http://answers.unity3d.com/questions/32618/changing-box-collider-size.html

	public GameObject player;
	private float chasingSpeed = 0.075f;
	private float accel = 0;
	private Color originalColor;
	private Sight vision;
	private SpriteRenderer guardRenderer;


	// Use this for initialization
	void Start()
	{
		vision = gameObject.GetComponent<Sight>();
		guardRenderer = (SpriteRenderer)gameObject.GetComponent<Renderer>();
		originalColor = guardRenderer.color;
	}

	//track chasing mode
	void FixedUpdate(){
		if (vision.GetChase()) {
			//Double call of the ChasingMovement produces the most enjoyable chasing feeling
			ChasingMovement();
			ChasingMovement();
		}
		else{
			stopChasing();
		}
	}


	private void stopChasing() {	
		guardRenderer.color = originalColor;
		accel = 0;
	}


	//See whether the guard catch the player
	public void ChasingMovement(){
		float offsetX = (transform.position.x - player.transform.position.x);
		float offsetY = (transform.position.y - player.transform.position.y);
		float distance = Mathf.Sqrt(Mathf.Pow(offsetX, 2) + Mathf.Pow(offsetY, 2));

		if (distance <= 0.56)
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
			transform.Translate(unitVector * (-chasingSpeed*accel));
			accel = accel + 0.005f; 
		}

		else{
			transform.Translate(unitVector * -chasingSpeed);
		}
	}
		
	IEnumerator ChangeToCaught(){
		yield return new WaitForSeconds(1.0f);
		float fadeTime = GameObject.Find("UIManager").GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene("Caught");
	}

}