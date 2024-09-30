using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Por favor, forneça a string a ser pesquisada.");
            return;
        }

        string searchString = args[0];

        string fixedPath = @"C:\KB\SescNet_Develop1\Data099\web\"; 

        if (!Directory.Exists(fixedPath))
        {
            Console.WriteLine($"A pasta '{fixedPath}' não existe.");
            return;
        }
        
        Console.WriteLine($"Procurando por '{searchString}' em todos os arquivos .cs na pasta '{fixedPath}'...\n");

        SearchInDirectory(fixedPath, searchString);
    }

    static void SearchInDirectory(string directory, string searchString)
    {
        HashSet<string> foundFiles = new HashSet<string>(); 

        try
        {
            foreach (string file in Directory.GetFiles(directory, "*.cs"))
            {
                SearchInFile(file, searchString, foundFiles);
            }

            foreach (string subDirectory in Directory.GetDirectories(directory))
            {
                SearchInDirectory(subDirectory, searchString);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao acessar o diretório {directory}: {ex.Message}");
        }
    }

    static void SearchInFile(string filePath, string searchString, HashSet<string> foundFiles)
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(searchString, StringComparison.OrdinalIgnoreCase))
                {
                    string fileName = Path.GetFileName(filePath);
                    if (foundFiles.Add(fileName)) 
                    {
                        Console.WriteLine(fileName); 
                    }
                    break; 
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao ler o arquivo {filePath}: {ex.Message}");
        }
    }
}
