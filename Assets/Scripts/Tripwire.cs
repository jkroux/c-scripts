using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tripwire : MonoBehaviour {
	public bool Triped=false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Triped==true){
		}
		
	}
	void OnTriggerEnter2D(Collider2D other){
	}
}
