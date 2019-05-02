using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //* Calculating player choises
    private GameObject checker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    void DangerCheck()
    {
        for (int i = 0; i < Stats.boardSize; i++)
        {
            checker.transform.position =
        }
    }
}
