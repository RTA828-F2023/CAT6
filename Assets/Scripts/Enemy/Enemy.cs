using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour  {

    public GameObject player;
    public float speed;
    private float distance;
    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Launch();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Launch() {
        float x = 1;
        float y = 1;
        body.velocity = new Vector2(speed * x, speed * y);
    }
}
