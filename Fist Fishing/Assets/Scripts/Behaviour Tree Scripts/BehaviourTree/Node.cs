using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
    public BehaviorTree m_tree;
    public List<Node> m_children;
    public int currentChild;

    virtual public Node Init()
    {
        m_children = new List<Node>();
        currentChild = -1;
        return this;
    }
    public Node Init(BehaviorTree tree, IEnumerable<Node> children)
    {
        Init(tree);
        foreach(Node child in children)
        {
            m_children.Add(child);
            child.m_tree = tree;
            child.Init();
        }
        return this;
    }
    public Node Init(BehaviorTree tree){ Init(); m_tree = tree; return this; }




    public virtual NodeResult Execute()
    {
        return NodeResult.FAILURE;
    }

    public virtual bool SetChildResult(NodeResult result)
    {
        return true;
    }
}
