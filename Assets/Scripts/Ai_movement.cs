using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
// movement to player solved using unity tutorial
//http://answers.unity3d.com/questions/32618/changing-box-collider-size.html
//box collider resizing.
public class Ai_movement : MonoBehaviour
{
	public int time = 0;
	private float toggle =0;
	public float speed = 1;
	public float chasingSpeed = 1;
	public GameObject player;
	public float accel = 1;
	private Vector2 movement;
	public float chase;	
	private bool canSee;
	private bool caught;
	LayerMask mask;
	public float visionAngle;
	public float stepCount;
	private int timer;
	public float counterofAccel=1;
	private float accelHold;
	private float timer2=0;
	private Color originalGaurd;
	public int waitForIt=9;

	// Use this for initialization
	void Start()
	{
		SpriteRenderer gaurdchange = (SpriteRenderer)gameObject.GetComponent<Renderer>();
		originalGaurd = gaurdchange.color;
		accelHold = accel;
		mask = 1 << 2;
		mask = ~mask;
		movement = new Vector2(1, 0);
		chase = 1;
		canSee = true;
		timer=0;
		List<Vector2> angleMeasure =Angle ();
		caught = false;
	}

	public void fullChaseMethodForEZUse(){
		float offsetX = (transform.position.x - player.transform.position.x);
		float offsetY = (transform.position.y - player.transform.position.y);
		float distance = Mathf.Sqrt(Mathf.Pow(offsetX, 2) + Mathf.Pow(offsetY, 2));

		if (distance < 0.5)
		{
			SpriteRenderer render2 = (SpriteRenderer)player.GetComponent<Renderer>();
			render2.color = new Color(.5f, .2f, 1f, 1f);
			caught = true;
			print("you have been caught");
		}
		else
		{
			toggle++;
			if (toggle >= 5){
				SpriteRenderer gaurdchange = (SpriteRenderer)gameObject.GetComponent<Renderer>();
				gaurdchange.color = new Color (.8F, .3F, .4F);
				ChasePlayer(offsetX, offsetY, distance);
		}
	}
	}

	// Update is called once per frame
	void Update() {
		if (chase == 1 || chase==3)
        {
			
			//DefaultMovement ();
        }
        else
        {
            float offsetX = (transform.position.x - player.transform.position.x);
            float offsetY = (transform.position.y - player.transform.position.y);
            float distance = Mathf.Sqrt(Mathf.Pow(offsetX, 2) + Mathf.Pow(offsetY, 2));

            if (distance < 0.5)
            {
                SpriteRenderer render2 = (SpriteRenderer)player.GetComponent<Renderer>();
                render2.color = new Color(.5f, .2f, 1f, 1f);
                caught = true;
                print("you have been caught");
				SceneManager.LoadScene("Caught");
            }
            else
            {
                ChasePlayer(offsetX, offsetY, distance);
            }
        }
	}

    void DefaultMovement()
    {
        time++;
        if (time == 0)
        {
            movement = new Vector2(1, 0);
        }
        else if (time == 120)
        {
            movement = new Vector2(0, 1);
        }
        else if (time == 240)
        {
            movement = new Vector2(-1, 0);
        }
        else if (time == 360)
        {
            movement = new Vector2(0, -1);
        }
        else if (time >= 480)
        {
            movement = new Vector2(1, 0);
            time = 0;
        }
        transform.Translate(movement * speed);
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
				if (timer >= 900 * 15 && chase!=1) {
					chase = 3;
					transform.Translate (Vector2.zero);
					if (timer2 < waitForIt*1500) {
						timer2++;
					} 
					else {
						SpriteRenderer gaurdchange = (SpriteRenderer)gameObject.GetComponent<Renderer>();
						chase = 1;
						timer2 = 0;
						gaurdchange.color = originalGaurd;
						accel = accelHold;
			}
				}
			}
		}
	}


	void ChasePlayer(float x, float y, float d) {
		Vector2 unitVector = new Vector2 (x/d, y/d);
		if (accel <1) {
			transform.Translate (unitVector * (-chasingSpeed*accel));
			accel = accel + (counterofAccel/80);
		}

		else{
			print("hello I am no longer accelerating");
			transform.Translate (unitVector * -chasingSpeed);
		}
	}

	public bool getCaught(){
		return (caught);
	}

}