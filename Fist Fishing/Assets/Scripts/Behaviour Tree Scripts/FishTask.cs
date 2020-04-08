using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base class for all tasks for fish AI
/// contains some data about the fish that child tasks may make use of
/// </summary>
public class FishTask : Node
{
    protected float m_speed;
    protected float m_turnSpeed;
    protected float m_accuracy;
    [SerializeField]
    protected GameObject m_target;
    protected GameObject m_me;
    protected float m_biteCooldown;


    protected void ReadInfo()
    {
        m_me = m_tree.parent;
        m_target = (GameObject)m_tree.GetValue(FishBrain.TargetName);
        m_speed = (float)m_tree.GetValue(FishBrain.SpeedName); // should, like targetname, pass the variable names in.
        m_turnSpeed = (float)m_tree.GetValue(FishBrain.TurnSpeedName);
        m_accuracy = (float)m_tree.GetValue(FishBrain.AccuracyName);
        m_biteCooldown = (float)m_tree.GetValue(FishBrain.BiteCooldownName);
    }
}
