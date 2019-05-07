using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveColm : MonoBehaviour
{
    int colmsChecked = 0;
    // Start is called before the first frame update
    void Start()
    {
        colmsChecked = 0;   
    }

    public GameObject moveCol;
    public Rigidbody moveColBody = new Rigidbody();

    // Update is called once per frame
    void Update()
    {
        if (Stats.goMoveCol == true)
        {
            Debug.Log(string.Format("Checker:0{0}", (Stats.boardSize - 4)));
            moveCol = GameObject.Find(string.Format("Checker:0{0}", (Stats.boardSize - 4)));
            Debug.Log("moveColCheckerName: " + moveCol.name);
            moveColBody = moveCol.GetComponent<Rigidbody>();
            moveColBody.velocity = new Vector3(0, 100, 0);
            MoveCheck();
        }
        if (Stats.goMoveCol == false)
        {
            Reset();
        }
    }

    int collisionCount;
    int colmCount = 1;

    void MoveCheck()
    {
        moveCol = GameObject.Find(string.Format("Checker:0{0}", Stats.boardSize - 4));
        moveColBody = moveCol.GetComponent<Rigidbody>();

        GameObject cellToMove = new GameObject();

        if (colmsChecked < (Stats.boardSize + 1))
        {

            if (collisionCount == Stats.boardSize)
            {
                cellToMove = GameObject.Find(string.Format("0 {0}", colmCount));
                collisionCount = 0;
                moveCol.transform.position = cellToMove.transform.position;
                colmCount++;
                colmsChecked++;

                if (colmCount == Stats.boardSize)
                {
                    colmCount = 0;
                    Debug.Log("ColmCount;" + colmCount);
                }
            }
        }
        else
        {
            //  Reset();
            Debug.Log("this happned");
        }
    }

    public void Reset()
    {
        moveColBody.velocity = new Vector3(0, 0, 0);
        moveColBody.transform.position = GameObject.Find("0 1").transform.position;
        colmsChecked = 0;
        Stats.goMoveCol = false;
        Stats.goMoveDL = false;
        Stats.goMoveDR = false;
        Stats.goMoveRow = false;
    }

    private void OnTriggerExit(Collider other)
    {
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
        collisionCount++;
    }



}
