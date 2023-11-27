using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private int damage;
    void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.CompareTag("Player")) {
            Player player = other.transform.GetComponent<Player>();
            player.Die();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
