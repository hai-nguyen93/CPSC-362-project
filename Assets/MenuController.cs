using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void LoadTutorial(){
		Debug.Log("Load tutorial");
		//TODO
	}
	
	public void LoadGame(){
		SceneManager.LoadScene("PracticeGame");
	}
	
	public void Quit(){
		Debug.Log("Quit Game");
		Application.Quit();
	}
}
