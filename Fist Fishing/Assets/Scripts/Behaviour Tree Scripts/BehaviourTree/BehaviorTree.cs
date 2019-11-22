using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree : MonoBehaviour {
    protected Stack<Node> CallStack;
    public Hashtable Blackboard;
    public Node root;
    public GameObject parent;
    // Use this for initialization
    public virtual void Awake()
    {
        parent = gameObject;
        CallStack = new Stack<Node>();
        Blackboard = new Hashtable();
        // build the tree by walking the children and well, building the data structure.
        // we need to do this recursively
        // along the way, whatever we find first needs to be the root of the tree.
        // to simplify, require that the top level always only have one thing.
        GameObject r = FindRootNode();
        BuildTree(r);
    }
    public static BehaviorTree GetTreeNamed(GameObject obj, string treename)
    {
        BehaviorTree[] trees = obj.GetComponentsInChildren<BehaviorTree>();
        foreach (BehaviorTree tree in trees)
        {
            if (tree.name == treename)
            {
                Debug.Log("Found tree");
                return tree;
            }
        }
        Debug.Log("Did not find tree");
        return null;
    }
    GameObject FindRootNode()
    {
        foreach (Transform child in gameObject.transform)
        {
            // look for a Node type in it.
            Node n = child.gameObject.GetComponent<Node>();
            if (n != null)
            {
                // we can stop - we found one
                root = n;
                return child.gameObject;
            }
        }
        Debug.LogError("No nodes found directly under the tree");
        return null;
    }
    void BuildTree(GameObject obj)
    {
        Debug.Log("Building tree");
        // obj has a node.  find everything under it and connect it up.
        Node n = obj.GetComponent<Node>();
        n.m_tree = this;
        foreach (Transform child in obj.transform)
        {
            Node cn = child.gameObject.GetComponent<Node>();
            if (cn != null)
            {
                n.m_children.Add(cn);
                BuildTree(child.gameObject);
            }
        }
    }

    // Update is called once per frame
    public virtual void Update() {
        RunStack();
    }

    public void AddKey(string key)
    {
        if (Blackboard.ContainsKey(key) == false)
        {
            Blackboard.Add(key, null);
        }
    }

    public object GetValue(string key)
    {
        if (Blackboard.ContainsKey(key))
        {
            return Blackboard[key];
        }
        else
        {
            return null;
        }
    }

    public void SetValue(string key, object value)
    {
        Blackboard[key] = value;
    }

    public void PushNode(Node node)
    {
        node.Init();
        node.m_tree = this;
        CallStack.Push(node);
    }

    public void PushNodeWithOutReset(Node node)
    {
        node.m_tree = this;
        CallStack.Push(node);
    }
    public void PopTop()
    {
        CallStack.Pop();
    }

    public NodeResult RunStack()
    {
        if (CallStack.Count == 0)
        {
            // stack is empty - add to it
            PushNode(root);
        }

        Node top = CallStack.Peek();
        NodeResult result = top.Execute();
        switch (result)
        {
            case NodeResult.FAILURE:
            case NodeResult.SUCCESS:
                CallStack.Pop(); // remove this node
                if (CallStack.Count == 0)
                {
                    return result;
                }
                Node parent = CallStack.Peek();
                bool runstack = parent.SetChildResult(result);
                if (runstack == true)
                {
                    return RunStack(); // and let the parent node continue
                }
                else
                {
                    return result;
                }
            case NodeResult.RUNNING:
                return result; // we do not need to do anything in this case.
                        // we will continue with this node in the next frame.
            case NodeResult.STACKED:
                return RunStack(); // let the newly added child node have some CPU
                ;
            default:
                return result;
        }
    }
}
