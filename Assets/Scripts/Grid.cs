using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    /* 
        * Grid Specifications 
        */
   // [SerializeField]
    private int rows = Stats.boardSize;
   // [SerializeField]
    private int cols = Stats.boardSize;
    [SerializeField]
    private Vector2 gridSize;
    [SerializeField]
    private Vector2 gridOffset; // Setting grid
    /* 
     * Cell info
     */
    [SerializeField]
    private Sprite cellSprite;
    private Vector3 cellSize;
    private Vector3 cellScale;
    private GameObject[,] BoxArr;

    // Start is called before the first frame update
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
        GameObject cellObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        BoxCollider cellColider = cellObject.GetComponent<BoxCollider>();

        cellSize = cellColider.bounds.size; // Getting Colider size

        // Adjusting Cell size so it would fit in the grid
        Vector3 newCellsize = new Vector3(gridSize.x / (float)cols, gridSize.y / (float)rows, (float)10.0);

        // Setting new scales of the Cells
        cellScale.x = newCellsize.x / cellSize.x;
        cellScale.y = newCellsize.y / cellSize.y;
        cellScale.z = newCellsize.z / cellSize.z;


        cellSize = newCellsize; // From base size to new Size

        cellObject.transform.localScale = new Vector3(cellScale.x - 5, cellScale.y - 5, cellScale.z);

        // adjusting grid mapping
        gridOffset.x = -(gridSize.x / 2) + cellSize.x / 2;
        gridOffset.y = -(gridSize.y / 2) + cellSize.y / 2;




        // Instanciating all cells in grid and assigning
        BoxArr = new GameObject[rows, cols];
        for (int row = 0; row < rows; row++)
        {
            for (int colm = 0; colm < cols; colm++)
            {
                // Object position
                Vector2 pos = new Vector2(colm * cellSize.x + gridOffset.x, row * cellSize.y + gridOffset.y);

                // Copying and creating new objects
                //GameObject Cell0 = Instantiate(cellObject, pos, Quaternion.identity) as GameObject;
                
                BoxArr[row, colm] = (GameObject)Instantiate(cellObject, pos, Quaternion.identity) as GameObject;
                BoxArr[row, colm].AddComponent<MeshRenderer>();
                BoxArr[row, colm].AddComponent<ColorChanger>();

                BoxArr[row, colm].transform.parent = transform;
            }
        }
    }

    void CheckGridSize()
    {
        Gizmos.DrawWireCube(transform.position, gridSize);
    }
}

