// DisjointSet is a data structure that implements the union-find algorithm
public class DisjointSet
{
    // Array to hold the parent of each element in the disjoint set
    private readonly int[] _parent;

    // Constructor to initialize the disjoint set
    public DisjointSet(int count)
    {
        // Initialize the _parent array with the specified number of elements (count)
        _parent = new int[count];

        // Initially, each element is its own parent, forming 'count' number of singleton sets
        for (var i = 0; i < count; i++)
        {
            _parent[i] = i;
        }
    }

    // Find method to find the root (or representative) of the set
    public int Find(int i)
    {
        // Follow the chain of parent pointers until the root is reached
        while (i != _parent[i])
        {
            i = _parent[i];
        }
        
        return i; 
    }

    // Union method to merge two sets
    public void Union(int x, int y)
    {
        // Find the roots of the sets that x and y belong to
        var xRoot = Find(x);
        var yRoot = Find(y);

        // Make the root of x's set point to the root of y's set, effectively merging the two sets
        _parent[xRoot] = yRoot;
    }
}