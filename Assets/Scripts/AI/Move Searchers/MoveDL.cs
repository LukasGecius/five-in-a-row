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


        if (hitCount == 0)
        {
            GameObject cellToMove = new GameObject();
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


    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(3);
        Application.LoadLevel(2);
    }

    public void Reset()
    {
        diagnolCheckBody.velocity = new Vector3(0, 0, 0);
        diagnolCheckBody.transform.position = GameObject.Find("0 4").transform.position;
        stopped = true;
     //   Debug.Log("MoverReset");

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

    public System.Random rnd = new System.Random();

    public int randomPick = 0;

    GameObject selectedCell = new GameObject();
    public int redCount = 0;

    private void OnTriggerExit(Collider other)
    {
        hitCount--;
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
            Debug.Log("No DiagnolL moves in row");
        }
        if (redCount == 3
                 && GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString()).GetComponent<Renderer>().material.color == Color.white
                 && GameObject.Find(((int.Parse(other.name[0].ToString())) - 3).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 3).ToString()).GetComponent<Renderer>().material.color == Color.white
   )
        {
            Debug.Log("Performing winning move");
            System.Random rnd = new System.Random();
            randomPick = rnd.Next(0, 2);
        //    Debug.Log("option nr: " + randomPick);

            if (randomPick == 0)
            {
                selectedCell = GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString());

            }
            else
            {
                selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 3).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 3).ToString());
            }

            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;
         //   Debug.Log("Colored: " + selectedCell.name);

            if (GameObject.Find((int.Parse(other.name[0].ToString()) + 2).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 2).ToString()) != null
             && GameObject.Find((int.Parse(other.name[0].ToString()) - 4).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 4).ToString()) != null)
            {
            StartCoroutine(Stop());
            }
            else
            {
                Reset();
                Stats.moveCount++;
            }



        }// If redCount 3 - win

        if (redCount == 2
    && GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString()).GetComponent<Renderer>().material.color == Color.white
    && GameObject.Find(((int.Parse(other.name[0].ToString())) - 2).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 2).ToString()).GetComponent<Renderer>().material.color == Color.white
    )
        {
         //   Debug.Log("Performing good move");
            System.Random rnd = new System.Random();
            randomPick = rnd.Next(0, 2);
      //      Debug.Log("option nr: " + randomPick);

            if (randomPick == 0)
            {
                selectedCell = GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString());

            }
            else
            {
                selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 2).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 2).ToString());
            }

            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;
        //    Debug.Log("Colored: " + selectedCell.name);
            Reset();
            Stats.moveCount++;
        } // Performing Good move\

        if (redCount == 1
    && GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString()).GetComponent<Renderer>().material.color == Color.white
    && GameObject.Find(((int.Parse(other.name[0].ToString())) - 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 3).ToString()).GetComponent<Renderer>().material.color == Color.white
)
        {
         //   Debug.Log("Performing Ok move");
            System.Random rnd = new System.Random();
            randomPick = rnd.Next(0, 2);
          //  Debug.Log("option nr: " + randomPick);

            if (randomPick == 0)
            {
                selectedCell = GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString());

            }
            else
            {
                selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 1).ToString());
            }

            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;
        //    Debug.Log("Colored: " + selectedCell.name);
            Reset();
            Stats.moveCount++;
        } // Performing OK move

    }
}
