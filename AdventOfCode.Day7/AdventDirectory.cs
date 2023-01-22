namespace AdventOfCode;

public class AdventDirectory
{
    public AdventDirectory? Parent { get; }
    private Dictionary<string, AdventFile> Files { get; }
    
    public Dictionary<string, AdventDirectory> Dirs { get; }

    public int Size => Dirs.Sum(x => x.Value.Size) + Files.Sum(x => x.Value.Size);
    
    public string Name { get; }

    public AdventDirectory(string name, AdventDirectory? parent =null)
    {
        Name = name;
        Parent = parent;
        Files = new Dictionary<string, AdventFile>();
        Dirs = new Dictionary<string, AdventDirectory>();
    }

    public void AddDir(string name)
    {
        Dirs.Add(name, new AdventDirectory(name, this));
    }

    public void AddFile(int size, string name)
    {
        Files.Add(name, new AdventFile(this, size, name));
    }

    public AdventDirectory GetDir(string name)
    {
        return Dirs[name];
    }
    
    
}