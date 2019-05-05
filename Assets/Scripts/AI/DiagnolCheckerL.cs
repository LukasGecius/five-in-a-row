using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagnolCheckerL : MonoBehaviour
{

    int colmCount = Stats.boardSize - 5;
    int checkedPosX = 4;
    int checkedPosY = Stats.boardSize - 5;

    // Start is called before the first frame update
    void Start()
    {

    }
    int colmsChecked = 0;
    bool stopped = false;







    int hitCount = 5;
    int startHit = 5;
    int startHitB = 5;

    // Update is called once per frame
    void Update()
    {
        DangerCheck();
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
                cellToMove = GameObject.Find(string.Format("{0} {1}", checkedPosY, (Stats.boardSize - 1) ));
                diagnolCheckBody.transform.position = cellToMove.transform.position;
                checkedPosY--;
                hitCount = startHitB;
                startHitB++;
            }
        }



        // Shooting checker

        if (Input.GetKeyDown("space"))
        {
            diagnolCheckBody.velocity = new Vector3(-300, 210, 0); // Overall thinking time depends on checker velocity
            stopped = false;

        }

    }



    int blueCount = 0;
    int whiteCount = 0;
    bool WhiteWasSecond;
    private void OnTriggerExit(Collider other)
    {
        hitCount--;
       // Debug.Log(hitCount);
        // other.transform.GetComponent<Renderer>().material.color = Color.red;

        if (other.name == string.Format("{0} 0", Stats.boardSize - 1))
        {
            diagnolCheckBody.velocity = new Vector3(0, 0, 0);
            diagnolCheckBody.transform.position = GameObject.Find("0 4").transform.position;
            stopped = true;
           
        }
 
        // Debug.Log(hitCount);



        //   other.transform.GetComponent<Renderer>().material.color = Color.blue;
        
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
            Debug.Log("DANGER in pos:" + ((int.Parse(other.name[0].ToString())) + 1) + " " + (int.Parse(other.name[2].ToString()) - 1));
            Debug.Log(" and in pos: " + ((int.Parse(other.name[0].ToString())) - 3) + " " + (int.Parse(other.name[2].ToString()) + 3));
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

            Debug.Log("Danger in pos: " + ((int.Parse(other.name[0].ToString())) + 1) + " " + ((int.Parse(other.name[2].ToString())) - 1));

        }
        // IF AFTER ONE BLUE, WAS WHITE, THEN TWO BLUES
        else if (other.transform.GetComponent<Renderer>().material.color == Color.blue && whiteCount == 1 && blueCount == 2 && WhiteWasSecond == false)
        {
            Debug.Log("Danger in pos: " + ((int.Parse(other.name[0].ToString())) - 2) + " " + ((int.Parse(other.name[2].ToString())) + 2));
        }
       
    }

}
