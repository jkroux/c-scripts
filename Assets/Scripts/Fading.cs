using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Most of the code in this script are copied from the tutorial "How to Fade Between Scenes in Unity"
// https://www.youtube.com/watch?v=0HwZQt94uHQ

public class Fading : MonoBehaviour {
	public Texture2D black;
	public float fadeSpeed = 1.0f;

	private int drawDepth = -1000; //black renders on the top in the hierarchy
	private float alpha = 1.0f;  //black starts by being visible
	private int fadeDir = -1; // -1: the scene fade in


	void OnGUI(){ //change alpha value
		alpha += fadeDir*fadeSpeed*Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);

		//set black's color (change alpha)
		GUI.color= new Color (GUI.color.r, GUI.color.b, GUI.color.g, alpha);
		GUI.depth = drawDepth; //black on top
		GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height),black);
	}

	public float BeginFade (int direction){
		fadeDir = direction;
		return (fadeSpeed); //avoid loading the next scene before the current scene fade out
	}

	void OnLevelFinishedLoading(){
		alpha = 1;
		BeginFade (-1);
	}
 
}
