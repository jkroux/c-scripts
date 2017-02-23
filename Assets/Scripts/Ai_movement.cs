using UnityEngine;
using System.Collections;

public class Ai_movement : MonoBehaviour {
	public int time = 0;
	public float speed = 1;
	private Vector2 movement;
	
	// Use this for initialization
	void Start () {
		movement = new Vector2(1, 0);
	}

	// Update is called once per frame
	void Update()
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
	
}
