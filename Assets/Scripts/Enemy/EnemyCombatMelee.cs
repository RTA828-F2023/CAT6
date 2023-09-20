using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatMelee : MonoBehaviour
{
    [SerializeField] private int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //If collides with an Enemy
        if (other.collider.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage); 
            }
        }
    }
}
