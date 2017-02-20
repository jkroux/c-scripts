using UnityEngine;
using System.Collections;

public class Ai_movement : MonoBehaviour {
	public int time = 0;
	public float speed = 1;
	private Vector2 movement;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update()
	{
		time++;
		if (time < 60)
		{
			movement = new Vector2(1, 0);
		}
		else if (time > 60 && time < 120)
		{
			movement = new Vector2(0, 1);
		}
		else if (time > 120 && time < 180)
		{
			movement = new Vector2(-1, 0);
		}
		else if (time > 180 && time < 240)
		{
			movement = new Vector2(0, -1);
		}
		else if (time >= 240){
			time = 0;
		}
		transform.Translate(movement * speed);

	}
}
