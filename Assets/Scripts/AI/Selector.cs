using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Selector : Node
{
    private List<Node> children = new List<Node>();
    public Selector(List<Node> nodes)
    {
        children = nodes;
    }
    public override NodeStates Evaluate()
    {
        foreach (Node node in children)
        {
            switch (node.Evaluate())
            {
                case NodeStates.FAILURE:
                    continue;
                case NodeStates.SUCCESS:
                    nodeState = NodeStates.SUCCESS;
                    return nodeState;
                case NodeStates.RUNNING:
                    nodeState = NodeStates.RUNNING;
                    return nodeState;
                default:
                    continue;
            }
        }
        nodeState = NodeStates.FAILURE;
        return nodeState;
    }
}
