using UnityEngine;
using System.Collections;
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
	private Transform target; //player's position
	private bool chase;
	private float offsetX;
	private float offsetY;
	private float distance;
	int xDir, yDir; //no use?
	Movement store; //no use?
	float hp;  //no use?
	bool canSee;  
	LayerMask mask;

	// Use this for initialization
	void Start()
	{
		mask = 1 << 2;
		mask = ~mask;
		movement = new Vector2(1, 0);
		target = player.transform;
		chase = false;
		xDir = 0;  //???
		yDir = 0;  //????
		canSee = true; 
	}

	// Update is called once per frame
	void Update()
	{
		if (chase == false)
		{
			time++;
			if ((time == 75) || (time == 150) || (time == 225))
			{
				transform.Rotate(0, 0, 90);
			}
			else if (time >= 300)
			{
				transform.Rotate(0, 0, 90);
				time = 0;
			}
			transform.Translate(movement * speed);  
		}
		else {
			offsetX = Mathf.Abs (transform.position.x - target.position.x);
			offsetY = Mathf.Abs (transform.position.y - target.position.y);
			distance = Mathf.Sqrt (Mathf.Pow (offsetX,2) + Mathf.Pow (offsetY,2));
			if (distance < 0.5) {
				SpriteRenderer render2 = (SpriteRenderer)player.GetComponent<Renderer> ();
				render2.color = new Color (.5f, .2f, 1f, 1f);
				//player.SetActive (false);
				print ("you have been caught");
			} 
			else {
				ChasePlayer ();
			}
		}
	}
	void FixedUpdate(){
		RaycastHit2D hit = Physics2D.Raycast (transform.position, movement, Mathf.Infinity, mask); //???direction of ray: Vector2.up?  I change this to "movement"
		if (hit.collider.tag=="Wall") {
			canSee = false;
			//chase = true;
			print (canSee);
		} 
		else {
			canSee = true;
		}
	}

	void ChasePlayer(){
		Vector2 unitVector = new Vector2 (offsetX / distance, offsetY / distance);
		transform.Translate (unitVector * chasingSpeed);
	}
		
	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.CompareTag("Player")&& canSee==true){
			chase = true;
		}
	}

	void OnTriggerEnter2d(Collider2D other){   // ????what does this entire thing do?????????
		if (other.gameObject.CompareTag ("Walls")) { 
			Vector2 wall = other.gameObject.transform.position; //wall's position
			float xWall = wall.x;
			float yWall = wall.y;
			BoxCollider2D collider = GetComponent<BoxCollider2D> ();
			Vector2 boxBox=collider.size;
		}
	}
}