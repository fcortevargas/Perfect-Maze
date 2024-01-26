using UnityEngine;

public class Cell
{
    public int X, Y;
    public GameObject CellObject;

    public Cell(int x, int y, GameObject gameObject)
    {
        X = x;
        Y = y;
        CellObject = gameObject;
    }
}