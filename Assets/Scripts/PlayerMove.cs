using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{






 /*   Color[] colors = new Color[] { Color.white, Color.red, Color.blue };
    private int currentColor, length;
*/
    void Start()
    {





/*
        currentColor = 0;
        length = colors.Length;

*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 1000))
            {
                print(hitInfo.collider.gameObject.name);
                hitInfo.transform.GetComponent<Renderer>().material.color = Color.blue;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 1000))
            {
                print(hitInfo.collider.gameObject.name);
                hitInfo.transform.GetComponent<Renderer>().material.color = Color.red;
            }
        }




        /*   if (Input.GetMouseButtonDown(0))
           {
               Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
               RaycastHit hit;
               if (Physics.Raycast(ray, out hit, 1000))
               {
                   currentColor = (currentColor + 1) % length;

                   GetComponent<Renderer>().material.color = colors[currentColor];

               }
           }*/
    }
}
