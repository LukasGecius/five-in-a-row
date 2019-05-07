using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRow : MonoBehaviour
{
    int colmsChecked = 0;

    // Start is called before the first frame update
    void Start()
    {
        moveRow = GameObject.Find("Checker:02");
        moveRowBody = moveRow.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Stats.goMoveRow == true)
        {
            Debug.Log("Go");
            moveRowBody.velocity = new Vector3(100, 0, 0);
            MoveCheck();
        }
        if (Stats.goMoveRow == false)
        {
            Reset();
        }
    }

    int collisionCount;
    int colmCount = 1;

    private GameObject moveRow;

    public Rigidbody moveRowBody = new Rigidbody();

    void MoveCheck()
    {
        moveRow = GameObject.Find("Checker:02");
        moveRowBody = moveRow.GetComponent<Rigidbody>();

        GameObject cellToMove = new GameObject();

        if (colmsChecked < Stats.boardSize)
        {
            if (collisionCount == Stats.boardSize)
            {
                cellToMove = GameObject.Find(string.Format("{0} 0", colmCount));
                collisionCount = 0;
                moveRow.transform.position = cellToMove.transform.position;
                colmCount++;

                colmsChecked++;
                if(colmCount == Stats.boardSize)
                {
                    colmCount = 0;
                }
            }
        }
        else
        {
            Reset();
        }


    }
    public void Reset()
    {
        moveRowBody.velocity = new Vector3(0, 0, 0);
        moveRowBody.transform.position = GameObject.Find("0 0").transform.position;
        colmsChecked = 0;
        Stats.goMoveRow = false;
        Stats.goMoveCol = false;
        Stats.goMoveDL = false;
        Stats.goMoveDR = false;

        Debug.Log("MoveCheck in row complete");
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
        if (other.name == string.Format("{0} {0}", Stats.boardSize - 1))
        {
            Reset();
            Debug.Log("AllGoodAndGreen?");

        }
        collisionCount++;
    }



}
