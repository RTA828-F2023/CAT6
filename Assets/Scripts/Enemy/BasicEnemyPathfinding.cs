using System;
using System.IO;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyPathfinding : MonoBehaviour
{
    // Works! Sort of! Follows the closest player no matter what.

    private GameObject[] Players;
    private float speed = 2f;
    private float distance;
    private float viewRadius;

    private Level level;
    private Vector2[] PatrolRoute;
    private Boolean onPath = true;
    private int nextPathPoint = 0;
    private GameObject target;
    
    private void Start()
    {
        speed = 2f;
        viewRadius = 3f;
        Players = GameObject.FindGameObjectsWithTag("Player");
        level = GetComponentInParent<Level>();
        PatrolRoute = level.patrolRoute;
    }

    private void Update()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        if (Players.Length > 0) {
            target = ClosestPlayer();
            distance = Vector2.Distance(transform.position, target.transform.position);
            //Vector2 direction = target.transform.position - transform.position;
            //transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
            Move();
        }
    }

    private GameObject ClosestPlayer()
    {
        distance = Vector2.Distance(transform.position, Players[0].transform.position);
        GameObject closest = Players[0];
        for (int i = 1; i < Players.Length; i++)
        {
            if (Vector2.Distance(transform.position, Players[i].transform.position) < distance)
            {
                distance = Vector2.Distance(transform.position, Players[i].transform.position);
                closest = Players[i];
            }
        }
        return closest;
    }

    private int NearestPathPoint()
    {
        distance = Vector2.Distance(transform.position, PatrolRoute[0]);
        int closest = 0;
        for (int i = 1; i < PatrolRoute.Length; i++)
        {
            if (Vector2.Distance(transform.position, PatrolRoute[i]) < distance)
            {
                distance = Vector2.Distance(transform.position, PatrolRoute[i]);
                closest = i;
            }
        }
        return closest;
    }

    private void Move()
    {
        distance = Vector2.Distance(transform.position, ClosestPlayer().transform.position);
        //if player in view, leave path, move toward target
        if (distance < viewRadius)
        {
            onPath = false;
            transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else
        //else join, move towards next path point
        {
            if (!onPath)
            {
                nextPathPoint = NearestPathPoint();
                onPath = true;
            }
            transform.position = Vector2.MoveTowards(this.transform.position, PatrolRoute[nextPathPoint], speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, PatrolRoute[nextPathPoint]) <= 1f)
            {
                nextPathPoint = (nextPathPoint + 1) % PatrolRoute.Length;
            }
            //just remember - if path[0] is always the doors, skip it when finding the next point
            //  if (nextPathPoint == 0) nextPathPoint = 1;
        }
    }
}
