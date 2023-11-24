using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisruptorPathfinding : MonoBehaviour
{
    // Works! Sort of! Follows the closest player no matter what.

    private float speed = 4f;
    private Vector2 target;
    private float distance;

    private void Start()
    {
        speed = 4f;
        //Enemy spawns outside game area, so should first enter game area
        target = new Vector2(this.transform.position.x, 2.5f);
    }

    private void Update()
    {
        //move toward target. When target is reached, pick a new target
        transform.position = Vector2.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        distance = Vector2.Distance(transform.position, target);
        if (distance <= 1f)
        {
            target = RandomPlace();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        target = RandomPlace();
    }

    private Vector2 RandomPlace()
    {
        var newTarget = new Vector2(UnityEngine.Random.Range(-9.5f, 9.5f), UnityEngine.Random.Range(-5.3f, 2.3f));
        return newTarget;
    }

   

}
