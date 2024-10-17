using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;

namespace UI.checker;

public class SymbolTable
{
    public List<Ident> Table;
    public static int CurrentLevel;

    public abstract class Ident
    {
        public IToken Token { get; }
        public int Level { get; }
        public SymbolType Type { get; }
        public string Value { get; }

        public Ident(IToken t, int level, SymbolType type)
        {
            Token = t;
            Level = level;
            Type = type;
            Value = t.Text;
        }

        public abstract void Print();
    }

    public class VarIdent : Ident
    {
        public bool IsConstant { get; }

        public VarIdent(IToken t, SymbolType type, int level, bool isConstant) : base(t, level, type)
        {
            IsConstant = isConstant;
        }
        
        public override void Print()
        {
            Console.WriteLine($"Var: {Token.Text}, Type: {Type} ,Level: {Level}.");
        }
    }

    public class MethodIdent : Ident
    {
        public List<string> Params;
    
        public MethodIdent(IToken token, SymbolType type, int level, List<string> param) : base(token, level, type)
        {
            this.Params = param;
        }

        public override void Print()
        {
            Console.WriteLine($"Method: {Token.Text}, Type: {Type}, Level: {Level} , Params: {Params.Count}.");
        }
    }
    public SymbolTable()
    {
        Table = new List<Ident>(); 
        CurrentLevel = -1;
    }

    public void InsertVar(IToken t, SymbolType type)
    {
        VarIdent varIdent = new VarIdent(t, type, CurrentLevel, false);
        Table.Insert(0, varIdent);
    }

    public void InsertMethod(IToken t, SymbolType type, List<string> p)
    {
        MethodIdent methodIdent = new MethodIdent(t, type, CurrentLevel, p);
        Table.Insert(0, methodIdent);
    }

    public Ident Search(string name)
    {
        Ident temp = null;
        foreach (var i in Table)
        {
            if (i.Token.Text.Equals(name))
            {
                temp = i;
            } 
        }
        return temp;
    }

    public Ident SearchCurrentLevel(string name)
    {
        if (CurrentLevel >= 0 && CurrentLevel < Table.Count)
        {
            foreach (var i in Table)
            {
                if (i.Token.Text.Equals(name))
                {
                    return i;
                } 
            }
        }
        return null;
    }

    public Ident SearchPreviousLevel(int level ,string name)
    {
        if (level >= 0 && level < Table.Count)
        {
            foreach (var i in Table)
            {
                if (i.Token.Text.Equals(name) && i.Level == level)
                {
                    return i;
                } 
            }
        }
        return null;
    }

    public void OpenScope()
    {
        CurrentLevel++;
    }

    public void CloseScope()
    {
        Table = Table.Where(n => n.Level != CurrentLevel).ToList();
        CurrentLevel--;
    }

    public void PrintTable()
    {
        Console.WriteLine("----Starting Table----");
        for (int i = 0; i <= CurrentLevel; i++)
        {
            Console.WriteLine($"Level: {i}");
            foreach (var id in Table)
            {
                if (id.Level == i)
                {
                    id.Print();
                }
            }
        }
        Console.WriteLine("----Table Finished----");
    }
}

public enum SymbolType {
    Variable,
    Function,
    Parameter
}