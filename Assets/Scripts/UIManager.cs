using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
	private GameObject[] pauseObjects;
	private int previousScene;
	private int nextScene;
	private int transitionIndex = 5;
	private int caughtIndex = 4;
	private int highestLevel = 12;

	void Start () {
		int indexOfScene = SceneManager.GetActiveScene ().buildIndex;

		if (indexOfScene == transitionIndex) {
			DisplayMeme ();
			previousScene = PlayerPrefs.GetInt ("previousScene");
			nextScene = previousScene + 1;
		} else if (indexOfScene == caughtIndex) {
			previousScene = PlayerPrefs.GetInt ("previousScene");
			nextScene = previousScene + 1;
		}else {
			Time.timeScale = 1;
			pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
			hidePaused();
		}
	}

	void DisplayMeme(){
		GameObject meme = GameObject.Find("Meme");
		Image image = meme.GetComponent<Image> ();
		image.sprite = UpdateMemes.mostRecentSprite;
	}

	//https://www.sitepoint.com/adding-pause-main-menu-and-game-over-screens-in-unity/
	void Update () {
		if(Input.GetKeyDown(KeyCode.P))
		{
			pauseControl ();
		}
	}
		

	//Restart Button's method
	public void Reload(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}


	//Play Button's method
	//https://www.sitepoint.com/adding-pause-main-menu-and-game-over-screens-in-unity/
	public void pauseControl(){
		if(Time.timeScale == 1)
		{
			Time.timeScale = 0;
			showPaused();
		} else if (Time.timeScale == 0){
			Time.timeScale = 1;
			hidePaused();
		}
	}


	//show all ShowOnPause objects
	//https://www.sitepoint.com/adding-pause-main-menu-and-game-over-screens-in-unity/
	public void showPaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(true);
		}
	}


	//hides those objects
	//https://www.sitepoint.com/adding-pause-main-menu-and-game-over-screens-in-unity/
	public void hidePaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(false);
		}
	}


	//function to be called to go to another scene
	public void LoadScene(string name){
		StartCoroutine(LevelLoad(name));
	} 
		

	//Quit the game
	public void quit(){
		Application.Quit ();
		Debug.Log("Game is exiting");
	}


	//replay the previous scene
	public void replay(){
		StartCoroutine(LevelLoad(previousScene));
	}


	//go to next game level
	public void nextLevel(){
		if (previousScene == highestLevel) {
			StartCoroutine (LevelLoad ("Win"));
		} else {
			StartCoroutine (LevelLoad (nextScene));
		}
	}


	//load level by name after one sceond delay
	IEnumerator LevelLoad(string name){
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene(name);
	}


	//load level by index after one sceond delay
	IEnumerator LevelLoad(int index){
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(index);
	}

}
