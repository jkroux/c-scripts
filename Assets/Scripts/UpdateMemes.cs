using UnityEngine;

public class UpdateMemes : MonoBehaviour {

	public static Sprite mostRecentSprite;
	public Sprite spriteToPass;
	
	void Awake()
	{
			mostRecentSprite = spriteToPass;
	}

}