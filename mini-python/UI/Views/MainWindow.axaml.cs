using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Avalonia.Controls;
using Avalonia.Interactivity;
using generated;
using UI.checker;
using UI.Imports;

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
        string projectRoot = Directory.GetParent(appDirectory).Parent.Parent.Parent.FullName;
        string saveFilePath = Path.Combine(projectRoot, "src", "lastFile.json");
        File.WriteAllText(saveFilePath, jsonString);
    }

    public string GetLastOpenedFilePath()
    {
        string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string projectRoot = Directory.GetParent(appDirectory).Parent.Parent.Parent.FullName;
        string filePath = Path.Combine(projectRoot, "src", "lastFile.json");
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
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
            string fileContent = File.ReadAllText(lastFilePath);
            CodeTextBox.Text = fileContent;
        }
        else
        {
            CodeTextBox.Text = "There isn't a last file to open";
        }
    }
    public void test(object source, RoutedEventArgs args)
    {
        if (CodeTextBox.Text != null || CodeTextBox.Text == "")
        {
            string input = CodeTextBox.Text;
            Console.WriteLine(input);
            ICharStream stream = CharStreams.fromString(input);
            MPLexer lexer = new MPLexer(stream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            MPParser parser = new MPParser(tokens);
            
            ErrorListener errorListener = new ErrorListener();
            ContextAnalyzer contextAnalyzer = new ContextAnalyzer();
            
            lexer.RemoveErrorListeners();
            parser.RemoveErrorListeners();
            lexer.AddErrorListener(errorListener);
            parser.AddErrorListener(errorListener);
            IParseTree tree = parser.program();
            try
            {
                if (errorListener.HasErrors())
                {
                    ErrorTextBlock.Text = errorListener.ToString();
                }
                else
                {
                    ErrorTextBlock.Text = "Compilation complete \nNo errors found, starting context analyzer.";
                    contextAnalyzer.Visit(tree);
                    if (contextAnalyzer.HasErrors())
                    {
                        ErrorTextBlock.Text = "Context analyzer failed.";
                    }
                    else
                    {
                        ErrorTextBlock.Text = "Context analyzer completed successfully.";
                    }
                }
            }
            catch (NullReferenceException  ex)
            {
                Console.WriteLine("Error!");
            }
            
        }
        
    }
}