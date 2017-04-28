using UnityEngine;
using UnityEngine.SceneManagement;


public class UpdateMemes : MonoBehaviour {

	public static Sprite mostRecentSprite;
	public Sprite spriteToPass;

	void Awake()
	{
		mostRecentSprite = spriteToPass;
	}

}