using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GeneralizedMovement : MonoBehaviour {
	
	public float speed = 1;
	public Vector2 movement;
	public Transform Pos1;
	public Transform Pos2;
	public Transform Pos3;
	public Transform Pos4;
	public Transform Pos5;
	public Transform Pos6;
	public Transform Pos7;
	private Transform work;
	private ArrayList list = new ArrayList();
	private int indexforMovement = 0;
	public int numOfPatrolPoints;

	// Use this for initialization
	void Start () {
		list.Add (Pos1);
		list.Add (Pos2);
		list.Add (Pos3);
		list.Add (Pos4);
		list.Add (Pos5);
		list.Add (Pos6);
		list.Add (Pos7);
	}
	
	// Update is called once per frame
	void Update () {
		work = (Transform) list [indexforMovement];
		Ai_movement script = (Ai_movement) gameObject.GetComponent<Ai_movement> ();
		float offsetX = (transform.position.x - work.position.x);
		float offsetY = (transform.position.y - work.position.y);
		float distance = Mathf.Sqrt (Mathf.Pow (offsetX, 2) + Mathf.Pow (offsetY, 2));

		if (!script.chase){
		ModableMovement (offsetX,offsetY,distance);
		}
	}
	bool RangeCheck(float x, float compareX){
		if ((compareX - .5) < x && x < (compareX + .5)) {
			return true;
		} else {
			return false;
		}
	}
	void ModableMovement(float x, float y, float d){
		{
			Vector2 unitVector = new Vector2 (x / d, y / d);
			transform.Translate(unitVector * -speed);
			if (RangeCheck(transform.position.x,work.position.x) && RangeCheck(transform.position.y,work.position.y)  &&  indexforMovement < (numOfPatrolPoints-1)) {
				print ("here");
				indexforMovement++;
			} 
			else if (RangeCheck(transform.position.x,work.position.x) && RangeCheck(transform.position.y,work.position.y) && indexforMovement == (numOfPatrolPoints-1)){
				indexforMovement = 0;
	}
		}
	
	}
}
