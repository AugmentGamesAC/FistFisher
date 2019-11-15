using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node {
    NodeResult childResult;

   public override NodeResult Execute()
    {
        if (currentChild == -1)
        {
            currentChild++;
            m_tree.PushNode(m_children[currentChild]);
            return NodeResult.STACKED;
        }
        // if we've previously pushed a child onto the stack and we're
        // executing, then that child has completed (with either a sucess or a failure)
        if (childResult == NodeResult.FAILURE)
        {
            // we're done - report failure up the tree
            Init();
            return NodeResult.FAILURE;
        }
        else
        {
            // we got a Success.  if we have more children, push the next one.
            // if no more children, report Sucess
            currentChild++;
            if (currentChild == m_children.Count)
            {
                Init();
                return NodeResult.SUCCESS;
            }
            else
            {
                m_tree.PushNode(m_children[currentChild]);
                return NodeResult.STACKED;
            }
        }
    }

    public override Node Init()
    {
        childResult = NodeResult.UNKNOWN;
        currentChild = -1;
        if (m_children == default)
            m_children = new List<Node>();
        return this;
    }

    public override bool SetChildResult(NodeResult result)
    {
        childResult = result;
        return true;
    }
}
