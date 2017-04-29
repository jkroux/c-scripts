using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UpdateMemes : MonoBehaviour {

	public static Sprite mostRecentSprite;
	public Sprite spriteToPass;

//	private int SmallestIndOfLevel =6;
//	private int BiggestIndOfLevel=12;
//	private int TransitionIndex=5;
	void Awake()
	{
			mostRecentSprite = spriteToPass;

	}

}