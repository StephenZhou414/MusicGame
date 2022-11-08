using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPath : MonoBehaviour
{

    private Transform[] waypoints;
    private LineRenderer lr;

    private float speed;

    private int waypointIndex = 1;
    private GameObject player;
    private bool followPath = false;

    public void PaintPath()
    {
        waypoints = transform.GetComponentsInChildren<Transform>();
        lr = GetComponent<LineRenderer>();
        lr.positionCount = waypoints.Length - 1;
        for (int i = 1; i < waypoints.Length; i++)
        {
            lr.SetPosition(i - 1, waypoints[i].position);
        }
    }

    public void BeginPath(GameObject p, int speed)
    {
        player = p;
        this.speed = speed;
        
        player.transform.position = waypoints[waypointIndex].position;
        followPath = true;

    }

    public Transform GetLastPoint()
    {
        return waypoints[waypoints.Length - 1];
    }

    // Update is called once per frame
    private void Update()
    {

        if(followPath)
            Move();
    }

    // Method that actually make Enemy walk
    private void Move()
    {
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position,
               waypoints[waypointIndex].transform.position,
               speed * Time.deltaTime);

            if (player.transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex++;
            }
        }
        else
        {
            followPath = false;
            //trigger event
            Level.current.LastPointOfPath();
        }
    }
}
