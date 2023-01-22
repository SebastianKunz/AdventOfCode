namespace AdventOfCode;

public class AdventFile
{
    public AdventDirectory Parent { get; }
    public int Size { get; }
    
    public string Name { get; }

    public AdventFile(AdventDirectory parent, int size, string name)
    {
        Parent = parent;
        Size = size;
        Name = name;
    }
}