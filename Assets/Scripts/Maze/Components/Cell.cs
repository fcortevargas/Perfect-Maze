using UnityEngine;

// The Cell class represents an individual cell in a maze
public class Cell
{
    // X and Y are the coordinates of the cell in the maze grid
    public readonly int X;
    public readonly int Y;

    // GameObject associated with this cell
    public GameObject GameObject;

    // Constructor to initialize a Cell instance
    public Cell(int x, int y, GameObject gameObject)
    {
        X = x; 
        Y = y;
        GameObject = gameObject;
    }
}