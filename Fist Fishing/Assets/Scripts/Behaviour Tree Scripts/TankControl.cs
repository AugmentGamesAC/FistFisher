using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : MonoBehaviour
{
    public GameObject waypointPrefab;
    public GameObject TankPrefab;
    public GameObject[] waypoints;
    public float radius = 15.0f;
    // Use this for initialization
    void Start()
    {
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
    void Update()
    {
        if (Input.GetKeyDown("2"))
        {
            GameObject t = Instantiate(TankPrefab, waypoints[0].transform.position, Quaternion.identity);
            BehaviorTree Patrol = BehaviorTree.GetTreeNamed(t,"Patrol");
            Patrol.SetValue("Speed", Random.Range(3.0f,6.0f));
            Patrol.SetValue("TurnSpeed", 2.0f);
            Patrol.SetValue("Accuracy", 0.5f);
            Patrol.SetValue("Waypoints", waypoints);
            Patrol.SetValue("Waypoint", waypoints[0]);
            Patrol.SetValue("Index", 0);
            Patrol.SetValue("PauseAtWaypoint", 0.25f);

            BehaviorTree Turret = BehaviorTree.GetTreeNamed(t, "SpinTurret");
            Turret.SetValue("TurnSpeed", 180.0f);
        }
    }
}
