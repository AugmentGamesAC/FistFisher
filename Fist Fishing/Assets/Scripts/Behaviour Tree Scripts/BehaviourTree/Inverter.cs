using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : Node {
    NodeResult childResult;
    public override NodeResult Execute()
    {
        if (childResult == NodeResult.UNKNOWN)
        {
            m_tree.PushNode(m_children[0]);
            return NodeResult.STACKED;
        }
        if (childResult == NodeResult.SUCCESS)
        {
            return NodeResult.FAILURE;
        }
        if (childResult == NodeResult.FAILURE)
        {
            return NodeResult.SUCCESS;
        }
        return NodeResult.FAILURE; // should never get here
    }

    public override Node Init()
    {
        childResult = NodeResult.UNKNOWN;
        return base.Init();
    }

    public override bool SetChildResult(NodeResult result)
    {
        childResult = result;
        return true;
    }
}
