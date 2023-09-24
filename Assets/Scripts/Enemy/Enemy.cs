using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private int enemyScoreValue = 1;


    [Header("Prefabs")]
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
        //If collides with a Shuriken
        if (other.collider.CompareTag("Shuriken"))
        {
            Shuriken projectile = other.gameObject.GetComponent<Shuriken>();
            Player player = projectile.GetOwner().GetComponent<Player>();

            player.UpdatePlayerScore(enemyScoreValue);
            _currentHealth--;

            Destroy(other.gameObject);
            if (_currentHealth <= 0) Die();
        }
    }
}
