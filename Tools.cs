using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;

public static class Tools
{
    public static bool IsPrime(int n)
    {
        n = Math.Abs(n);
		
        var limit = (int) Math.Ceiling(Math.Sqrt(n));
        if (limit == n)
            limit--;
		
        for (int i = 2; i <= limit; i++)
        {
            if (n % i == 0)
                return false;
        }

        return true;
    }

    public static void WriteNodesToFile(BinaryTreeNode<int> root)
    {
        var path = Path.Join(OS.GetExecutablePath(), "../tree.res");
        using var f = File.OpenWrite(path);
        using var sw = new StreamWriter(f);
        
        List<int> list = new();
        root.ToList(ref list);
        sw.WriteLine($"{string.Join(' ', list)} 0");
    }
}