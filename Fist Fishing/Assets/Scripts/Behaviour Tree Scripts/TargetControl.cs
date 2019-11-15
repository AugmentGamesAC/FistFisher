using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetControl : MonoBehaviour {
    public GameObject waypointPrefab;
    public GameObject TargetPrefab;
    public GameObject[] waypoints;
    public float radius = 10.0f;
	// Use this for initialization
	void Start () {
        waypoints = new GameObject[20];
        for (int i = 0; i < 20; i++)
        {
            float x = Mathf.Cos(i * 18 * Mathf.Deg2Rad) * radius;
            float z = Mathf.Sin(i * 18 * Mathf.Deg2Rad) * radius;
            Vector3 position = new Vector3(x, 0, z);
            waypoints[i] = Instantiate(waypointPrefab, position, Quaternion.identity);
            // spawn waypoints
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("1"))
        {
            GameObject t = Instantiate(TargetPrefab, waypoints[0].transform.position,Quaternion.identity);
            BehaviorTree Patrol = BehaviorTree.GetTreeNamed(t, "Patrol");
            Patrol.SetValue("Speed", 5.0f);
            Patrol.SetValue("TurnSpeed", 2.0f);
            Patrol.SetValue("Accuracy", 0.5f);
            Patrol.SetValue("Waypoints", waypoints);
            Patrol.SetValue("Waypoint", waypoints[0]);
            Patrol.SetValue("Index", 0);
            Patrol.SetValue("PauseAtWaypoint", 1.0f);
        }
	}
}
