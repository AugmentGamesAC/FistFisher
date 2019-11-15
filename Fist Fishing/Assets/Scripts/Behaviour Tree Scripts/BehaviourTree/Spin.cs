using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : Task {
    public string TurnSpeedName;
    float TurnSpeed;
    float currentRotationAmount = 0.0f;
    
	// Use this for initialization
    public override void Init () {
        TurnSpeed = (float)(tree.GetValue(TurnSpeedName));
        currentRotationAmount = 0.0f;

    }
	
	// Update is called once per frame
	public override NodeResult Execute () {
        currentRotationAmount += TurnSpeed * Time.deltaTime;
        if (currentRotationAmount > 360)
        {
            tree.parent.transform.Rotate(Vector3.up, 0, Space.Self);
            return NodeResult.SUCCESS;
        }
        else
        {
            tree.parent.transform.Rotate(Vector3.up, TurnSpeed * Time.deltaTime, Space.Self);
        }
        return NodeResult.RUNNING;
	}
}
