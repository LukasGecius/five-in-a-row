using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowChecker : MonoBehaviour
{
    int colmsChecked = 0;
    void Start()
    {
        colmsChecked = 0;
        RemoveGravity();
    
    }

    // Update is called once per frame
    void Update()
    {
        DangerCheck();
    }
    //* Calculating player choises


    int collisionCount;
    int colmCount = 1;


    private GameObject rowChecker;
   // private GameObject colmChecker;


    public Rigidbody rowCheckBody = new Rigidbody();
    // public Rigidbody colmCheckBody = new Rigidbody();

    public void RemoveGravity()
    {
        for(int i = 0; i < Stats.boardSize; i++)
            for(int f = 0; f < Stats.boardSize; f++)
            {
                rowChecker = GameObject.Find(string.Format("Checker:{0}{1}", i.ToString(), f.ToString()));
                if (rowChecker != null)
                {
                    rowCheckBody = rowChecker.GetComponent<Rigidbody>();
                    rowCheckBody.useGravity = false;
                }
            }

    }

   // string ignoreTag = "Cell";


    void DangerCheck()
    {

        

        rowChecker = GameObject.Find("Checker:00");
        rowCheckBody = rowChecker.GetComponent<Rigidbody>();
        

        // rowChecker move up if finished checking row

        
        GameObject cellToMove = new GameObject();
        // cellToMove = GameObject.Find(string.Format("{0} 0", colmCount));

        if (colmsChecked < Stats.boardSize)
        {
            if (collisionCount == Stats.boardSize)
            {
                cellToMove = GameObject.Find(string.Format("{0} 0", colmCount));
                collisionCount = 0;
                rowChecker.transform.position = cellToMove.transform.position;
                colmCount++;

                colmsChecked++;
                if (colmCount == Stats.boardSize)
                {
                    colmCount = 0;
                }
            }
        }
        else
        {
            
            // Stop and rePosition checker
            rowCheckBody.velocity = new Vector3(0, 0, 0);
            rowCheckBody.transform.position = GameObject.Find("0 0").transform.position;
            colmsChecked = 0;
            
            Debug.Log("Danger search in rows complete");
        }


        if (Input.GetKeyDown("space"))
        {
        //    rowCheckBody.velocity = new Vector3(300, 0, 0); // Overall thinking time depends on checker velocity
            
        }

    }

    public string[] DangerCellName;


    int blueCount = 0;
    int whiteCount = 0;
    bool WhiteWasSecond;
    private void OnTriggerExit(Collider other)
    {
        // RESET WHEN 2 THERE ARE TWO WHITES IN A ROW
        if (other.transform.GetComponent<Renderer>().material.color == Color.white && whiteCount == 2)
        {
            blueCount = 0;
            whiteCount = 0;
            WhiteWasSecond = false;
        }


        // DangerCheck
        // FOUND BLUE
        if (other.transform.GetComponent<Renderer>().material.color == Color.blue && blueCount == 0) 
        {
            blueCount = 1;
            Debug.Log("BlueCount: " + blueCount);
        }
        // SECOND BLUE
        else if (other.transform.GetComponent<Renderer>().material.color == Color.blue && blueCount == 1)
        {
            blueCount = 2;
            Debug.Log("BlueCount: " + blueCount);
        }
        // THIRD BLUE IN A ROW
        else if (other.transform.GetComponent<Renderer>().material.color == Color.blue && blueCount == 2 && whiteCount == 0)
        {
            // Hitted thrid blue
            Debug.Log("BlueCount: " + blueCount);
            Debug.Log("DANGER in pos:" + other.name[0] + " " + ((int.Parse(other.name[2].ToString())) - 3));
            Debug.Log(" and in pos: " + other.name[0] + " " + ((int.Parse(other.name[2].ToString())) + 1) );
        }

        // If after 2 BLUES is WHITE
        else if (other.transform.GetComponent<Renderer>().material.color == Color.white && blueCount == 2)
        {
            whiteCount++;
            Debug.Log("WhiteCount: " + whiteCount);
            WhiteWasSecond = true;
        }
        // IF AFTER BLUE, WAS WHITE, THEN BLUE AGAIN
        else if (other.transform.GetComponent<Renderer>().material.color == Color.white && blueCount == 1)
        {
            whiteCount++;
            Debug.Log("WhiteCount: " + whiteCount);
            WhiteWasSecond = false;
        }

        // IF AFTER TWO BLUES, ONE WHITE AGAIN BLUE
        else if (other.transform.GetComponent<Renderer>().material.color == Color.blue && whiteCount == 1 && blueCount == 2 && WhiteWasSecond == true)
        {

                Debug.Log("Danger in pos: " + other.name[0] + " " + ((int.Parse(other.name[2].ToString())) - 1));

        }
        else if (other.transform.GetComponent<Renderer>().material.color == Color.blue && whiteCount == 1 && blueCount == 2 && WhiteWasSecond == false)
        {
            Debug.Log("Danger in pos: " + other.name[0] + " " + ((int.Parse(other.name[2].ToString())) - 2));
        }

        collisionCount++;
    }

    // 
}
