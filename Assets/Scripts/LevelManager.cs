using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    // Use this for initialization
    public void LoadLevel(string name)
    {
        if (Stats.boardSize < 8)
        {
            Debug.Log("Invalid size");
        }
        else
        {
            Debug.Log("New level load: " + name);
            Application.LoadLevel(name);
            Stats.moveCount = 0;
            Stats.shootPopSound = false;
        }

    }
    public void QuitGame()
    {
        Debug.Log("Quit request");
        Application.Quit();
    }
    public void Text_Changed(string newText)
    {
        int temp = int.Parse(newText);
 
            Stats.boardSize = temp;
        
        
    }
}

