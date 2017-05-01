using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// used online sound managment unity tutorial https://unity3d.com/learn/tutorials/topics/audio/sound-effects-scripting
public class SoundManagement : MonoBehaviour {
	
	public AudioClip clip;
	public GameObject player;
	public AudioClip clip2;
	public AudioClip clip3;
	private AudioSource source;
	private PlayerPickups cardValueScript;
	private int artCollected; 
	private float volume;


	// Use this for initialization
	void Start () {
		artCollected = 0;
		source  = GetComponent<AudioSource>();
		volume = 1.0f;
		cardValueScript= player.GetComponent<PlayerPickups>();
			
	}
	
	// Update is called once per frame
	void Update () {
		if (cardValueScript.getPlaySound ()) {
			source.PlayOneShot (clip, volume);
			cardValueScript.setPlaySound (false);
		}
		if (artCollected < cardValueScript.getArtCollected ()) {
			source.PlayOneShot (clip2, volume);	
			artCollected++;
		}
		if (artCollected == cardValueScript.getNumOfArt()) {
			source.PlayOneShot (clip3, volume);
			artCollected++;
		}
	}
}