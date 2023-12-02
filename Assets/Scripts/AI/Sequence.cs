using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Sequence : Node
{
    private List<Node> children = new List<Node>();
    public Sequence(List<Node> nodes)
    {
        children = nodes;
    }
    public override NodeStates Evaluate()
    {
        bool anyChildRunning = false;
        bool anyChildFailed = false;

        foreach (Node node in children)
        {
            switch (node.Evaluate())
            {
                case NodeStates.FAILURE:
                    nodeState = NodeStates.FAILURE;
                    return nodeState;
                case NodeStates.SUCCESS:
                    continue;
                case NodeStates.RUNNING:
                    anyChildRunning = true;
                    return NodeStates.RUNNING; 
                default:
                    anyChildFailed = true;
                    break;
            }
        }

        if (!anyChildRunning && anyChildFailed)
        {
            nodeState = NodeStates.FAILURE;
            return nodeState;
        }

        nodeState = NodeStates.SUCCESS;
        return nodeState;
    }
}
