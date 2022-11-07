using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPath : MonoBehaviour
{

    private Transform[] waypoints;
    private LineRenderer lr;

    [SerializeField]
    private float moveSpeed = 2f;

    private int waypointIndex = 1;
    private GameObject player;
    private bool followPath = false;
    private void Start()
    {
        waypoints = transform.GetComponentsInChildren<Transform>();
        lr = GetComponent<LineRenderer>();
        lr.positionCount = waypoints.Length - 1;
        for (int i = 1; i < waypoints.Length; i++)
        {
            Debug.Log(waypoints[i].gameObject.name);
            lr.SetPosition(i-1, waypoints[i].position);
        }
        
    }

    public void BeginPath(GameObject p)
    {
        player = p;
        Debug.Log("Initial pos: " + waypoints[waypointIndex].transform.position);
        player.transform.position = waypoints[waypointIndex].transform.position;
        Debug.Log("Player pos: " + player.transform.position);
        followPath = true;

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
               moveSpeed * Time.deltaTime);

            if (player.transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex++;
            }
        }
        else
            followPath = false;
    }
}
