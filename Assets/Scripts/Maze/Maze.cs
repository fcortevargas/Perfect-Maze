using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Maze : MonoBehaviour
{
    // Create a singleton instance
    public static Maze Instance { get; private set; } 
    
    // Width of the maze
    private readonly int _width = GameManager.MazeWidth;
    // Height of the maze
    private readonly int _height = GameManager.MazeHeight;
    // How fast the maze will generate
    private float _speed;

    // Prefab for the visual representation of the walls of the maze
    public GameObject wallPrefab;
    // Prefab for the visual representation of the cells of the maze
    public GameObject cellPrefab;
    
    // Parent game object for the walls
    public GameObject wallsParent;
    // Parent game object for the removed walls
    public GameObject removedWallsParent;
    // Parent game object for the cells
    public GameObject cellsParent;
    // Parent game object for the borders
    public GameObject bordersParent;

    // Two-dimensional array of cell objects
    private Cell[,] _cells;
    // List of wall objects
    private readonly List<Wall> _walls = new();
    // List of wall game objects that will be removed
    private readonly List<GameObject> _wallObjectsToRemove = new();

    private void Awake()
    {
        // Singleton pattern to ensure only one GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Start")
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetMazeSpeed();
        CreateCells();
        CreateBorders();
        CreateWalls();
        GenerateMaze();
    }

    void SetMazeSpeed()
    {
        var slope = (1 - 1 / (_width * (float)_height)) / (1 - 100);
        var intercept = 1 - slope;
        _speed = slope * GameManager.MazeSpeed + intercept;
    }
    
    private void CreateCells()
    {
        _cells = new Cell[_width, _height];

        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                var newCell = new Cell(x, y, Instantiate(cellPrefab, new Vector3(x, y, 0.5f), Quaternion.identity));
                newCell.GameObject.transform.SetParent(cellsParent.transform);
                _cells[x, y] = newCell;
            }
        }

        CombineMeshes(cellsParent);
    }

    private void CreateBorders()
    {
        var leftBorder = Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);
        var rightBorder = Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);
        var topBorder = Instantiate(wallPrefab, Vector3.zero, Quaternion.Euler(0, 0, 90));
        var bottomBorder = Instantiate(wallPrefab, Vector3.zero, Quaternion.Euler(0, 0, 90));
    
        leftBorder.transform.SetParent(bordersParent.transform);
        leftBorder.transform.position = new Vector3(-0.5f, (_height - 1) * 0.5f, 0);
        leftBorder.transform.localScale = new Vector3(0.3f, _height - 1 + 1.3f, 1);
    
        rightBorder.transform.SetParent(bordersParent.transform);
        rightBorder.transform.position = new Vector3(-0.5f + _width, (_height - 1) * 0.5f, 0);
        rightBorder.transform.localScale = new Vector3(0.3f, _height - 1 + 1.3f, 1);
    
        bottomBorder.transform.SetParent(bordersParent.transform);
        bottomBorder.transform.position = new Vector3((_width - 1) * 0.5f, -0.5f, 0);
        bottomBorder.transform.localScale = new Vector3(0.3f, _width - 1 + 1.3f, 1);
    
        topBorder.transform.SetParent(bordersParent.transform);
        topBorder.transform.position = new Vector3((_width - 1) * 0.5f, -0.5f + _height, 0);
        topBorder.transform.localScale = new Vector3(0.3f, _width - 1 + 1.3f, 1);
        
        CombineMeshes(bordersParent);
    }

    private void CreateWalls()
    {
        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                if (x < _width - 1)
                {
                    var newWall = new Wall(_cells[x, y], _cells[x + 1, y],
                        Instantiate(wallPrefab, new Vector3(x + 0.5f, y, 0), Quaternion.identity));
                    newWall.GameObject.transform.SetParent(wallsParent.transform);
                    _walls.Add(newWall);
                }

                if (y < _height - 1)
                {
                    var newWall = new Wall(_cells[x, y], _cells[x, y + 1],
                        Instantiate(wallPrefab, new Vector3(x, y + 0.5f, 0), Quaternion.Euler(0, 0, 90)));
                    newWall.GameObject.transform.SetParent(wallsParent.transform);
                    _walls.Add(newWall);
                }
            }
        }
    }

    private static void CombineMeshes(GameObject parentObject)
    {
        var meshFilters = parentObject.GetComponentsInChildren<MeshFilter>();
        var combineInstances = new List<CombineInstance>();

        foreach (var meshFilter in meshFilters)
        {
            if (meshFilter != null && meshFilter.sharedMesh != null)
            {
                var combineInstance = new CombineInstance
                {
                    mesh = meshFilter.sharedMesh,
                    transform = meshFilter.transform.localToWorldMatrix
                };
                combineInstances.Add(combineInstance);
                meshFilter.gameObject.SetActive(false);
            }
        }

        var combinedMesh = new Mesh
        {
            indexFormat = UnityEngine.Rendering.IndexFormat.UInt32
        };
        combinedMesh.CombineMeshes(combineInstances.ToArray(), true, true);
        parentObject.GetComponent<MeshFilter>().mesh = combinedMesh;
        parentObject.GetComponent<MeshCollider>().sharedMesh = combinedMesh;
        parentObject.gameObject.SetActive(true);
    }

    private void GenerateMaze()
    {
        _walls.Sort((w1, w2) => w1.Weight.CompareTo(w2.Weight));

        var forest = new DisjointSet(_width * _height);
        foreach (var wall in _walls)
        {
            var cell1 = wall.Cell1;
            var cell2 = wall.Cell2;

            var set1 = forest.Find(cell1.X + cell1.Y * _width);
            var set2 = forest.Find(cell2.X + cell2.Y * _width);

            if (set1 != set2)
            {
                forest.Union(set1, set2);
                _wallObjectsToRemove.Add(wall.GameObject);
                wall.GameObject.transform.SetParent(removedWallsParent.transform);
            }
        }
        
        CombineMeshes(wallsParent);

        StartDisablingWalls();
    }

    public void ResetMaze()
    {
        foreach (Transform child in removedWallsParent.transform)
        {
            child.gameObject.SetActive(true);
        }
        
        GameManager.IsMazeReset = true;
        GameManager.IsMazeCompleted = false;
    }
    
    private static IEnumerator DisableWallsSequentially(List<GameObject> wallObjects, float delayTime)
    {
        foreach (var wallObject in wallObjects.Where(wallObject => wallObject != null))
        {
            wallObject.SetActive(false);
            yield return new WaitForSeconds(delayTime);
        }

        GameManager.IsMazeCompleted = true;
    }

    public void StartDisablingWalls()
    {
        StartCoroutine(DisableWallsSequentially(_wallObjectsToRemove, _speed));
        
        GameManager.IsMazeReset = false;
    }
}

