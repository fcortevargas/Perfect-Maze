using UnityEngine;

public class Wall
{
    public Cell Cell1;
    public Cell Cell2;
    public float Weight;
    public GameObject GameObject;

    public Wall(Cell c1, Cell c2, GameObject gameObject)
    {
        Cell1 = c1;
        Cell2 = c2;
        Weight = Random.Range(0f, 1f);
        GameObject = gameObject;
    }
}