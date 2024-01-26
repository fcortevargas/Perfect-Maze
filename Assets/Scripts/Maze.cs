using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Maze : MonoBehaviour
{
    // Width of the maze
    public int width = 10;
    // Height of the maze
    public int height = 10;
    
    // Prefab for the visual representation of the walls of the maze
    public GameObject wallPrefab;
    // Prefab for the visual representation of the cells of the maze
    public GameObject cellPrefab;
    
    // Two-dimensional array of cell objects
    private Cell[,] _cells;
    // List of wall objects
    private List<Wall> _walls = new List<Wall>();

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Start()
    {
        MoveCamera();
        CreateCells();
        CreateBorders();
        CreateWalls();
        GenerateMaze();
    }

    private void MoveCamera()
    {
        var x = (float)width;
        var y = (float)height;
        _mainCamera.gameObject.transform.position = new Vector3(x / 2, y / 2, -10);
        
        if (_mainCamera.aspect >= x / y)
        {
            _mainCamera.orthographicSize = y / 2 + 5;
        }
        else
        {
            var differenceInSize = x / y / _mainCamera.aspect;
            _mainCamera.orthographicSize = y / 2 * differenceInSize + 5;
        }
    }
    
    private void CreateCells()
    {
        _cells = new Cell[width, height];

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                _cells[x, y] = new Cell(x, y, Instantiate(cellPrefab, new Vector3(x, y, 0.5f), Quaternion.identity));
            }
        }
    }

    private void CreateBorders()
    {
        var leftBorder = Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);
        var rightBorder = Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);
        var topBorder = Instantiate(wallPrefab, Vector3.zero, Quaternion.Euler(0, 0, 90));
        var bottomBorder = Instantiate(wallPrefab, Vector3.zero, Quaternion.Euler(0, 0, 90));
        
        leftBorder.transform.position = new Vector3(-0.5f, (height - 1) * 0.5f, 0);
        leftBorder.transform.localScale = new Vector3(0.2f, height - 1 + 1.2f, 1);
        
        rightBorder.transform.position = new Vector3(-0.5f + width, (height - 1) * 0.5f, 0);
        rightBorder.transform.localScale = new Vector3(0.2f, height - 1 + 1.2f, 1);
        
        bottomBorder.transform.position = new Vector3((width - 1) * 0.5f, -0.5f, 0);
        bottomBorder.transform.localScale = new Vector3(0.2f, width - 1 + 1.2f, 1);
        
        topBorder.transform.position = new Vector3((width - 1) * 0.5f, -0.5f + height, 0);
        topBorder.transform.localScale = new Vector3(0.2f, width - 1 + 1.2f, 1);
    }

    private void CreateWalls()
    {
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                if (x < width - 1)
                {
                    _walls.Add(new Wall(_cells[x, y], _cells[x + 1, y], Instantiate(wallPrefab, new Vector3(x + 0.5f, y, 0), Quaternion.identity)));
                }

                if (y < height - 1)
                {
                    _walls.Add(new Wall(_cells[x, y], _cells[x, y + 1], Instantiate(wallPrefab, new Vector3(x, y + 0.5f, 0), Quaternion.Euler(0, 0, 90))));
                }
            }
        }
    }
    
    private void GenerateMaze()
    {
        _walls.Sort((w1, w2) => w1.Weight.CompareTo(w2.Weight));

        var forest = new DisjointSet(width * height);
        foreach (var wall in _walls)
        {
            Cell cell1 = wall.Cell1;
            Cell cell2 = wall.Cell2;

            var set1 = forest.Find(cell1.X + cell1.Y * width);
            var set2 = forest.Find(cell2.X + cell2.Y * width);

            if (set1 != set2)
            {
                forest.Union(set1, set2);
                Destroy(wall.WallObject);
            }
        }
    }
}
