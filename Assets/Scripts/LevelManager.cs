using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
    public void LoadLevel(string name)
    {
        Debug.Log("New level load: " + name);
        Application.LoadLevel(name);

    }
    public void QuitGame()
    {
        Debug.Log("Quit request");
        Application.Quit();
    }
}
