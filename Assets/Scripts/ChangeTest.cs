using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTest : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        gameObject.transform.GetComponent<Renderer>().material.color = Color.blue;
    }
}
