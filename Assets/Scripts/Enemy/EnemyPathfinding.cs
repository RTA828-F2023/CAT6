using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    // Works! Sort of! Follows the closest player no matter what.

    public GameObject[] Players;
    public float speed;
    private float distance;

    private void Start()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
    }

    private void Update()
    {
        GameObject target = closestPlayer();
        distance = Vector2.Distance(transform.position, target.transform.position);
        Vector2 direction = target.transform.position - transform.position;
        transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private GameObject closestPlayer()
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

    /*
     * Past here: needs a lot of work
     * For now: Just learn to get the enemies to follow arrays of Vector2's
    private void attack()
    {

    }

    private void move()
    {
        if (dist(target, (x, y)) < viewRadius){
            onPath = false;
            moveTowards((target.x, target.y));
        }
        else
        {
            if (!onPath)
            {
                nextPathPoint = nearestPathPoint();
                onPath = true;
            }
            moveTowards(currentRoom.path[nextPathPoint]);
            if ((x, y) == currentRoom.path[nextPathPoint])
                nextPathPoint = (nextPathPoint++) % currentRoom.path.length();
            //just remember - if path[0] is always the doors, skip it when finding the next point
            //  if (nextPathPoint == 0) nextPathPoint = 1;
        }
    }

    /*

    private Player lowestHpPlayer()
    {
        int weakest = 0;
        int health = 100000;
        for (int i = 0; i < players.length(); i++)
        {
            if (players[i].health < health)
            {
                weakest = i;
                health = players[i].health;
            }
        }
        return players[weakest];
    }

    private void moveTowards((int x, int y))
    {

    }

    private int nearestPathPoint()
    {
        int closest = 0;
        int distance = 100000;
        for (int i = 0; i < currentRoom.path.length(); i++)
        {
            if (dist(currentRoom.path[i], self) < distance)
            {
                closest = i;
                distance = dist(currentRoom.path[i], self);
            }
        }
        return currentRoom.path[closest];
    }
    */
}
