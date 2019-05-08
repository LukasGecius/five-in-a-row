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
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (Stats.moveCount % 2 == 0)
        {
            Reset();
        }

        if (Stats.goMoveRow == true)
        {
      //      Debug.Log("Go");
            moveRowBody.velocity = new Vector3(1600, 0, 0);
            MoveCheck();
        }
      else if (Stats.goMoveRow == false)
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
        GameObject cellToMove = new GameObject();
        moveRow = GameObject.Find("Checker:02");
        moveRowBody = moveRow.GetComponent<Rigidbody>();



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

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(3);
        Application.LoadLevel(2);
    }

    public void Reset()
    {
        moveRow = GameObject.Find("Checker:02");
        moveRowBody = moveRow.GetComponent<Rigidbody>();
        moveRowBody.velocity = new Vector3(0, 0, 0);
        moveRowBody.transform.position = GameObject.Find("0 0").transform.position;
        colmsChecked = 0;
        Stats.goMoveRow = false;
        Stats.goMoveCol = false;
        Stats.goMoveDL = false;
        Stats.goMoveDR = false;

     //   Debug.Log("MoveCheck in row complete");
    }

    public System.Random rnd = new System.Random();

    public int randomPick = 0;


    public int redCount = 0;
    private void OnTriggerExit(Collider other)
    {
        GameObject selectedCell = new GameObject();
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
        if (other.transform.GetComponent<Renderer>().material.color == Color.red)
        {
            redCount++;
        }
        if (other.transform.GetComponent<Renderer>().material.color != Color.red)
        {
            redCount = 0;
        }

        if (other.name == string.Format("{0} {0}", Stats.boardSize - 1))
        {
        //    Debug.Log("No moves in row");
        }


        if (redCount == 3
            && GameObject.Find(other.name[0].ToString() + " " + ((int.Parse(other.name[2].ToString())) + 1).ToString()) != null
            && GameObject.Find(other.name[0].ToString() + " " + ((int.Parse(other.name[2].ToString())) - 3).ToString())
            && GameObject.Find(other.name[0].ToString() + " " + ((int.Parse(other.name[2].ToString())) + 1).ToString()).GetComponent<Renderer>().material.color == Color.white
            && GameObject.Find(other.name[0].ToString() + " " + ((int.Parse(other.name[2].ToString())) - 3).ToString()).GetComponent<Renderer>().material.color == Color.white
            )
        {
            Debug.Log("Performing winning move");
            System.Random rnd = new System.Random();
            randomPick = rnd.Next(0, 2);
            Debug.Log("option nr: " + randomPick);

                if (randomPick == 0)
                {
                    selectedCell = GameObject.Find(other.name[0].ToString() + " " + ((int.Parse(other.name[2].ToString())) + 1).ToString());

                }
                else
                {
                    selectedCell = GameObject.Find(other.name[0].ToString() + " " + ((int.Parse(other.name[2].ToString())) - 3).ToString());
                } 

                selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;
            Debug.Log("Colored: " + selectedCell.name);

            if (GameObject.Find((int.Parse(other.name[0].ToString())).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 2).ToString()) != null
 && GameObject.Find((int.Parse(other.name[0].ToString()) ).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 4).ToString()) != null)
            {
                Stats.moveCount++;
                StartCoroutine(Stop());
                Stats.moveMade = true;

            }
            else
            {
                Reset();
                Stats.moveCount++;
                Stats.moveMade = true;
            }
            



        } // If redCount 3 - win

       else if (redCount == 2
            && GameObject.Find(other.name[0].ToString() + " " + ((int.Parse(other.name[2].ToString())) + 1).ToString()) != null
            && GameObject.Find(other.name[0].ToString() + " " + ((int.Parse(other.name[2].ToString())) - 2).ToString()) != null

            && GameObject.Find(other.name[0].ToString() + " " + ((int.Parse(other.name[2].ToString())) + 1).ToString()).GetComponent<Renderer>().material.color == Color.white
            && GameObject.Find(other.name[0].ToString() + " " + ((int.Parse(other.name[2].ToString())) - 2).ToString()).GetComponent<Renderer>().material.color == Color.white
            )
        {
            Debug.Log("Performing good move");
            System.Random rnd = new System.Random();
            randomPick = rnd.Next(0, 2);
            Debug.Log("option nr: " + randomPick);

            if (randomPick == 0)
            {
                selectedCell = GameObject.Find(other.name[0].ToString() + " " + ((int.Parse(other.name[2].ToString())) + 1).ToString());

            }
            else
            {
                selectedCell = GameObject.Find(other.name[0].ToString() + " " + ((int.Parse(other.name[2].ToString())) - 2).ToString());
            }

            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;
            Debug.Log("Colored: " + selectedCell.name);
            Stats.moveCount++;
            Reset();

        } // Performing Good move

       else if (redCount == 1
            && GameObject.Find(other.name[0].ToString() + " " + ((int.Parse(other.name[2].ToString())) + 1).ToString()) != null
            && GameObject.Find(other.name[0].ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString()) != null
            && GameObject.Find(other.name[0].ToString() + " " + ((int.Parse(other.name[2].ToString())) + 1).ToString()).GetComponent<Renderer>().material.color == Color.white
            && GameObject.Find(other.name[0].ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString()).GetComponent<Renderer>().material.color == Color.white
    )
        {
            Debug.Log("Performing Ok move");
            System.Random rnd = new System.Random();
            randomPick = rnd.Next(0, 2);
        Debug.Log("option nr: " + randomPick);

            if (randomPick == 0)
            {
                selectedCell = GameObject.Find(other.name[0].ToString() + " " + ((int.Parse(other.name[2].ToString())) + 1).ToString());

            }
            else
            {
                selectedCell = GameObject.Find(other.name[0].ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString());
            }

            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;
            //      Debug.Log("Colored: " + selectedCell.name);
            Stats.moveCount++;
            Reset();
            Stats.moveMade = true;

        } // Performing OK move


        collisionCount++;
    }



}
