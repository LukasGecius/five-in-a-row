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
        if (Stats.goMoveDL == true)
        {
            stopped = false;
            DangerCheck();
                diagnolCheckBody.velocity = new Vector3(-1050, 735, 0);
        }
        else if (Stats.goMoveDL == false)
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
        diagnolChecker = GameObject.Find(string.Format("Checker:0{0}", Stats.boardSize - 2));

        if (diagnolCheckBody != null)
        { 
        diagnolCheckBody = diagnolChecker.GetComponent<Rigidbody>();
        diagnolCheckBody.velocity = new Vector3(0, 0, 0);
        diagnolCheckBody.transform.position = GameObject.Find("0 4").transform.position;
        }

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
    public int redCount = 0;

    private void OnTriggerExit(Collider other)
    {

        GameObject selectedCell = new GameObject();
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

        }
        if (redCount == 3
            && GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString()) != null
            && GameObject.Find(((int.Parse(other.name[0].ToString())) - 3).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 3).ToString()) != null
                 && GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString()).GetComponent<Renderer>().material.color == Color.white
                 && GameObject.Find(((int.Parse(other.name[0].ToString())) - 3).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 3).ToString()).GetComponent<Renderer>().material.color == Color.white
   )
        {

            System.Random rnd = new System.Random();
            randomPick = rnd.Next(0, 2);

            if (randomPick == 0)
            {
                selectedCell = GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString());
            }
            else
            {
                selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 3).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 3).ToString());
            }
            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;

            if (GameObject.Find((int.Parse(other.name[0].ToString()) + 2).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 2).ToString()) != null
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
        }// If redCount 3 - win
       else if (redCount == 2
            && GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString()) != null
             && GameObject.Find(((int.Parse(other.name[0].ToString())) - 2).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 2).ToString()) != null
              && GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString()).GetComponent<Renderer>().material.color == Color.white
               && GameObject.Find(((int.Parse(other.name[0].ToString())) - 2).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 2).ToString()).GetComponent<Renderer>().material.color == Color.white
    )
        {

            System.Random rnd = new System.Random();
            randomPick = rnd.Next(0, 2);

            if (randomPick == 0)
            {
                selectedCell = GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString());
            }
            else
            {
                selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 2).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 2).ToString());
            }
            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;

            Stats.moveCount++;
            Reset();
        } // Performing Good move\
       else if (redCount == 1
            && GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1)) != null
            && GameObject.Find(((int.Parse(other.name[0].ToString())) - 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 3).ToString()) != null
    && GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString()).GetComponent<Renderer>().material.color == Color.white
    && GameObject.Find(((int.Parse(other.name[0].ToString())) - 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 3).ToString()).GetComponent<Renderer>().material.color == Color.white
)
        {

            System.Random rnd = new System.Random();
            randomPick = rnd.Next(0, 2);

            if (randomPick == 0)
            {
                selectedCell = GameObject.Find((int.Parse(other.name[0].ToString()) + 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) - 1).ToString());
            }
            else
            {
                selectedCell = GameObject.Find(((int.Parse(other.name[0].ToString())) - 1).ToString() + " " + ((int.Parse(other.name[2].ToString())) + 1).ToString());
            }
            selectedCell.transform.GetComponent<Renderer>().material.color = Color.red;

            Stats.moveCount++;
            Reset();
            Stats.moveMade = true;
        } // Performing OK move
    }
}
