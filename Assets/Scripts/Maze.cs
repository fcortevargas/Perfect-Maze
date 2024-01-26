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
    
    // Two-dimensional array of 
    private Cell[,] _cells;

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
            }
        }
    }
}
