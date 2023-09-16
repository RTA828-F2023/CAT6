using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private ParticleSystem explosionPrefab;

    private int _currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Die()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //If collides with an Enemy
        if (other.collider.CompareTag("Shuriken"))
        {
            _currentHealth--;

            Destroy(other.gameObject);
            if (_currentHealth <= 0) Die();
        }
    }
}
