using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDL : MonoBehaviour
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

        if (Stats.goMoveDL == true)
        {
            stopped = false;
            // Debug.Log("Should go");
            DangerCheck();
            diagnolCheckBody.velocity = new Vector3(-1650, 1155, 0); // Overall thinking time depends on checker velocity


        }
        if (Stats.goMoveDL == false)
        {
            Reset();
        }





    }


    private GameObject diagnolChecker;
    public Rigidbody diagnolCheckBody = new Rigidbody();

    void DangerCheck()
    {



        diagnolChecker = GameObject.Find(string.Format("Checker:0{0}", Stats.boardSize - 2));
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
        diagnolCheckBody.velocity = new Vector3(0, 0, 0);
        diagnolCheckBody.transform.position = GameObject.Find("0 4").transform.position;
        stopped = true;
        Debug.Log("MoverReset");

        Stats.goMoveDL = false;
        Stats.goMoveCol = false;
        Stats.goMoveDR = false;
        Stats.goMoveRow = false;

        colmCount = Stats.boardSize - 5;
        checkedPosY = Stats.boardSize - 5;
        checkedPosX = 4;

        hitCount = 5;
        startHit = 5;
        startHitB = 5;

    }

    private void OnTriggerExit(Collider other)
    {
        hitCount--;
        if (other.transform.GetComponent<Renderer>().material.color == Color.white)
        {
            other.transform.GetComponent<Renderer>().material.color = Color.green;
        }
        else if (other.transform.GetComponent<Renderer>().material.color == Color.green)
        {
            other.transform.GetComponent<Renderer>().material.color = Color.white;
        }

    }
}
