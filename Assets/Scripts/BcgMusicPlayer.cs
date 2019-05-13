using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BcgMusicPlayer : MonoBehaviour
{
    static BcgMusicPlayer instance = null;
    
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }
}
