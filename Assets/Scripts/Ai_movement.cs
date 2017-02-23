using UnityEngine;
using System.Collections;
// movement to player solved using unity tutorial
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

	// Use this for initialization
	void Start()
	{
		movement = new Vector2(1, 0);
		target = GameObject.FindGameObjectWithTag("Player").transform;
		chase = false;
		xDir = 0;
		yDir = 0;
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
	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.CompareTag("Player")){
			chase = true;
			other.gameObject.GetComponent<Movement>();
		}
	}
}