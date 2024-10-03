namespace StringSearch;
using System;
using System.Collections.Generic;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        VerifyIfConsoleIsEmpty(args);
        string searchString = args[0];

        string fixedPath = @"Your\Path\";

    public static void VerifyIfDirectoryExists(String fixedPath){
        if (!Directory.Exists(fixedPath))
        {
            Console.WriteLine($"A pasta '{fixedPath}' não existe.");
            return;
        }
    }

    public static void VerifyIfConsoleIsEmpty(string[] args){
        if (args.Length == 0)
        {
            Console.WriteLine("Por favor, forneça a string a ser pesquisada.");
            return;
        }
    }

    public static void SearchInDirectory(string directory, string searchString)
    {
        try
        {
            SearchInFilesOfDirectory(directory, searchString);
            RecursionForSubDirectories(directory,searchString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao acessar o diretório {directory}: {ex.Message}");
        }
    }

    public static void SearchInFilesOfDirectory(string directory, string searchString){
        HashSet<string> foundFiles = new HashSet<string>(); 
        foreach (string file in Directory.GetFiles(directory, "*.cs")){
                SearchInFile(file, searchString, foundFiles);
            }
    }

    public static void RecursionForSubDirectories(string directory, string searchString){
        foreach (string subDirectory in Directory.GetDirectories(directory)){
            SearchInDirectory(subDirectory, searchString);
        }
    }

    public static void SearchInFile(string filePath, string searchString, HashSet<string> foundFiles){
        try{
            string[] lines = File.ReadAllLines(filePath);
            for (int i = 0; i < lines.Length; i++){
                if (lines[i].Contains(searchString, StringComparison.OrdinalIgnoreCase)){
                    string fileName = Path.GetFileName(filePath);
                    VerifyIfFileAdded(foundFiles,fileName);
                    break; 
                }
            }
        }
        catch (Exception ex){
            Console.WriteLine($"Erro ao ler o arquivo {filePath}: {ex.Message}");
        }
    }

    public static void VerifyIfFileAdded(HashSet<string> foundFiles, string fileName){
        string FileWihtOutExtension = RemoveExtension(fileName);
        if (foundFiles.Add(FileWihtOutExtension)){
            Console.WriteLine(FileWihtOutExtension); 
        }
    }
    
    public static string RemoveExtension(string fileName)
    {
        return Path.GetFileNameWithoutExtension(fileName);
    }
}
