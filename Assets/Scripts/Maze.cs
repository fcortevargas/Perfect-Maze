using System.Collections.Generic;
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

    private void Start()
    {
        InitializeMaze();
    }
    
    private void InitializeMaze()
    {
        _cells = new Cell[width, height];

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                _cells[x, y] = new Cell(x, y, Instantiate(cellPrefab, new Vector3(x, y, 0.5f), Quaternion.identity));

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
}
