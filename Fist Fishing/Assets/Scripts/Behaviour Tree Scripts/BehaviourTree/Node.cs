using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
    public BehaviorTree m_tree;
    public List<Node> m_children;
    public int currentChild;

    public Node()
    {
        m_children = new List<Node>();
    }
    public Node(BehaviorTree tree, IEnumerable<Node> children) : this(tree)
    {
        foreach(Node child in children)
        {
            m_children.Add(child);
            child.m_tree = tree;
            child.Init();
        }
    }
    public Node(BehaviorTree tree) : this() { m_tree = tree; }




    public virtual NodeResult Execute()
    {
        return NodeResult.FAILURE;
    }

    public virtual bool SetChildResult(NodeResult result)
    {
        return true;
    }

    public virtual void Init()
    {
        currentChild = -1;
    }
}
