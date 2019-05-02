// Color Changer does not work yet https://answers.unity.com/questions/856790/click-gameobject-to-change-color.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    
    Color[] colors = new Color[] { Color.white, Color.red, Color.blue };
    private int currentColor, length;
    void Start()
    {
        
        currentColor = 0;
        length = colors.Length;
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                currentColor = (currentColor + 1) % length;
                
                GetComponent<Renderer>().material.color = colors[currentColor];
                
            }
        }
    }
}
