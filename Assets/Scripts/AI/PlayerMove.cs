using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour
{
    // Player Selected Cell name
    private string selColName;
    // AI Selection
    private GameObject pickAI;
    public Renderer pickAIbody = new Renderer();
    public string firstPosCellName;
    bool firstMove;
    bool secondMove;
    bool started = false;
    // Random
    public System.Random rnd = new System.Random();
    public bool goCheckers = false;
    
    
    void Start()
    {
        firstMove = true;
        secondMove = false;
        
    }
    void SearchForDanger()
    {
        Stats.goCheckerDL = true;
        Stats.goCheckerColm = true;
        Stats.goCheckerDR = true;
        Stats.goCheckerRow = true;
        Stats.dangerNotFound = false;
    }
    void SmartMove()
    {
        Stats.goMoveRow = true;
        Stats.goMoveCol = true;
        Stats.goMoveDL = true;
        Stats.goMoveDR = true;
      //  Debug.Log("Fired");
    }
    // Update is called once per frame
    void Update()
    {
        if (Stats.moveCount % 2 == 0) // PLAYER MOVE
        {
            Text text;
            text = GameObject.Find("MoveText").GetComponent<Text>();

            text.text = "It is for you to move!";

            
            

            Stats.goMoveCol = false;
            Stats.goMoveDL = false;
            Stats.goMoveDR = false;
            Stats.goMoveRow = false;
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
 // prevension of playing sound on start
                if (Physics.Raycast(ray, out hitInfo, 1000))
                {
                    AudioSource popAudio = GameObject.Find("PopSoundPlayer").GetComponent<AudioSource>();
                    popAudio.Play();
                    //        print(hitInfo.collider.gameObject.name);
                    hitInfo.transform.GetComponent<Renderer>().material.color = Color.blue;
                    selColName = hitInfo.collider.gameObject.name;
                    Stats.moveCount++;
                    Stats.dangerNotFound = true;
                }
            }
        }
        if (Stats.moveCount % 2 == 1) // Computer move
        {
            Text text;
            text = GameObject.Find("MoveText").GetComponent<Text>();

            text.text = "Computer is thinking (but he's not very bright yet)";

            if (firstMove == true) // First computer move
            {
                int posX = rnd.Next(-1, 2);
                int posY = rnd.Next(-1, 2);
                if (selColName[0].ToString() == "0") // If in first row, place on top
                {
                    posY = 0;
                    posX = 1;
                }
                if ((int.Parse(selColName[0].ToString()) == Stats.boardSize - 1)) // If in last row, place below
                {
                    posY = 0;
                    posX = -1;
                }
                if (selColName[2].ToString() == "0") // If in first colm, place right
                {
                    posX = 0;
                    posY = 1;
                }
                if ((int.Parse(selColName[2].ToString()) == Stats.boardSize - 1)) // If in last Colm, place left
                {
                    posX = 0;
                    posY = -1;
                }
                while (posX == 0 && posY == 0)
                {
                    posY = rnd.Next(-1, 2);
                }
                firstPosCellName = ((int.Parse(selColName[0].ToString()) + posX).ToString() + " " + (int.Parse(selColName[2].ToString()) + posY).ToString());
                
                pickAI = GameObject.Find(firstPosCellName);
                pickAIbody = pickAI.GetComponent<Renderer>();
                pickAIbody.material.color = Color.red;
                firstMove = false;
                secondMove = true;
                Stats.moveCount++;
            }
            else if (secondMove == true)
            {
             //   Debug.Log("picking around: " + firstPosCellName);
                int posXX = rnd.Next(-1, 2);
                int posYY = rnd.Next(-1, 1);
                while (posXX == 0 && posYY == 0)
                {
                    posXX = rnd.Next(-1, 2);
                    posYY = rnd.Next(-1, 2);
                }
                
                // Selecting new position ^
                pickAI = GameObject.Find((int.Parse(firstPosCellName[0].ToString()) + posXX).ToString() + " " + ((int.Parse(firstPosCellName[2].ToString()) + posYY).ToString())   );
                pickAIbody = pickAI.GetComponent<Renderer>();
                // Taking that position object ^
                if (pickAIbody.material.color != Color.blue) // if not blue, make blue
                {
                    pickAIbody.material.color = Color.red;
                    Stats.moveCount++;
                    secondMove = false;
              //      Debug.Log("Moving colm - " + posXX + " Moving row - " + posYY);
            //        Debug.Log("pick was not blue");
                } // if not blue, make blue
                else
                {
               //     Debug.Log(" Pick was blue"); // Update() shoots the program again, and if it's still second move, it will recalculate and pick another Cell
                }
            }
            else if (firstMove == false && secondMove == false)
            {
                if (Stats.dangerNotFound == true)
                {
                    SearchForDanger();
                }
                if (Stats.goCheckerDL == false && Stats.goCheckerColm == false && Stats.goCheckerDR == false)
                {
                //    Stats.moveCount++;
                    SmartMove();
                 //   Stats.dangerNotFound = false;
                }
             //   CheckForMoves();
            }
        }
    }
}
