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

    // Update is called once per frame
    void Update()
    {
        if (Stats.goCheckerDR == true)
        {
            stopped = false;
            // Debug.Log("Should go");
            DangerCheck();
            diagnolCheckBody.velocity = new Vector3(1800, 1260, 0); // Overall thinking time depends on checker velocity Optimal: 1800, 1260


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
             //   Debug.Log("checkedPos" + checkedPosX);
                cellToMove = GameObject.Find(string.Format("0 {0}", Stats.boardSize - checkedPosX));
              //  Debug.Log("Jump to: " + string.Format("0 {0}", Stats.boardSize - checkedPosX));
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

        if (Input.GetKeyDown("space"))
        {
            diagnolCheckBody.velocity = new Vector3(1650, 1155, 0); // Overall thinking time depends on checker velocity
            stopped = false;

        }

    }

    public void Reset()
    {
        diagnolCheckBody.velocity = new Vector3(0, 0, 0);
        diagnolCheckBody.transform.position = GameObject.Find(string.Format("0 {0}", (Stats.boardSize - 5))).transform.position;
      //  Debug.Log("Diagnol danger Reset");
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

        // RedCount
        if (other.transform.GetComponent<Renderer>().material.color == Color.red)
        {
            redCount++;
            winCheck = 0;
        }

        if (other.transform.GetComponent<Renderer>().material.color == Color.white)
        {
            redCount = 0;
            resetCount++;
            //      Debug.Log("ResetCount: " + resetCount);
            //      Debug.Log("RedCountReset");

        }

        if (other.transform.GetComponent<Renderer>().material.color == Color.blue)
        {
            winCheck++;
            Debug.Log("Wincheck: " + winCheck);

        }

        if (winCheck == 5)
        {
            Application.LoadLevel(3);
        }

        if (resetCount > 1)
        {
            winCheck = 0;
            resetCount = 0;
            //    redCount = 0;
            //     blueCount = 0;
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
            Debug.Log("BlueCount: " + blueCount);
        }
        // SECOND BLUE
        else if (other.transform.GetComponent<Renderer>().material.color == Color.blue && blueCount == 1)
        {
            blueCount = 2;
            Debug.Log("BlueCount: " + blueCount);
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
        //    Debug.Log("BlueCount: " + blueCount);
         //   Debug.Log("DANGER in pos:" + ((int.Parse(other.name[0].ToString())) + 1) + " " + (int.Parse(other.name[2].ToString()) + 1));
          //  Debug.Log(" and in pos: " + ((int.Parse(other.name[0].ToString())) - 3) + " " + (int.Parse(other.name[2].ToString()) - 3));
        }

        // If after 2 BLUES is WHITE
        else if (other.transform.GetComponent<Renderer>().material.color == Color.white && blueCount == 2)
        {
            whiteCount++;
         //   Debug.Log("WhiteCount: " + whiteCount);
            WhiteWasSecond = true;
        }
        // IF AFTER BLUE, WAS WHITE, THEN BLUE AGAIN
        else if (other.transform.GetComponent<Renderer>().material.color == Color.white && blueCount == 1)
        {
            whiteCount++;
         //   Debug.Log("WhiteCount: " + whiteCount);
            WhiteWasSecond = false;
        }

        // IF AFTER TWO BLUES, ONE WHITE AGAIN BLUE
        else if (other.transform.GetComponent<Renderer>().material.color == Color.blue && whiteCount == 1 && blueCount == 2 && WhiteWasSecond == true)
        {
            selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString());
            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;
            Reset();
            Stats.moveCount++;

         //   Debug.Log("Danger in pos: " + ((int.Parse(other.name[0].ToString())) - 1) + " " + ((int.Parse(other.name[2].ToString())) - 1));

        }
        // IF AFTER ONE BLUE, WAS WHITE, THEN TWO BLUES
        else if (other.transform.GetComponent<Renderer>().material.color == Color.blue && whiteCount == 1 && blueCount == 2 && WhiteWasSecond == false)
        {
            selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 2).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 2).ToString());
            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;
            Reset();
            Stats.moveCount++;

        //    Debug.Log("Danger in pos: " + ((int.Parse(other.name[0].ToString())) - 2) + " " + ((int.Parse(other.name[2].ToString())) - 2));
        }

    }

}