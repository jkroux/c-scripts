﻿using UnityEngine;
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
		caught = false;
	}

	// Update is called once per frame
	void Update() {
        if (chase == false)
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
					chase = true;
					timer = 0;
				} else if (hit.collider.tag != "Player") {
					timer++;
					if (timer >= 900 * 6) { 
						chase = false;
					}
				}
			}
		}
	}

	void ChasePlayer(float x, float y, float d) {
		Vector2 unitVector = new Vector2 (x/d, y/d);
        transform.Translate(unitVector * -chasingSpeed);
	}

	public bool getCaught(){
		return (caught);
	}

}