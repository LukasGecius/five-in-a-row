using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridScript : MonoBehaviour
{
// Grid specs
    private int rows = Stats.boardSize;
    private int cols = Stats.boardSize;
    [SerializeField]
    private Vector2 gridSize;
    [SerializeField]
    public Vector2 gridOffset; // Setting grid
// Cell info
    [SerializeField]
    private Sprite cellSprite;
    public Vector3 cellSize;
    private Vector3 cellScale;
    public GameObject[,] BoxArr;
    public GameObject[,] checker;
// Info to Stats
    public Rigidbody rb;
    public float mass;
    // Checker Transform
    public Transform checkerPrefab;




    void Start()
    {
        InitializeCells();

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(GameObject.Find("Sphere"));

    }



    void InitializeCells()
    {
        GameObject cellObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        SphereCollider cellColider = cellObject.GetComponent<SphereCollider>();

        GameObject checkerObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        SphereCollider checkerCollider = checkerObj.GetComponent<SphereCollider>();

        cellSize = cellColider.bounds.size; // Getting Colider size

        // Adjusting Cell size so it would fit in the grid
        Vector3 newCellsize = new Vector3(gridSize.x / (float)cols, gridSize.y / (float)rows, (float)10.0);

        // Setting new scales of the Cells
        cellScale.x = newCellsize.x / cellSize.x;
        cellScale.y = newCellsize.y / cellSize.y;
        cellScale.z = newCellsize.z / cellSize.z;
        


        cellSize = newCellsize; // From base size to new Size

        cellObject.transform.localScale = new Vector3(cellScale.x - 5, cellScale.y - 5, cellScale.z + 10);
        checkerObj.transform.localScale = new Vector3(cellScale.x / 11, cellScale.y / 11, cellScale.z / 11);

        // adjusting grid mapping
        gridOffset.x = -(gridSize.x / 2) + cellSize.x / 2;
        gridOffset.y = -(gridSize.y / 2) + cellSize.y / 2;




        // Instanciating all cells in grid and assigning
        BoxArr = new GameObject[rows, cols];
        checker = new GameObject[rows, cols];

        // Setting mass to almost 0
        mass = 0.0000000000000000001f;

        for (int row = 0; row < rows; row++)
        {
            for (int colm = 0; colm < cols; colm++)
            {
               
                
                // Object position
                 Vector3 posOfGSphere = new Vector3(colm * cellSize.x + gridOffset.x, row * cellSize.y + gridOffset.y, 0);
                 Vector3 posOfChecker = new Vector3(colm * cellSize.x + gridOffset.x, row * cellSize.y + gridOffset.y, 0 );

                // Adding shperes
                
                BoxArr[row, colm] = (GameObject)Instantiate(cellObject, posOfGSphere, Quaternion.identity) as GameObject;
                BoxArr[row, colm].name = row + " " + colm;
                BoxArr[row, colm].transform.parent = transform;
                BoxArr[row, colm].tag = "Cell";
                BoxArr[row, colm].GetComponent<Collider>().isTrigger = true;
                BoxArr[row, colm].AddComponent<Rigidbody>();
                BoxArr[row, colm].GetComponent<Rigidbody>().useGravity = false;
                BoxArr[row, colm].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

               // BoxArr[row, colm].AddComponent<Renderer>();
                BoxArr[row, colm].transform.GetComponent<Renderer>().material.color = Color.white;
                // BoxArr[row, colm].AddComponent<CollisionDetection>();
                //  BoxArr[row,colm].AddComponent<ChangeTest>();
                // BoxArr[row, colm].AddComponent<Renderer>();

                Destroy(GameObject.Find("Sphere"));// Removing excess sphere



                // Adding checkers

                if (row == 0)
                {
                    checker[row,colm] = (GameObject)Instantiate(checkerObj, posOfChecker, Quaternion.identity) as GameObject;
                    checker[row,colm].name = "Checker:" + row + "" + colm;
                    checker[row,colm].tag = "BottomCheckers";
                    checker[row,colm].layer = 9; // layer BotC

                    checker[row, colm].GetComponent<Renderer>().material.shader = Shader.Find("_Color");
                    checker[row, colm].GetComponent<Renderer>().material.SetColor("_Color", Color.green);

                    checker[row, colm].AddComponent<Rigidbody>();
                   

                //    rb = checker[row, colm].GetComponent<Rigidbody>();

                    checker[row, colm].GetComponent<Collider>().isTrigger = true;

                    //  checker[row, colm].AddComponent<AI>();

                    Destroy(GameObject.Find("Sphere"));// Removing excess sphere




                }
 
                
            }
        }
        checker[0, 0].AddComponent<RowChecker>();
        checker[0, 1].AddComponent<ColmChecker>();
        checker[0, 4].AddComponent<DiagnolCheckerL>();

        // Mover checkers, for them to work we need to reposition them. Because we recycled DangerChecker code
        checker[0, 2].GetComponent<Rigidbody>().transform.position = GameObject.Find("0 0").transform.position;
        checker[0, 2].AddComponent<MoveRow>();

        checker[0, (Stats.boardSize - 4)].GetComponent<Rigidbody>().transform.position = GameObject.Find("0 1").transform.position; // ColmCheckers position (by design)
        checker[0, (Stats.boardSize - 4)].AddComponent<MoveColm>();

        checker[0, Stats.boardSize - 3].GetComponent<Rigidbody>().transform.position = GameObject.Find(string.Format("0 {0}", Stats.boardSize - 5)).transform.position;
        checker[0, Stats.boardSize - 3].AddComponent<MoveHR>();

        checker[0, Stats.boardSize - 2].GetComponent<Rigidbody>().transform.position = GameObject.Find(string.Format("0 4")).transform.position;
        checker[0, Stats.boardSize - 2].AddComponent<MoveDL>();






        checker[0, (Stats.boardSize - 5)].AddComponent<DiagnolChecker>();
        Destroy(GameObject.Find("Sphere")); // Removing excess sphere
        Destroy(GameObject.Find("Sphere"));

    }



            void CheckGridSize()
    {
        Gizmos.DrawWireCube(transform.position, gridSize);
    }
}

