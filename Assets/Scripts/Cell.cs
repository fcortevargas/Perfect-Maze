using UnityEngine;

public class Cell
{
    public readonly int X;
    public readonly int Y;
    public GameObject CellObject;

    public Cell(int x, int y, GameObject gameObject)
    {
        X = x;
        Y = y;
        CellObject = gameObject;
    }
}