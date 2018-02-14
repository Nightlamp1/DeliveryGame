using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadLevel()
    {
        Application.LoadLevel("Level1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2", LoadSceneMode.Single);
    }
}
