﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DiagnolCheckerL : MonoBehaviour
{
    int colmCount = Stats.boardSize - 5;
    int checkedPosX = 4;
    int checkedPosY = Stats.boardSize - 5;

    void Start()
    {
    }
    int colmsChecked = 0;
    bool stopped = false;
    int hitCount = 5;
    int startHit = 5;
    int startHitB = 5;

    void Update()
    {
        if (Stats.goCheckerDL == true)
        {
            stopped = false;
            DangerCheck();
            diagnolCheckBody.velocity = new Vector3(-1050, 735, 0); // Overall thinking time depends on checker velocity
        }
        if (Stats.goCheckerDL == false)
        {
            Reset();
        }
    }
    private GameObject diagnolChecker;
    public Rigidbody diagnolCheckBody = new Rigidbody();
    void DangerCheck()
    {
        diagnolChecker = GameObject.Find("Checker:04");
        diagnolCheckBody = diagnolChecker.GetComponent<Rigidbody>();
        GameObject cellToMove = new GameObject();
        if (hitCount == 0)
        {
            if (Stats.boardSize > checkedPosX + 1)
            {
                checkedPosX++;
                cellToMove = GameObject.Find(string.Format("0 {0}", checkedPosX));
                diagnolCheckBody.transform.position = cellToMove.transform.position;
                startHit++;
                hitCount = startHit;
            }
            if (Stats.boardSize == checkedPosX + 1 && stopped == false)
            {
                cellToMove = GameObject.Find(string.Format("{0} {1}", checkedPosY, (Stats.boardSize - 1)));
                diagnolCheckBody.transform.position = cellToMove.transform.position;
                checkedPosY--;
                hitCount = startHitB;
                startHitB++;
            }
        }
        // Shooting checker
        if (Input.GetKeyDown("space"))
        {
        }
    }
    public void Reset()
    {
        if (diagnolCheckBody != null)
        { 
        diagnolCheckBody.velocity = new Vector3(0, 0, 0);
        diagnolCheckBody.transform.position = GameObject.Find("0 4").transform.position;
        }
        stopped = true;
        Stats.goCheckerDL = false;
        Stats.goCheckerColm = false;
        Stats.goCheckerDR = false;
        Stats.goCheckerRow = false;
        colmCount = Stats.boardSize - 5;
        checkedPosY = Stats.boardSize - 5;
        checkedPosX = 4;
        hitCount = 5;
        startHit = 5;
        startHitB = 5;
    }
    int blueCount = 0;
    int whiteCount = 0;
    bool WhiteWasSecond;
    int redCount = 0;
    public System.Random rnd = new System.Random();
    int randomCell;
    int winCheck;
    int resetCount;
    private void OnTriggerExit(Collider other)
    {
        hitCount--;
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

        if (other.name == string.Format("{0} 0", Stats.boardSize - 1))
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
            && GameObject.Find(((int.Parse(other.name[0].ToString())) - 3).ToString() + " " + (int.Parse(other.name[2].ToString()) + 3).ToString()).GetComponent<Renderer>().material.color != Color.red)
        {
            if (int.Parse(other.name[0].ToString()) >= 0 && int.Parse(other.name[2].ToString()) < Stats.boardSize)  // Checking if the cell is in Board Bounds
            {
                randomCell = rnd.Next(0, 2);
                if (randomCell == 0)
                {
                    selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) + 1).ToString() + " " + (int.Parse(other.name[2].ToString()) - 1).ToString());
                }
                else
                {
                    selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 3).ToString() + " " + (int.Parse(other.name[2].ToString()) + 3).ToString());
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
            selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 1).ToString());
            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;
            Reset();
            Stats.moveCount++;
        }

        // IF AFTER ONE BLUE, WAS WHITE, THEN TWO BLUES
        else if (other.transform.GetComponent<Renderer>().material.color == Color.blue && whiteCount == 1 && blueCount == 2 && WhiteWasSecond == false)
        {
            selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 2).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 2).ToString());
            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;
            Reset();
            Stats.moveCount++;

        }
    }
}