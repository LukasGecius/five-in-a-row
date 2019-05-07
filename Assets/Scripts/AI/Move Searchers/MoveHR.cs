using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHR : MonoBehaviour
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
        if (Stats.goMoveDR == true)
        {
            stopped = false;
            // Debug.Log("Should go");
            DangerCheck();
            diagnolCheckBody.velocity = new Vector3(300, 210, 0); // Overall thinking time depends on checker velocity Optimal: 1800, 1260


        }

        if (Stats.goMoveDR == false)
        {
            Reset();
        }
    }
    private GameObject diagnolChecker;
    public Rigidbody diagnolCheckBody = new Rigidbody();

    void DangerCheck()
    {



        diagnolChecker = GameObject.Find(string.Format("Checker:0{0}", (Stats.boardSize - 3)));
        diagnolCheckBody = diagnolChecker.GetComponent<Rigidbody>();
        GameObject cellToMove = new GameObject();

        if (hitCount == 0)
        {

            if (Stats.boardSize - checkedPosX != 0)
            {
                checkedPosX++;
                Debug.Log("checkedPos" + checkedPosX);
                cellToMove = GameObject.Find(string.Format("0 {0}", Stats.boardSize - checkedPosX));
                Debug.Log("Jump to: " + string.Format("0 {0}", Stats.boardSize - checkedPosX));
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

                Debug.Log("CheckedPosY: " + checkedPosY);
            }
        }

    }

    public void Reset()
    {
        diagnolCheckBody.velocity = new Vector3(0, 0, 0);
        diagnolCheckBody.transform.position = GameObject.Find(string.Format("0 {0}", (Stats.boardSize - 5))).transform.position; // Might be a sudden death, sometimes it drops value to 
        Debug.Log("MoveDR reset");
        Stats.goMoveDR = false;
        Stats.goMoveRow = false;
        Stats.goMoveCol = false;
        Stats.goMoveDL = false;

        stopped = true;
        hitCount = 5;
        startHit = 5;
        startHitB = 5;
        checkedPosX = startPosX;
        checkedPosY = startPosY;
    }


    private void OnTriggerExit(Collider other)
    {
        hitCount--;

        /*
        if (other.transform.GetComponent<Renderer>().material.color == Color.white)
        {
            other.transform.GetComponent<Renderer>().material.color = Color.green;
        }
        else if (other.transform.GetComponent<Renderer>().material.color == Color.green)
        {
            other.transform.GetComponent<Renderer>().material.color = Color.white;
        }
        */
        // DONT FORGET TO RESET IF CONDITION IS MET

    }

}
