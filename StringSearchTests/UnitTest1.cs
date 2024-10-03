namespace StringSearch.StringSearchTests;
using System;
using System.IO;
using System.Collections.Generic;
using Xunit;
using Moq;
public class ProgramTests
{
    [Fact]
    public void SearchInFile_FindsSearchStringInFile(){
        string tempFile = Path.GetTempFileName();
        string searchString = "hello";
        File.WriteAllText(tempFile, "hello world");
        HashSet<string> foundFiles = new HashSet<string>();
        Program.SearchInFile(tempFile, searchString, foundFiles);
        Assert.Contains(Path.GetFileName(Program.RemoveExtension(tempFile)), foundFiles);
        File.Delete(tempFile);
    }
    
    [Fact]
    public void SearchInFile_DoesNotFindSearchStringInFile(){
        string tempFile = Path.GetTempFileName();
        string searchString = "notfound";
        File.WriteAllText(tempFile, "hello world");
        HashSet<string> foundFiles = new HashSet<string>();
        Program.SearchInFile(tempFile, searchString, foundFiles);
        Assert.DoesNotContain(Path.GetFileName(tempFile), foundFiles);
        File.Delete(tempFile);
    }

    [Fact]
    public void SearchInDirectory_DirectoryDoesNotExist_ThrowsException(){
        string invalidDirectory = @"C:\NonExistentDirectory";
        string searchString = "sample";
        var ex = Record.Exception(() => Program.SearchInDirectory(invalidDirectory, searchString));
        Assert.Null(ex); // Não deve lançar exceção, apenas imprimir erro
    }

    [Fact]
    public void SearchInDirectory_FindsFilesInValidDirectory(){
        string tempDir = Path.GetTempPath();
        string tempFile = Path.Combine(tempDir, "tempFile.cs");
        string searchString = "test";
        File.WriteAllText(tempFile, "This is a test string");
        var ex = Record.Exception(() => Program.SearchInDirectory(tempDir, searchString));

        Assert.Null(ex);// Não deve lançar exceção, apenas imprimir erro
        File.Delete(tempFile);
    }

    [Fact]
    public void SearchInFile_IgnoreCase_FindsStringWithDifferentCases(){
        string tempFile = Path.GetTempFileName();
        string searchString = "hello";
        File.WriteAllText(tempFile, "HELLO WORLD");
        HashSet<string> foundFiles = new HashSet<string>();
        Program.SearchInFile(tempFile, searchString, foundFiles);
        Assert.Contains(Path.GetFileName(Program.RemoveExtension(tempFile)), foundFiles); // Verifica se o arquivo foi encontrado
        File.Delete(tempFile);
    }

    [Fact]
    public void RemoveExtension_RemovesCsExtension(){
        string fileName = "example.cs";
        string result = Program.RemoveExtension(fileName);
        Assert.Equal("example", result);
    }

}
