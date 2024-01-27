using UnityEngine;

public class Cell
{
    public readonly int X;
    public readonly int Y;
    public GameObject GameObject;

    public Cell(int x, int y, GameObject gameObject)
    {
        X = x;
        Y = y;
        GameObject = gameObject;
    }
}