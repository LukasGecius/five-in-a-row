using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Transform checkerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    RemoveGravity();
    
    }

    // Update is called once per frame
    void Update()
    {
        DangerCheck();
    }
    //* Calculating player choises


    private GameObject littleChecker;
    public Rigidbody rb = new Rigidbody();

    void RemoveGravity()
    {
        for(int i = 0; i < Stats.boardSize; i++)
            for(int f = 0; f < Stats.boardSize; f++)
            {
                littleChecker = GameObject.Find(string.Format("Checker:{0}{1}", i.ToString(), f.ToString()));
                if (littleChecker != null)
                {
                    rb = littleChecker.GetComponent<Rigidbody>();
                    rb.useGravity = false;
                }
            }

    }

   // string ignoreTag = "Cell";


    void DangerCheck()
    {

        Rigidbody rb;

        littleChecker = GameObject.Find("Checker:00");
        rb = littleChecker.GetComponent<Rigidbody>();
        //rb.isKinematic = true;

        // Shooting checker

        if (Input.GetKeyDown("space"))
        {
            rb.velocity = new Vector3(100, 0, 0);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
    }
}
