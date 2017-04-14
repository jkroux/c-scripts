using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
	GameObject[] pauseObjects;
	public GameObject player;
	private int previousScene;
	private int nextScene;

	void Start () {
		previousScene = PlayerPrefs.GetInt( "previousScene" );
//		previousScene = SceneManager.GetSceneAt(previousSceneInt).name();
		nextScene = previousScene + 1;
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		hidePaused();
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.P))
		{
			if(Time.timeScale == 1)
			{
				Time.timeScale = 0;
				showPaused();
			} else if (Time.timeScale == 0){
				Time.timeScale = 1;
				hidePaused();
			}
		}
	}


	//Restart Button's method
	public void Reload(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	//Play Button's method
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
	public void showPaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(true);
		}
	}

	//hides those objects
	public void hidePaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(false);
		}
	}

//	//Homepage Button's method
//	public void LoadLevel(string level){
//		SceneManager.LoadScene(level);
//	}

	//function to be called to go to another scene
	public void LoadScene(string name){
		StartCoroutine(LevelLoad(name));
	} 

	//load level by name after one sceond delay
	IEnumerator LevelLoad(string name){
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(name);
	}

	//load level by index after one sceond delay
	IEnumerator LevelLoad(int index){
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(index);
	}


	//Quit the game
	public void quit(){
		Application.Quit ();
		Debug.Log("Game is exiting");
	}

	public void replay(){
		StartCoroutine(LevelLoad(previousScene));
	}

	public void nextLevel(){
		StartCoroutine(LevelLoad(nextScene));
	}
}
