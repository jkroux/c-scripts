using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// establishes SightCone and if player is caught for gaurd.
public class Sight : MonoBehaviour {
	
	private float visionAngle = 360;
	private float stepCount = 180;
	private bool chase = false;
	private LayerMask mask;	
	private int timeOfOutSight = 0;
//	public float chasingSpeed = 0.15f;
	public int pauseTime=9;
	private float hasPaused=0;

	// Use this for initialization
	void Start () {
		mask = 1 << 2; 
		mask = ~mask;
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Vector2 sightLine in Sightlines ()) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, sightLine, 2, mask);

			if (hit.collider != null && hit.collider.tag == "Player") {
				playerInSight ();
			} else {
				playerOutOfSight ();
			}
		}
	}
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
					chase=false;
				}
			}
		}
	}
		//private void stopChasing() {
		//SpriteRenderer gaurdchange = (SpriteRenderer)gameObject.GetComponent<Renderer> ();
		//gaurdchange.color = originalGaurd;
		//chase = false;
		//hasPaused = 0;
		//accel = 0;
	//}
	public bool GetChase(){
		return chase;
	}

}
