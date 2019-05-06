using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColmChecker : MonoBehaviour
{
        int colmsChecked = 0;


    void Start()
    {
        colmsChecked = 0;

    }

    private GameObject colChecker;
    public Rigidbody colCheckBody = new Rigidbody();



    // Update is called once per frame
    void Update()
    {
        if (Stats.goCheckerColm == true)
        {


            colChecker = GameObject.Find("Checker:01");
            colCheckBody = colChecker.GetComponent<Rigidbody>();
            colCheckBody.velocity = new Vector3(0, 2000, 0);
            DangerCheck();

        }
        if (Stats.goCheckerColm == false)
        {
            Reset();
        }
    }
    //* Calculating player choises


    int collisionCount;
    int colmCount = 1;
 




    void DangerCheck()
    {



        colChecker = GameObject.Find("Checker:01");
        colCheckBody = colChecker.GetComponent<Rigidbody>();


        // colmChecker move right if finished checking colm


        GameObject cellToMove = new GameObject();
        // cellToMove = GameObject.Find(string.Format("{0} 0", colmCount));

        if (colmsChecked < (Stats.boardSize + 1))
        {

            if (collisionCount == Stats.boardSize)
            {
                cellToMove = GameObject.Find(string.Format("0 {0}", colmCount));
                collisionCount = 0;
                colChecker.transform.position = cellToMove.transform.position;
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
            Reset();
        }


        // Shooting checker

    }

    public void Reset()
    {
        colCheckBody.velocity = new Vector3(0, 0, 0);
        colCheckBody.transform.position = GameObject.Find("0 0").transform.position;
        colmsChecked = 0;
        Debug.Log("ColmCheckerReseted");
        Stats.goCheckerColm = false;
        colmsChecked = 0;

        Stats.goCheckerDL = false;
        Stats.goCheckerDR = false;
        Stats.goCheckerRow = false;

    }

    


    public string[] DangerCellName;


    int blueCount = 0;
    int whiteCount = 0;
    bool WhiteWasSecond;
    int redCount = 0;

    public System.Random rnd = new System.Random();
    int randomCell;


    private void OnTriggerExit(Collider other)
    {
        /*
        Debug.Log(redCount);
        if (other.transform.GetComponent<Renderer>().material.color == Color.white)
        {
            other.transform.GetComponent<Renderer>().material.color = Color.green;
        }
        else if (other.transform.GetComponent<Renderer>().material.color == Color.green)
        {
            other.transform.GetComponent<Renderer>().material.color = Color.white;
        }
        */

        GameObject selectedCell = new GameObject();

        if (other.transform.GetComponent<Renderer>().material.color == Color.red)
        {
            redCount++;
        }

        // RESET WHEN 2 THERE ARE TWO WHITES IN A ROW
        if (other.transform.GetComponent<Renderer>().material.color == Color.white && whiteCount == 2)
        {
            blueCount = 0;
            whiteCount = 0;
            WhiteWasSecond = false;
            redCount = 0;
        }

        // DangerCheck
        
        if (other.transform.GetComponent<Renderer>().material.color == Color.blue && blueCount == 0)
        {
            blueCount = 1;
            Debug.Log("BlueCount: " + blueCount);
        } // FOUND BLUE
        
        else if (other.transform.GetComponent<Renderer>().material.color == Color.blue && blueCount == 1)
        {
            blueCount = 2;
            Debug.Log("BlueCount: " + blueCount);
        } // SECOND BLUE
        // THIRD BLUE IN A ROW
        else if (other.transform.GetComponent<Renderer>().material.color == Color.blue && blueCount == 2 && whiteCount == 0 && redCount == 0
            && GameObject.Find(((int.Parse(other.name[0].ToString())) - 3) + " " + other.name[2]).GetComponent<Renderer>().material.color != Color.red
            && GameObject.Find(((int.Parse(other.name[0].ToString())) + 1) + " " + other.name[2].ToString()).GetComponent<Renderer>().material.color != Color.red
            )
        {
            if (true)  // Checking if the cell is in Board Bounds
            {
                randomCell = rnd.Next(0, 2);

                if (randomCell == 0)
                {
                    selectedCell = GameObject.Find((int.Parse(other.name[0].ToString())) - 3 + " " + other.name[2]);
                }
                else
                {
                    selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) + 1) + " " + other.name[2]);
                } // picking random dangerous cell

                selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;




                // Hitted thrid blue

                Debug.Log("BlueCount: " + blueCount);
                Debug.Log("DANGER in pos:" + ((int.Parse(other.name[0].ToString())) - 3 + " " + other.name[2]));
                Debug.Log(" and in pos: " + ((int.Parse(other.name[0].ToString())) + 1) + " " + other.name[2]);
                Reset();
            }
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
                selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 1) + " " + other.name[2]);
                selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;


                Debug.Log("Danger in pos: " + ((int.Parse(other.name[0].ToString())) - 1) + " " + other.name[2]);
            Reset();

        }
        else if (other.transform.GetComponent<Renderer>().material.color == Color.blue && whiteCount == 1 && blueCount == 2 && WhiteWasSecond == false)
        {
                selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 2) + " " + other.name[2]);
                selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;


                Debug.Log("Danger in pos: " + ((int.Parse(other.name[0].ToString())) - 2) + " " + other.name[2]);
        }
        
        collisionCount++;
    }


}
