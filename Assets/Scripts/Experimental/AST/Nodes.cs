using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node { }

public class CommandNode : Node
{
    public string Command { get; }
    public Node Target { get; }

    public CommandNode(string command, Node target)
    {
        Command = command;
        Target = target;
    }
}

public class TargetNode : Node
{
    public string Name { get; }
    public TargetNode(string name)
    {
        Name = name;
    }
}

public class AssignmentNode : Node
{
    public string Lhs { get; }
    public string Op1 { get; }   // 变量名或字面量
    public string Operator { get; } // "+", "-", "*", "/" 或 null
    public string Op2 { get; }   // 变量名或字面量 或 null

    public AssignmentNode(string lhs, string op1, string op, string op2)
    {
        Lhs = lhs;
        Op1 = op1;
        Operator = op;
        Op2 = op2;
    }
}

