using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ColmChecker : MonoBehaviour
{
    int colmsChecked = 0;
    void Start()
    {
        colmsChecked = 0;
        Reset();
    }
    private GameObject colChecker;
    public Rigidbody colCheckBody = new Rigidbody();

    void Update()
    {
        if (Stats.goCheckerColm == true)
        {
            colChecker = GameObject.Find("Checker:01");
            colCheckBody = colChecker.GetComponent<Rigidbody>();
            colCheckBody.velocity = new Vector3(0, 1200, 0);
            DangerCheck();
        }
        if (Stats.goCheckerColm == false)
        {
            Reset();
        }
    }
    //* Calculating player choises
    int collisionCount;
    int colmCount = 2;
    void DangerCheck()
    {
        colChecker = GameObject.Find("Checker:01");
        colCheckBody = colChecker.GetComponent<Rigidbody>();
        // colmChecker move right if finished checking colm
        GameObject cellToMove = new GameObject();

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
        if (colCheckBody != null)
        { 
        colCheckBody.velocity = new Vector3(0, 0, 0);
        colCheckBody.transform.position = GameObject.Find("0 0").transform.position;
        }
        colmsChecked = 0;

        Stats.goCheckerColm = false;
        colmCount = 2;
        collisionCount = 0;
        Stats.goCheckerDL = false;
        Stats.goCheckerDR = false;
        Stats.goCheckerRow = false;
    }
    public string[] DangerCellName;
    int blueCount = 0;
    int whiteCount = 0;
    bool WhiteWasSecond;
    int redCount = 0;
    int resetCount;
    int winCheck;
    public System.Random rnd = new System.Random();
    int randomCell;
    private void OnTriggerExit(Collider other)
    {
        
        GameObject selectedCell = new GameObject();
        if (other.transform.GetComponent<Renderer>().material.color == Color.red)
        {
            redCount++;
            winCheck = 0;
        }
        if (other.transform.GetComponent<Renderer>().material.color == Color.white)
        {
            redCount = 0;
            resetCount++;

        }
        if (other.transform.GetComponent<Renderer>().material.color == Color.blue)
        {
            winCheck++;

        }
        if (winCheck == 5)
        {
            Application.LoadLevel(3);
        }
        if (resetCount > 1)
        {
            winCheck = 0;
            resetCount = 0;

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

        } // FOUND BLUE
        else if (other.transform.GetComponent<Renderer>().material.color == Color.blue && blueCount == 1)
        {
            blueCount = 2;

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

                Reset();
                Stats.moveCount++;
            }
        }
        // If after 2 BLUES is WHITE
        else if (other.transform.GetComponent<Renderer>().material.color == Color.white && blueCount == 2)
        {
            whiteCount++;

            WhiteWasSecond = true;
        }
        // IF AFTER BLUE, WAS WHITE, THEN BLUE AGAIN
        else if (other.transform.GetComponent<Renderer>().material.color == Color.white && blueCount == 1)
        {
            whiteCount++;

            WhiteWasSecond = false;
        }
        // IF AFTER TWO BLUES, ONE WHITE AGAIN BLUE
        else if (other.transform.GetComponent<Renderer>().material.color == Color.blue && whiteCount == 1 && blueCount == 2 && WhiteWasSecond == true)
        {
            selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 1) + " " + other.name[2]);
            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;

            Reset();
            Stats.moveCount++;
        }
        else if (other.transform.GetComponent<Renderer>().material.color == Color.blue && whiteCount == 1 && blueCount == 2 && WhiteWasSecond == false)
        {
            selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 2) + " " + other.name[2]);
            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;

            Reset();
            Stats.moveCount++;
        }
        collisionCount++;
    }
}