using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
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
    private GameObject[,] BoxArr;
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
               // BoxArr[row, colm].AddComponent<CollisionDetection>();
                 //  BoxArr[row,colm].AddComponent<ChangeTest>();
                // BoxArr[row, colm].AddComponent<Renderer>();





                // Adding checkers

                if (row == 0)
                {
                    checker[row,colm] = (GameObject)Instantiate(checkerObj, posOfChecker, Quaternion.identity) as GameObject;
                    checker[row,colm].name = "Checker:" + row + "" + colm;
                  //  checker[row, colm].tag = "BottomCheckers";

                    checker[row, colm].GetComponent<Renderer>().material.shader = Shader.Find("_Color");
                    checker[row, colm].GetComponent<Renderer>().material.SetColor("_Color", Color.green);

                    checker[row, colm].AddComponent<Rigidbody>();
                   

                //    rb = checker[row, colm].GetComponent<Rigidbody>();

                    checker[row, colm].GetComponent<Collider>().isTrigger = true;

                    checker[row, colm].AddComponent<CollisionDetection>();






                }
                if (colm == 0 && row !=0 )
                {
                    checker[row, colm] = (GameObject)Instantiate(checkerObj, posOfChecker, Quaternion.identity) as GameObject;
                    checker[row, colm].name = "Checker:" + row + "" + colm;
                   // checker[row, colm].tag = "Colm0Checkers";

                    checker[row, colm].GetComponent<Renderer>().material.shader = Shader.Find("_Color");
                    checker[row, colm].GetComponent<Renderer>().material.SetColor("_Color", Color.red);

                    checker[row, colm].AddComponent<Rigidbody>();


                    checker[row, colm].GetComponent<Collider>().isTrigger = true;


                    checker[row, colm].AddComponent<CollisionDetection>();



                }
                if ((colm + 1) == cols && row != 0)
                {
                    checker[row, colm] = (GameObject)Instantiate(checkerObj, posOfChecker, Quaternion.identity) as GameObject;
                    checker[row, colm].name = "Checker:" + row + "" + colm;
                   // checker[row, colm].tag = "LastColCheckers";

                    checker[row, colm].GetComponent<Renderer>().material.shader = Shader.Find("_Color");
                    checker[row, colm].GetComponent<Renderer>().material.SetColor("_Color", Color.gray);

                    checker[row, colm].AddComponent<Rigidbody>();

                    checker[row, colm].GetComponent<Collider>().isTrigger = true;


                    checker[row, colm].AddComponent<CollisionDetection>();

                }

                // Checker ignore Collision
                GameObject[] BottomCheckersObj = GameObject.FindGameObjectsWithTag("BottomCheckers");

                for (int i = 0; i < BottomCheckersObj.Length; i++)
                {
                    Physics.IgnoreCollision(BottomCheckersObj[i].GetComponent<Collider>(), BoxArr[row, colm].GetComponent<Collider>());
                }

                GameObject[] Colm0Checkers = GameObject.FindGameObjectsWithTag("Colm0Checkers");

                for (int i = 0; i < Colm0Checkers.Length; i++)
                {
                    Physics.IgnoreCollision(Colm0Checkers[i].GetComponent<Collider>(), BoxArr[row, colm].GetComponent<Collider>());
                }

                GameObject[] LastColCheckers = GameObject.FindGameObjectsWithTag("LastColCheckers");

                for (int i = 0; i < LastColCheckers.Length; i++)
                {
                    Physics.IgnoreCollision(LastColCheckers[i].GetComponent<Collider>(), BoxArr[row, colm].GetComponent<Collider>());
                }

            }
        }
        Destroy(GameObject.Find("Sphere")); // Removing excess sphere
    }



            void CheckGridSize()
    {
        Gizmos.DrawWireCube(transform.position, gridSize);
    }
}

