using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DiagnolChecker : MonoBehaviour
{
    int CountOffset = 0;
    int boardSize = Stats.boardSize;
    int good = 10;
    int colmCount = Stats.boardSize - 5;
    int checkedPosX = Stats.boardSize - 5;
    int checkedPosY = Stats.boardSize - 5;
    int startPosX;
    int startPosY;
    // Start is called before the first frame update
    void Start()
    {
        if (boardSize == 8)
        {
            CountOffset = -2;
        }
        if (boardSize == 9)
        {
            CountOffset = -1;
        }
        if (boardSize > 10)
        {
            while (boardSize != good)
            {
                CountOffset++;
                boardSize--;
            }
        }
        checkedPosX = checkedPosX - CountOffset;
        checkedPosY = checkedPosY - CountOffset;
        startPosX = checkedPosX;
        startPosY = checkedPosY;
    }
    int colmsChecked = 0;
    bool stopped = false;
    int hitCount = 5;
    int startHit = 5;
    int startHitB = 5;


    void Update()
    {
        if (Stats.goCheckerDR == true)
        {
            stopped = false;
            DangerCheck();
            diagnolCheckBody.velocity = new Vector3(1050, 735, 0); // Overall thinking time depends on checker velocity Optimal: 1800, 1260
        }
        if (Stats.goCheckerDR == false)
        {
            Reset();
        }
    }
    private GameObject diagnolChecker;
    public Rigidbody diagnolCheckBody = new Rigidbody();
    void DangerCheck()
    {
        diagnolChecker = GameObject.Find(string.Format("Checker:0{0}", (Stats.boardSize - 5)));
        diagnolCheckBody = diagnolChecker.GetComponent<Rigidbody>();
        GameObject cellToMove = new GameObject();
        if (hitCount == 0)
        {
            if (Stats.boardSize - checkedPosX != 0)
            {
                checkedPosX++;
                cellToMove = GameObject.Find(string.Format("0 {0}", Stats.boardSize - checkedPosX));
                diagnolCheckBody.transform.position = cellToMove.transform.position;
                startHit++;
                hitCount = startHit;
            }
            if (Stats.boardSize - checkedPosX == 0 && stopped == false)
            {
                cellToMove = GameObject.Find(string.Format("{0} 0", Stats.boardSize - checkedPosY));
                diagnolCheckBody.transform.position = cellToMove.transform.position;
                checkedPosY++;
                hitCount = startHitB;
                startHitB++;
           //     Debug.Log("CheckedPosY: " + checkedPosY);
            }
        }
        // Shooting checker
    }
    public void Reset()
    {
        if (diagnolCheckBody != null)
        {
            diagnolCheckBody.velocity = new Vector3(0, 0, 0);
            diagnolCheckBody.transform.position = GameObject.Find(string.Format("0 {0}", (Stats.boardSize - 5))).transform.position;
        }

        Stats.goCheckerDR = false;
        Stats.goCheckerColm = false;
        Stats.goCheckerDL = false;
        Stats.goCheckerRow = false;
        stopped = true;
        hitCount = 5;
        startHit = 5;
        startHitB = 5;
        checkedPosX = startPosX;
        checkedPosY = startPosY;
    }


    int blueCount = 0;
    int whiteCount = 0;
    bool WhiteWasSecond;
    int redCount = 0;
    int winCheck;
    int resetCount;
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
        hitCount--;
        if (other.name == string.Format("{0} {0}", Stats.boardSize - 1))
        {
            Reset();
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
        // FOUND BLUE
        if (other.transform.GetComponent<Renderer>().material.color == Color.blue && blueCount == 0)
        {
            blueCount = 1;

        }
        // SECOND BLUE
        else if (other.transform.GetComponent<Renderer>().material.color == Color.blue && blueCount == 1)
        {
            blueCount = 2;
        }
        // THIRD BLUE IN A ROW
        else if (other.transform.GetComponent<Renderer>().material.color == Color.blue && blueCount == 2 && whiteCount == 0 && redCount == 0
            && GameObject.Find(((int.Parse(other.name[0].ToString())) - 3).ToString() + " " + ((int.Parse(other.name[2].ToString()) - 3).ToString())).GetComponent<Renderer>().material.color != Color.red)
        {
            if (int.Parse(other.name[0].ToString()) >= 0 && int.Parse(other.name[2].ToString()) < Stats.boardSize)  // Check is out of bounds
            {
                randomCell = rnd.Next(0, 2);
                if (randomCell == 0)
                {
                    selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) + 1).ToString() + " " + (int.Parse(other.name[2].ToString()) + 1).ToString());
                }
                else
                {
                    selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 3).ToString() + " " + (int.Parse(other.name[2].ToString()) - 3).ToString());
                } // picking random dangerous cell
                selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;
                Reset();
                Stats.moveCount++;
            }
            // Hitted thrid blue

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
            selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString());
            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;
            Reset();
            Stats.moveCount++;

        }
        // IF AFTER ONE BLUE, WAS WHITE, THEN TWO BLUES
        else if (other.transform.GetComponent<Renderer>().material.color == Color.blue && whiteCount == 1 && blueCount == 2 && WhiteWasSecond == false)
        {
            selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 2).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 2).ToString());
            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;
            Reset();
            Stats.moveCount++;

        }
    }
}