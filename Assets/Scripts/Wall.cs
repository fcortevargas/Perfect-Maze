using UnityEngine;

public class Wall
{
    public Cell cell1;
    public Cell cell2;
    public float weight;
    public GameObject wallObject;

    public Wall(Cell c1, Cell c2, GameObject gameObject)
    {
        cell1 = c1;
        cell2 = c2;
        wallObject = gameObject;
    }
}