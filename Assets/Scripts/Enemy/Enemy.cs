using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int baseHealth = 1;

    [Header("Prefabs")]
    [SerializeField] private ParticleSystem explosionPrefab;

    private int _currentHealth;

    #region Unity Events

    private void Start()
    {
        _currentHealth = baseHealth;
    }

    #endregion

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0) Die();
    }

    private void Die()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
