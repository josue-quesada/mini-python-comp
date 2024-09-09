using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace UI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public async void OpenFile(object source, RoutedEventArgs args)
    {
        var openFileDialog = new OpenFileDialog
        {
            Title = "Open Mini-Python File",
            Filters = new List<FileDialogFilter>
            {
                new FileDialogFilter { Name = "Mini-Python Files", Extensions = new List<string> { "txt" } },
                new FileDialogFilter { Name = "All Files", Extensions = new List<string> { "*" } }
            }
        };

        var result = await openFileDialog.ShowAsync(this);
        if (result != null)
        {
            string filePath = result[0];
            string fileContent = File.ReadAllText(filePath);
        
            CodeTextBox.Text = fileContent;
            SaveLastFilePath(filePath);
        }
    }
    
    public void SaveLastFilePath(string filePath)
    {
        var config = new Dictionary<string, string>
        {
            { "LastFilePath", filePath }
        };

        string jsonString = JsonSerializer.Serialize(config);

        string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string saveFilePath = Path.Combine(appDirectory, "src", "lastFile.json");

        Console.WriteLine(appDirectory, saveFilePath);
        // Write the JSON content to the file
       // File.WriteAllText(saveFilePath, jsonString);
    }

    public string GetLastOpenedFilePath()
    {
        string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(appDirectory, "src", "lastFile.json");
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText("src/lastFile.json");
            var config = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);

            if (config.ContainsKey("LastFilePath"))
            {
                return config["LastFilePath"];
            }
        }

        return null;
    }
    public void OpenLastFile(object source, RoutedEventArgs args)
    {
        string lastFilePath = GetLastOpenedFilePath();

        if (!string.IsNullOrEmpty(lastFilePath))
        {
            string fileContent = CodeTextBox.Text;
        }
        else
        {
            CodeTextBox.Text = "There isn't a last file to open";
        }
    }

    public void print(object source, RoutedEventArgs args)
    {
        Console.WriteLine("hola");
    }

}