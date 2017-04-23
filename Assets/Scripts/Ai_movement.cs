using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
// movement to player solved using unity tutorial
//http://answers.unity3d.com/questions/32618/changing-box-collider-size.html
//box collider resizing.
public class Ai_movement : MonoBehaviour
{   public GameObject player;
	public float regularSpeed = 0.05f;
	public float chasingSpeed = 0.05f;
	public float visionAngle;
	public float stepCount;
	public int pauseTime=9;
	public float chase;	  //when chase=1, guard does not chase player; =2, chasing player; =3, guard lose the player and pause

	private int timer; //time that player is out of vision cone
	private float accel = 0;	
	private Color originalGaurd;
	private float toggle =0; //????? what does toggle do?
	private LayerMask mask;	
	private float hasPaused=0;

	// Use this for initialization
	void Start()
	{
		SpriteRenderer gaurdchange = (SpriteRenderer)gameObject.GetComponent<Renderer>();
		originalGaurd = gaurdchange.color;
		mask = 1 << 2; 
		mask = ~mask;
		chase = 1;
		timer=0;
	}

	// Update is called once per frame
	void Update() {
		if (chase != 1 && chase !=3){fullChaseMethodForEZUse ();}
	}

	//???? distance between guard and player
	public void fullChaseMethodForEZUse(){
		float offsetX = (transform.position.x - player.transform.position.x);
		float offsetY = (transform.position.y - player.transform.position.y);
		float distance = Mathf.Sqrt(Mathf.Pow(offsetX, 2) + Mathf.Pow(offsetY, 2));

		if (distance < 0.5)
		{
			SpriteRenderer render2 = (SpriteRenderer)player.GetComponent<Renderer>();
			render2.color = new Color(.5f, .2f, 1f, 1f);
			Movement playerMovement = player.GetComponent<Movement>();
			playerMovement.enabled = false;
			StartCoroutine(ChangeToCaught());
		}
		else
		{ //??toggle?
			toggle++;
			if (toggle >= 5){
				SpriteRenderer gaurdchange = (SpriteRenderer)gameObject.GetComponent<Renderer>();
				gaurdchange.color = new Color (.8F, .3F, .4F);
				ChasePlayer(offsetX, offsetY, distance); 
			}
		}
	}
		
	//the angle that the guard can see
	 public List<Vector2> Angle(){
		float stepAngleSize = visionAngle / stepCount;
		List<Vector2> viewPoint = new List<Vector2> ();
		for (int i=0; i<= stepCount; i++){
			float angle = transform.eulerAngles.y - visionAngle / 2 + stepAngleSize*i;
			Vector2 vAnglev = new Vector2 (Mathf.Sin (angle * Mathf.Deg2Rad) ,(Mathf.Cos (angle * Mathf.Deg2Rad)));
			viewPoint.Add (vAnglev);	
		}
		return viewPoint;
	}

	//change mode changes
	void FixedUpdate(){
		List<Vector2> dirVector = Angle ();
		for (int i = 0; i < dirVector.Count; i++) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, dirVector [i], 2, mask);
			if (hit.collider != null) {
				if (hit.collider.tag == "Player") {
					chase = 2;
					timer = 0;
				} 
			}
			if (hit.collider==null || hit.collider.tag != "Player") {
					timer++; 
				if (timer >= 900 * 15 && chase!=1) { //chase =2
					chase = 3;
					transform.Translate (Vector2.zero);
					if (hasPaused < pauseTime*1500) {
						hasPaused++;
					} 
					else {
						SpriteRenderer gaurdchange = (SpriteRenderer)gameObject.GetComponent<Renderer>();
						chase = 1;
						hasPaused = 0;
						gaurdchange.color = originalGaurd;
						accel = 0;
			}
				}
			}
		}
	}

	//guard's movement when it starts to change the player
	void ChasePlayer(float x, float y, float d) {
		Vector2 unitVector = new Vector2 (x/d, y/d);
		if (accel <1) {
			print ("I am accelerating");
			transform.Translate (unitVector * (-chasingSpeed*accel));
			accel = accel + 0.01f; 
		}

		else{
			print("hello I am no longer accelerating");
			transform.Translate (unitVector * -chasingSpeed);
		}
	}

	//load Caught scene
	IEnumerator ChangeToCaught(){
		yield return new WaitForSeconds (1.0f);
		float fadeTime = GameObject.Find("UIManager").GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene ("Caught");
	}

}