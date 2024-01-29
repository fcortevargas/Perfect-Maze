using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Maze : MonoBehaviour
{
    // Singleton instance to ensure only one Maze exists
    public static Maze Instance { get; private set; } 
    
    // Size of the maze
    private readonly int _width = GameManager.MazeWidth;
    private readonly int _height = GameManager.MazeHeight;
    
    // Speed of maze generation (wall disabling)
    private float _speed;

    // Prefabs for the maze structure
    public GameObject wallPrefab; // Prefab for the walls
    public GameObject cellPrefab; // Prefab for the cells

    // Parent objects to organize the maze elements in the scene hierarchy
    public GameObject wallsParent; // Parent for the walls
    public GameObject removedWallsParent; // Parent for the removed walls
    public GameObject cellsParent; // Parent for the cells
    public GameObject bordersParent; // Parent for the borders

    // Maze structure representation
    private Cell[,] _cells; // Two-dimensional array of cells
    private readonly List<Wall> _walls = new List<Wall>(); // List of walls
    private readonly List<GameObject> _wallObjectsToRemove = new List<GameObject>(); // Walls to be removed during generation

    private void Awake()
    {
        // Initialize the Maze singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene loads
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Destroy the maze when the Start scene is loaded
        if (scene.name == "Start")
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize and generate the maze
        SetMazeSpeed();
        CreateCells();
        CreateBorders();
        CreateWalls();
        GenerateMaze();
    }

    // Calculate maze generation speed based on size and game settings
    private void SetMazeSpeed()
    {
        // Calculate the slope for a linear function based on the size of the maze.
        // This determines how the speed of maze generation changes relative to the maze's size.
        var slope = (1 - 1 / (_width * (float)_height)) / (1 - 100);

        // Calculate the y-intercept of the linear function, which is used to adjust the starting point of the function.
        var intercept = 1 - slope;

        // Set the maze generation speed using the linear function defined above.
        _speed = slope * GameManager.MazeSpeed + intercept;
    }

    
    // Create and position cell GameObjects
    private void CreateCells()
    {
        // Initialize the 2D array of cells with the specified width and height
        _cells = new Cell[_width, _height];

        // Loop through each cell position in the maze
        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                // Instantiate a new cell GameObject at the current position
                // The cellPrefab is cloned at the (x, y, 0.5f) position with no rotation (Quaternion.identity)
                var newCell = new Cell(x, y, Instantiate(cellPrefab, new Vector3(x, y, 0.5f), Quaternion.identity));

                // Set the parent of the new cell to cellsParent, organizing the hierarchy
                newCell.GameObject.transform.SetParent(cellsParent.transform);

                // Store the newly created cell in the _cells array for future reference
                _cells[x, y] = newCell;
            }
        }

        // After all cells are created and positioned, combine their meshes for optimization
        CombineMeshes(cellsParent);
    }

    // Create and position border GameObjects
    private void CreateBorders()
    {
        // Instantiate the left border using the wall prefab with no rotation
        var leftBorder = Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);
        // Instantiate the right border, also using the wall prefab with no rotation
        var rightBorder = Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);
        // Instantiate the top border, rotated 90 degrees on the Z-axis to align vertically
        var topBorder = Instantiate(wallPrefab, Vector3.zero, Quaternion.Euler(0, 0, 90));
        // Instantiate the bottom border, also rotated 90 degrees on the Z-axis
        var bottomBorder = Instantiate(wallPrefab, Vector3.zero, Quaternion.Euler(0, 0, 90));

        // Set the parent of the left border to bordersParent for organization
        leftBorder.transform.SetParent(bordersParent.transform);
        // Position the left border and scale it to span the height of the maze
        leftBorder.transform.position = new Vector3(-0.5f, (_height - 1) * 0.5f, 0);
        leftBorder.transform.localScale = new Vector3(0.3f, _height - 1 + 1.3f, 1);

        // Similarly, set up the right border with its parent, position, and scale
        rightBorder.transform.SetParent(bordersParent.transform);
        rightBorder.transform.position = new Vector3(-0.5f + _width, (_height - 1) * 0.5f, 0);
        rightBorder.transform.localScale = new Vector3(0.3f, _height - 1 + 1.3f, 1);

        // Set up the bottom border with its parent, position, and scale
        bottomBorder.transform.SetParent(bordersParent.transform);
        bottomBorder.transform.position = new Vector3((_width - 1) * 0.5f, -0.5f, 0);
        bottomBorder.transform.localScale = new Vector3(0.3f, _width - 1 + 1.3f, 1);

        // Finally, set up the top border with its parent, position, and scale
        topBorder.transform.SetParent(bordersParent.transform);
        topBorder.transform.position = new Vector3((_width - 1) * 0.5f, -0.5f + _height, 0);
        topBorder.transform.localScale = new Vector3(0.3f, _width - 1 + 1.3f, 1);

        // After creating and positioning all borders, combine their meshes for optimization
        CombineMeshes(bordersParent);
    }
    
    // Create and position wall GameObjects
    private void CreateWalls()
    {
        // Iterate through each cell position in the maze
        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                // Create vertical walls (between cells horizontally)
                if (x < _width - 1)
                {
                    // Instantiate a new wall between the current cell and the cell to its right
                    var newWall = new Wall(_cells[x, y], _cells[x + 1, y],
                        Instantiate(wallPrefab, new Vector3(x + 0.5f, y, 0), Quaternion.identity));

                    // Set the parent of the new wall to wallsParent for organizational purposes
                    newWall.GameObject.transform.SetParent(wallsParent.transform);

                    // Add the new wall to the list of walls
                    _walls.Add(newWall);
                }

                // Create horizontal walls (between cells vertically)
                
                if (y < _height - 1)
                {
                    // Instantiate a new wall between the current cell and the cell above it
                    var newWall = new Wall(_cells[x, y], _cells[x, y + 1],
                        Instantiate(wallPrefab, new Vector3(x, y + 0.5f, 0), Quaternion.Euler(0, 0, 90)));

                    // Set the parent of the new wall to wallsParent for organizational purposes
                    newWall.GameObject.transform.SetParent(wallsParent.transform);

                    // Add the new wall to the list of walls
                    _walls.Add(newWall);
                }
            }
        }
    }

    // Combine meshes of given parent object for optimization
    private static void CombineMeshes(GameObject parentObject)
    {
        // Retrieve all MeshFilter components from the children of the parentObject
        var meshFilters = parentObject.GetComponentsInChildren<MeshFilter>();
        var combineInstances = new List<CombineInstance>();

        // Iterate through each MeshFilter component
        foreach (var meshFilter in meshFilters)
        {
            // Check if the MeshFilter and its mesh are valid
            if (meshFilter != null && meshFilter.sharedMesh != null)
            {
                // Create a CombineInstance struct which will hold the mesh data
                var combineInstance = new CombineInstance
                {
                    mesh = meshFilter.sharedMesh,
                    transform = meshFilter.transform.localToWorldMatrix
                };
                // Add this combineInstance to the list for later use
                combineInstances.Add(combineInstance);

                // Deactivate the GameObject holding the MeshFilter component to prevent it from rendering separately
                meshFilter.gameObject.SetActive(false);
            }
        }

        // Create a new mesh which will be the combination of all the child meshes
        var combinedMesh = new Mesh
        {
            // Use a 32-bit index format to allow for a larger number of vertices
            indexFormat = UnityEngine.Rendering.IndexFormat.UInt32
        };

        // Combine all the meshes stored in combineInstances into the combinedMesh
        combinedMesh.CombineMeshes(combineInstances.ToArray(), true, true);

        // Assign the combined mesh to the MeshFilter component of the parentObject
        parentObject.GetComponent<MeshFilter>().mesh = combinedMesh;

        // Assign the combined mesh to the MeshCollider component of the parentObject
        parentObject.GetComponent<MeshCollider>().sharedMesh = combinedMesh;

        // Reactivate the parentObject to make the combined mesh visible
        parentObject.gameObject.SetActive(true);
    }

    // Main method for generating the maze structure using randomized Kruskal's algorithm
    private void GenerateMaze()
    {
        // Sort the walls based on their weight
        _walls.Sort((w1, w2) => w1.Weight.CompareTo(w2.Weight));

        // Create a disjoint set (or union-find data structure) to keep track of separate cells in the maze
        var forest = new DisjointSet(_width * _height);

        // Iterate through each wall in the sorted list.
        foreach (var wall in _walls)
        {
            // Get the cells on either side of the wall.
            var cell1 = wall.Cell1;
            var cell2 = wall.Cell2;

            // Find the set (disjoint set) for each cell.
            var set1 = forest.Find(cell1.X + cell1.Y * _width);
            var set2 = forest.Find(cell2.X + cell2.Y * _width);

            // If the cells are in different sets, it means breaking the wall won't create a loop on the maze
            if (set1 != set2)
            {
                // Merge the two sets, which creates a path between these two cells
                forest.Union(set1, set2);

                // Mark this wall to be removed to make the path between cell1 and cell2 visible
                _wallObjectsToRemove.Add(wall.GameObject);

                // Reparent the wall GameObject to keep track of walls that are removed
                wall.GameObject.transform.SetParent(removedWallsParent.transform);
            }
        }

        // After reparenting the walls that will be removed, combine the meshes of the remaining walls for optimization
        CombineMeshes(wallsParent);

        // Start the process of visually disabling the walls to animate the maze generation
        StartDisablingWalls();
    }

    // Method to reset the maze to its initial state
    public void ResetMaze()
    {
        // Iterate through each child of the removedWallsParent GameObject
        foreach (Transform child in removedWallsParent.transform)
        {
            // Reactivate each wall GameObject, effectively "rebuilding" the walls in the maze
            child.gameObject.SetActive(true);
        }
    
        // Update the game state to reflect that the maze has been reset
        GameManager.IsMazeReset = true;
        GameManager.IsMazeCompleted = false;
    }

    // Coroutine to disable walls sequentially over time
    private static IEnumerator DisableWallsSequentially(List<GameObject> wallObjects, float delayTime)
    {
        // Iterate through the list of wall GameObjects
        foreach (var wallObject in wallObjects.Where(wallObject => wallObject != null))
        {
            // Deactivate the wall GameObject, making it invisible or non-interactive in the maze
            wallObject.SetActive(false);

            // Wait for a specified delay time before proceeding to the next wall
            yield return new WaitForSeconds(delayTime);
        }

        // After all walls in the list have been disabled, the maze generation is complete
        GameManager.IsMazeCompleted = true;
    }

    // Method to start the coroutine for disabling walls
    public void StartDisablingWalls()
    {
        // Start the coroutine to disable the walls sequentially
        StartCoroutine(DisableWallsSequentially(_wallObjectsToRemove, _speed));
    
        // Update the game state to reflect that the maze is no longer in its initial reset state
        GameManager.IsMazeReset = false;
    }
}

