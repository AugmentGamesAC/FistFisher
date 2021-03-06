﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait : Task {
    public string TimeToWaitKey;
    float TimeToWait;
    float elapsedTime = 0.0f;
    public override NodeResult Execute()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= TimeToWait)
        {
            Init();
            return NodeResult.SUCCESS;
        }
        else
        {
            return NodeResult.RUNNING;
        }
    }

    public override Node Init()
    {
        TimeToWait = (float)(m_tree.GetValue(TimeToWaitKey));
        elapsedTime = 0.0f;
        return base.Init();
    }
}
