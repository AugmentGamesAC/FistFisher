using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTask : Node
{
    protected float m_speed;
    protected float m_turnSpeed;
    protected float m_accuracy;
    [SerializeField]
    protected GameObject m_target;
    protected GameObject m_me;



    protected void ReadInfo()
    {
        m_me = m_tree.parent;
        m_target = (GameObject)m_tree.GetValue(FishBrain.TargetName);
        m_speed = (float)m_tree.GetValue(FishBrain.SpeedName); // should, like targetname, pass the variable names in.
        m_turnSpeed = (float)m_tree.GetValue(FishBrain.TurnSpeedName);
        m_accuracy = (float)m_tree.GetValue(FishBrain.AccuracyName);
    }
}
