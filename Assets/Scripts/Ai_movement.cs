using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// movement to player solved using unity tutorial
//http://answers.unity3d.com/questions/32618/changing-box-collider-size.html
//box collider resizing.
public class Ai_movement : MonoBehaviour
{
	public int time = 0;
	public float speed = 1;
	public float chasingSpeed = 1;
	public GameObject player;

	private Vector2 movement;
	private bool chase;	
	private bool canSee;
	private float offsetX;
	private float offsetY;
	private float distance;
	private bool caught;
	LayerMask mask;
	public float visionAngle;
	public float stepCount;
	private int timer;

	// Use this for initialization
	void Start()
	{
		mask = 1 << 2;
		mask = ~mask;
		movement = new Vector2(1, 0);
		chase = false;
		canSee = true;
		timer=0;
		List<Vector2> angleMeasure =Angle ();
	
	}

	// Update is called once per frame
	void Update(){
		if (chase == false){
			time++;
			if ((time == 75) || (time == 150) || (time == 225)){
				transform.Rotate(0, 0, 90);
			}
			else if (time >= 300){
				transform.Rotate(0, 0, 90);
				time = 0;
			}
			transform.Translate(movement * speed);
		}
		else {
			offsetX = Mathf.Abs (transform.position.x - player.transform.position.x);
			offsetY = Mathf.Abs (transform.position.y - player.transform.position.y);
			distance = Mathf.Sqrt (Mathf.Pow (offsetX,2) + Mathf.Pow (offsetY,2));
			if (distance < 0.5) {
				SpriteRenderer render2 = (SpriteRenderer)player.GetComponent<Renderer> ();
				render2.color = new Color (.5f, .2f, 1f, 1f);
				caught = true;
				print ("you have been caught");
			}
			else {
		//		ChasePlayer ();    //There is a bug in the ChasePlayer method. I will uncomment this when I fix this bug
		}
	  }
	}

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

	//???
	public void visionCircle(){
		List<Vector2> dirVector = Angle ();
		for (int i=0; i<=dirVector.Count;i++){
			RaycastHit2D hit = Physics2D.Raycast (transform.position, dirVector[i],1, mask);
			if (hit.collider.tag=="Player") {  
				canSee = true;
				//chase = true;
				print (canSee);
			} 
			else {
				canSee = false;
	
		}
	  }
	}

	void FixedUpdate(){
		List<Vector2> dirVector = Angle ();
		for (int i = 0; i < dirVector.Count; i++) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, dirVector [i], 2, mask);
			if (hit.collider != null) {
				if (hit.collider.tag == "Player") {
					chase = true;
					timer = 0;
				} else if (hit.collider.tag != "Player") {
					timer++;
					if (timer >= 900 * 6) {  //????
						chase = false;
					}
				}
			}
		}
	}

	//do we still need this?
	void OnTriggerStay2D(Collider2D other){

		if (other.gameObject.CompareTag("Player")&& canSee==true){
			chase = true;
			other.gameObject.GetComponent<Movement>();
		}
	}

//	void OnTriggerEnter2d(Collider2D other){
//		if (other.gameObject.CompareTag ("Walls")) {
//			Vector2 wall = other.gameObject.transform.position;
//			float xWall = wall.x;
//			float yWall = wall.y;
//			BoxCollider2D collider = GetComponent<BoxCollider2D> ();
//			//Vector2 boxBox=collider.size;
//		}
//	}

	void ChasePlayer(){
		Vector2 unitVector = new Vector2 (offsetX / distance, offsetY / distance);
		transform.Translate (unitVector * chasingSpeed);
	}

	public bool getCaught(){
		return (caught);
	}

}