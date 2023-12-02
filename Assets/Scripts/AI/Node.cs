using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node
{
    public enum NodeStates { SUCCESS, RUNNING, FAILURE};
    protected NodeStates nodeState;

    public NodeStates NodeState
    {
        get { return nodeState; }
    }

    public Node() { }

    public abstract NodeStates Evaluate();
}
