using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour
{
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void startGame(){
		Application.LoadLevel("StefT2");
	}
	
	void quitGame(){
		Application.Quit();
	}
	
	void loadGame(){
		//Nope?
	}
}
