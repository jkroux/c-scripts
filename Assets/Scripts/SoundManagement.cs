using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// used online sound managment unity tutorial https://unity3d.com/learn/tutorials/topics/audio/sound-effects-scripting
public class SoundManagement : MonoBehaviour {
	private AudioSource source;
	public AudioClip clip;
	public GameObject player;
	private Movement cardValueScript;
	private int artCollected; 
	private float volume;
	// Use this for initialization
	void Start () {
		artCollected = 0;
		source  = GetComponent<AudioSource>();
		volume = 1.0f;
		cardValueScript= (Movement)player.GetComponent<Movement>();
			
	}
	
	// Update is called once per frame
	void Update () {
		if (cardValueScript.getPlaySound()){
			source.PlayOneShot(clip,volume);
			cardValueScript.setPlaySound (false);
		}
		if (artCollected<cardValueScript.getArtCollected()){
			source.PlayOneShot (clip, volume);
			artCollected++;
		}
	}
}