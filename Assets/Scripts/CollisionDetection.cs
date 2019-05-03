using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    

    private void OnTriggerExit(Collider other)
    {
        other.transform.GetComponent<Renderer>().material.color = Color.blue;
        Debug.Log("triggered");
    }
}
