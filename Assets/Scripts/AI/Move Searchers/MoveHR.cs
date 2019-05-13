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
        Reset();
    }
    int colmsChecked = 0;
    bool stopped = false;
    int hitCount = 5;
    int startHit = 5;
    int startHitB = 5;

    void Update()
    {
        if (Stats.moveCount % 2 == 0)
        {
            Reset();
        }
        if (Stats.goMoveDR == true)
        {
            stopped = false;
            // Debug.Log("Should go");
            DangerCheck();
            diagnolCheckBody.velocity = new Vector3(1050, 735, 0); // Overall thinking time depends on checker velocity Optimal: 1800, 1260
        }
       else if (Stats.goMoveDR == false)
        {
            Reset();
        }
    }
    public GameObject diagnolChecker;
    public Rigidbody diagnolCheckBody = new Rigidbody();

    void DangerCheck()
    {
        diagnolChecker = GameObject.Find(string.Format("Checker:0{0}", (Stats.boardSize - 3)));
        diagnolCheckBody = diagnolChecker.GetComponent<Rigidbody>();
        if (hitCount == 0)
        {
 ;
            if (Stats.boardSize - checkedPosX != 0)
            {
                GameObject cellToMove = new GameObject();
                checkedPosX++;

                cellToMove = GameObject.Find(string.Format("0 {0}", Stats.boardSize - checkedPosX));

                diagnolCheckBody.transform.position = cellToMove.transform.position;
                startHit++;
                hitCount = startHit;
            }
            if (Stats.boardSize - checkedPosX == 0 && stopped == false)
            {
                GameObject cellToMove = new GameObject();
                cellToMove = GameObject.Find(string.Format("{0} 0", Stats.boardSize - checkedPosY));
                diagnolCheckBody.transform.position = cellToMove.transform.position;
                checkedPosY++;
                hitCount = startHitB;
                startHitB++;

            }
        }
    }
    public void Reset()
    {
        diagnolChecker = GameObject.Find(string.Format("Checker:0{0}", (Stats.boardSize - 3)));
        
        if (diagnolCheckBody != null)
        { 
        diagnolCheckBody = diagnolChecker.GetComponent<Rigidbody>();
        diagnolCheckBody.velocity = new Vector3(0, 0, 0);
        diagnolCheckBody.transform.position = GameObject.Find(string.Format("0 {0}", (Stats.boardSize - 5))).transform.position; // Might be a sudden death, sometimes it drops value to 
        }

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
    public System.Random rnd = new System.Random();
    IEnumerator Stop()
    {
        yield return new WaitForSeconds(3);
        Application.LoadLevel(2);
    }
    public int randomPick = 0;
    public int redCount = 0;
    private void OnTriggerExit(Collider other)
    {    GameObject selectedCell = new GameObject();
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
      //      Debug.Log("No DiagnolR moves in row");
        }
        if (redCount == 3
 
        && GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 1).ToString()) != null
         && GameObject.Find(((int.Parse(other.name[0].ToString())) - 3).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 3).ToString()) != null
          && GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 1).ToString()).GetComponent<Renderer>().material.color == Color.white
           && GameObject.Find(((int.Parse(other.name[0].ToString())) - 3).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 3).ToString()).GetComponent<Renderer>().material.color == Color.white
   )
        {

            System.Random rnd = new System.Random();
            randomPick = rnd.Next(0, 2);

            if (randomPick == 0)
            {
                selectedCell = GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 1).ToString());
            }
            else
            {
                selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 3).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 3).ToString());
            }
            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;

            if (GameObject.Find((int.Parse(other.name[0].ToString()) + 2).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 2).ToString()) != null
             && GameObject.Find((int.Parse(other.name[0].ToString()) - 4).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 4).ToString()) != null)
            {
                Stats.moveCount++;
                StartCoroutine(Stop());
                Stats.moveMade = true;
            }
            else
            {
                Stats.moveCount++;
                Reset();
                Stats.moveMade = true;
            }
        }
       else if (redCount == 2
        && GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 1).ToString()) != null
         && GameObject.Find(((int.Parse(other.name[0].ToString())) - 2).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 2).ToString()) != null
          && GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 1).ToString()).GetComponent<Renderer>().material.color == Color.white
           && GameObject.Find(((int.Parse(other.name[0].ToString())) - 2).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 2).ToString()).GetComponent<Renderer>().material.color == Color.white
    )
        {

            System.Random rnd = new System.Random();
            randomPick = rnd.Next(0, 2);

            if (randomPick == 0)
            {
                selectedCell = GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 1).ToString());
            }
            else
            {
                selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 2).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 2).ToString());
            }
            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;
            //         Debug.Log("Colored: " + selectedCell.name);
            Stats.moveCount++;
            Reset();
            Stats.moveMade = true;
        } 
      else  if (redCount == 1
            && GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 1).ToString()) != null
             && GameObject.Find(((int.Parse(other.name[0].ToString())) - 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString()) != null
              && GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 1).ToString()).GetComponent<Renderer>().material.color == Color.white
               && GameObject.Find(((int.Parse(other.name[0].ToString())) - 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString()).GetComponent<Renderer>().material.color == Color.white
)
        {
            System.Random rnd = new System.Random();
            randomPick = rnd.Next(0, 2);

            if (randomPick == 0)
            {
                selectedCell = GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 1).ToString());
            }
            else
            {
                selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString());
            }
            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;

            Stats.moveCount++;
            Reset();
            Stats.moveMade = true;
        } // Performing OK move
    }
}
