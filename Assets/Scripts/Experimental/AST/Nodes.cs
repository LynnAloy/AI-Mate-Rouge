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

public class AssignmentIntNode : Node
{
    public string Lhs { get; }
    public int Value { get; }

    public AssignmentIntNode(string lhs, int value)
    {
        Lhs = lhs;
        Value = value;
    }
}

public class AssignmentBoolNode : Node
{
    public string Lhs { get; }
    public bool Value { get; }
    public AssignmentBoolNode(string lhs, bool value)
    {
        Lhs = lhs;
        Value = value;
    }
}

