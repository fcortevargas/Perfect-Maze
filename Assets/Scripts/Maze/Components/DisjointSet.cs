public class DisjointSet
{
    private readonly int[] _parent;

    public DisjointSet(int count)
    {
        _parent = new int[count];
        for (var i = 0; i < count; i++)
        {
            _parent[i] = i;
        }
    }

    public int Find(int i)
    {
        while (i != _parent[i])
        {
            i = _parent[i];
        }
        
        return i;
    }

    public void Union(int x, int y)
    {
        var xRoot = Find(x);
        var yRoot = Find(y);
        _parent[xRoot] = yRoot;
    }
}
