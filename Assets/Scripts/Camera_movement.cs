using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The code in this script is copied from tutorial "Following the Player with Camera"
//https://unity3d.com/cn/learn/tutorials/projects/2d-ufo-tutorial/following-player-camera?playlist=25844

public class Camera_movement : MonoBehaviour {
	public GameObject player;       

	private Vector3 offsetDis;   

	void Start () 
	{
		offsetDis = transform.position - player.transform.position;
	}


	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{		
		transform.position = player.transform.position + offsetDis;
	}
}
