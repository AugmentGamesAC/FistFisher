﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node {
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
        if (childResult == NodeResult.SUCCESS)
        {
            // we're done - report success up the tree
            Init();
            return NodeResult.SUCCESS;
        }
        else
        {
            // we got a failure.  if we have more children, push the next one.
            // if no more children, report failure
            currentChild++;
            if (currentChild == m_children.Count)
            {
                Init();
                return NodeResult.FAILURE;
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
        return base.Init();
    }

    public override bool SetChildResult(NodeResult result)
    {
        childResult = result;
        return true;
    }
}
