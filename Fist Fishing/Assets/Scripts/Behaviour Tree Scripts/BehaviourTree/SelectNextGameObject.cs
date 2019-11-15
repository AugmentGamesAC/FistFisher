using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectNextGameObject : Node {
    public string ArrayKey;
    public string IndexKey;
    public string GameObjectKey;
    // Use this for initialization

    public override NodeResult Execute()
    {
        int index = (int)m_tree.GetValue(IndexKey);
        GameObject[] goa = (GameObject[])(m_tree.GetValue(ArrayKey));
        index++;
        if (index >= goa.Length)
        {
            index = 0;
        }
        m_tree.SetValue(IndexKey, index);
        m_tree.SetValue(GameObjectKey, goa[index]);
        return NodeResult.SUCCESS;
    }

    public override void Init()
    {
        GameObject[] goa = (GameObject[])(m_tree.GetValue(ArrayKey));
        m_tree.SetValue(GameObjectKey, goa[0]);
        base.Init();
    }
}
