using UnityEngine;
using System.Collections;
// movement to player solved using unity tutorial
//http://answers.unity3d.com/questions/32618/changing-box-collider-size.html
//box collider resizing.
public class Ai_movement : MonoBehaviour
{
	public int time = 0;
	public float speed = 1;
	private Vector2 movement;
	private Transform target;
	private bool chase;
	int xDir, yDir;
	Movement store;
	float hp;
	bool canSee;
	LayerMask mask;


	 

	// Use this for initialization
	void Start()
	{
		mask = 1 << 2;
		mask = ~mask;
		movement = new Vector2(1, 0);
		target = GameObject.FindGameObjectWithTag("Player").transform;
		chase = false;
		xDir = 0;
		yDir = 0;
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
			Movement.playerHp--;
		}
	}
	void FixedUpdate(){
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.up,100f, mask);
		if (hit.collider.tag=="Walls") {
			canSee = false;
			print (canSee);
		} 
		else {
			canSee = true;
		}
	
	}
	void OnTriggerStay2D(Collider2D other){

		if (other.gameObject.CompareTag("Player")&& canSee==true){
			chase = true;
			other.gameObject.GetComponent<Movement>();
		}
	}
	void OnTriggerEnter2d(Collider2D other){
		if (other.gameObject.CompareTag ("Walls")) {
			Vector2 wall = other.gameObject.transform.position;
			float xWall = wall.x;
			float yWall = wall.y;
			BoxCollider2D collider = GetComponent<BoxCollider2D> ();
			Vector2 boxBox=collider.size;
		}
	}
}