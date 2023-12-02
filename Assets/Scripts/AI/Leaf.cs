using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using static Node;

public class Leaf : Node
{
    public delegate NodeStates LeafDelegate();
    private LeafDelegate leaf;

    public Leaf(LeafDelegate action)
    {
        leaf = action;
    }

    public override NodeStates Evaluate()
    {
        switch (leaf())
        {
            case NodeStates.SUCCESS:
                nodeState = NodeStates.SUCCESS;
                return nodeState;
            case NodeStates.FAILURE:
                nodeState = NodeStates.FAILURE;
                return nodeState;
            case NodeStates.RUNNING:
                nodeState = NodeStates.RUNNING;
                return nodeState;
            default:
                nodeState = NodeStates.FAILURE;
                return nodeState;
        }
    }
}

