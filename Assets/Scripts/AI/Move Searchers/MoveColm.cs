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
        Reset();
    }
    public GameObject moveCol;
    public Rigidbody moveColBody = new Rigidbody();
    // Update is called once per frame

    void Update()
    {
        if (Stats.moveCount % 2 == 0)
        {
            Reset();
        }
        if (Stats.goMoveCol == true)
        {
          //  Debug.Log(string.Format("Checker:0{0}", (Stats.boardSize - 4)));
            moveCol = GameObject.Find(string.Format("Checker:0{0}", (Stats.boardSize - 4)));
          //  Debug.Log("moveColCheckerName: " + moveCol.name);
            moveColBody = moveCol.GetComponent<Rigidbody>();
            moveColBody.velocity = new Vector3(0, 1200, 0);
            MoveCheck();
        }
       else if (Stats.goMoveCol == false)
        {
            Reset();
        }
    }

    int collisionCount;
    int colmCount = 2;

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
                }
            }
        }
    }

    public void Reset()
    {
        moveCol = GameObject.Find(string.Format("Checker:0{0}", (Stats.boardSize - 4)));
        if (moveColBody != null)
        { 
        moveColBody = moveCol.GetComponent<Rigidbody>();
        moveColBody.velocity = new Vector3(0, 0, 0);
        moveColBody.transform.position = GameObject.Find("0 1").transform.position;
        }
        colmsChecked = 0;
        colmCount = 2;
        Stats.goMoveCol = false;
        Stats.goMoveDL = false;
        Stats.goMoveDR = false;
        Stats.goMoveRow = false;
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(3);
        Application.LoadLevel(2);
    }

    public int redCount;
    public int randomPick;

    private void OnTriggerExit(Collider other)
    {
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
        }
        if (redCount == 3
            && GameObject.Find(((int.Parse(other.name[0].ToString())) + 1).ToString() + " " + (other.name[2].ToString()).ToString()) != null
             && GameObject.Find(((int.Parse(other.name[0].ToString())) - 3).ToString() + " " + ((other.name[2].ToString())).ToString()) != null
               && GameObject.Find(((int.Parse(other.name[0].ToString())) + 1).ToString() + " " + (other.name[2].ToString()).ToString()).GetComponent<Renderer>().material.color == Color.white
                 && GameObject.Find(((int.Parse(other.name[0].ToString())) - 3).ToString() + " " + ((other.name[2].ToString())).ToString()).GetComponent<Renderer>().material.color == Color.white
    )
        {
            GameObject selectedCell = new GameObject();

            System.Random rnd = new System.Random();
            randomPick = rnd.Next(0, 2);

            if (randomPick == 0)
            {
                selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) + 1).ToString() + " " + (other.name[2].ToString()).ToString());
            }
            else
            {
                selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 3).ToString() + " " + ((other.name[2].ToString())).ToString());
            }
            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;

            Stats.moveCount++;
            Stats.moveMade = true;
            StartCoroutine(Stop());
        } 

       else if (redCount == 2
            && GameObject.Find(((int.Parse(other.name[0].ToString())) + 1).ToString() + " " + (other.name[2].ToString()).ToString()) != null
             && GameObject.Find(((int.Parse(other.name[0].ToString())) - 2).ToString() + " " + ((other.name[2].ToString())).ToString()) != null
              && GameObject.Find(((int.Parse(other.name[0].ToString())) + 1).ToString() + " " + (other.name[2].ToString()).ToString()).GetComponent<Renderer>().material.color == Color.white
               && GameObject.Find(((int.Parse(other.name[0].ToString())) - 2).ToString() + " " + ((other.name[2].ToString())).ToString()).GetComponent<Renderer>().material.color == Color.white
    )
        {
            GameObject selectedCell = new GameObject();

            System.Random rnd = new System.Random();
            randomPick = rnd.Next(0, 2);

            if (randomPick == 0)
            {
                selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) + 1).ToString() + " " + (other.name[2].ToString()).ToString());
            }
            else
            {
                selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 2).ToString() + " " + ((other.name[2].ToString())).ToString());
            }
            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;
            Stats.moveCount++;
            Stats.moveMade = true;
            Reset();
        }
      else  if (redCount == 1
            && GameObject.Find(((int.Parse(other.name[0].ToString())) + 1).ToString() + " " + (other.name[2].ToString()).ToString()) != null
            && GameObject.Find(((int.Parse(other.name[0].ToString())) - 1).ToString() + " " + ((other.name[2].ToString())).ToString()) != null
&& GameObject.Find(((int.Parse(other.name[0].ToString())) + 1).ToString() + " " + (other.name[2].ToString()).ToString()).GetComponent<Renderer>().material.color == Color.white
&& GameObject.Find(((int.Parse(other.name[0].ToString())) - 1).ToString() + " " + ((other.name[2].ToString())).ToString()).GetComponent<Renderer>().material.color == Color.white
)
        {
            GameObject selectedCell = new GameObject();

            System.Random rnd = new System.Random();
            randomPick = rnd.Next(0, 2);

            if (randomPick == 0)
            {
                selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) + 1).ToString() + " " + (other.name[2].ToString()).ToString());
            }
            else
            {
                selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 1).ToString() + " " + ((other.name[2].ToString())).ToString());
            }
            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;

            Stats.moveCount++;
            Stats.moveMade = true;
            Reset();
            redCount = 0;
        }
        collisionCount++;
    }
}
