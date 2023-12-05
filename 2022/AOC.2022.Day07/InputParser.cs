namespace AdventOfCode;

public static class InputParser
{
    public static async Task<AdventDirectory> Parse(string path)
    {
        AdventDirectory directory = null;
        AdventDirectory root = null;
        CommandType? lastCommand = null;
        await foreach (string line in File.ReadLinesAsync(path))
        {
            string[] split = line.Split(" ");
            LineType type = ParseLineType(split[0]);
            if (type == LineType.Command)
            {
                lastCommand = ParseCommandType(split[1]);

                if (lastCommand == CommandType.Cd)
                {
                    string dirName = split[2];

                    if (dirName == "/")
                    {
                        directory = new AdventDirectory("/");
                        root = directory;
                    }
                    else if (dirName == "..")
                    {
                        directory = directory.Parent;
                    }
                    else
                    {
                        directory = directory.GetDir(dirName);
                    }
                }
            }
            else if (type == LineType.Dir)
            {
                string dirName = split[1];
                directory.AddDir(dirName);
            }
            else if (type == LineType.File)
            {
                int fileSize = int.Parse(split[0]);
                string name = split[1];

                directory.AddFile(fileSize, name);
            }
        }

        return root;
    }

    private static CommandType ParseCommandType(string type)
    {
        if (type == "cd")
        {
            return CommandType.Cd;
        }

        if (type == "ls")
        {
            return CommandType.Ls;
        }

        throw new ArgumentOutOfRangeException(type);
    }

    private static LineType ParseLineType(string type)
    {
        if (type == "$")
        {
            return LineType.Command;
        }

        if (type == "dir")
        {
            return LineType.Dir;
        }

        if (int.TryParse(type, out _))
        {
            return LineType.File;
        }

        throw new ArgumentOutOfRangeException(type);
    }
}