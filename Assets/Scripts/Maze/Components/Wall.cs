using UnityEngine;

// The Wall class represents a wall in a maze
public class Wall
{
    // Cell1 and Cell2 represent the two cells that this wall separates
    public readonly Cell Cell1; 
    public readonly Cell Cell2; 

    // Weight of the wall, used in the maze generation algorithm
    public readonly float Weight;

    // The GameObject associated with this wall
    public readonly GameObject GameObject;

    // Constructor to initialize a Wall instance
    public Wall(Cell c1, Cell c2, GameObject gameObject)
    {
        Cell1 = c1; // Assign the first adjacent cell
        Cell2 = c2; // Assign the second adjacent cell
        Weight = Random.Range(0f, 1f); // Assign a random weight to the wall for maze generation algorithms
        GameObject = gameObject; // Assign the Unity GameObject that represents this wall visually
    }
}