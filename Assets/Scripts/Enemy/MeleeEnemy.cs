using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public int baseHealth = 2;
    [SerializeField] private int scoreValue = 100;

    [Header("Prefabs")]
    [SerializeField] private ParticleSystem explosionPrefab;

    protected int _currentHealth;

    #region Unity Events

    private void Start()
    {
        _currentHealth = baseHealth;
    }

    #endregion

    public virtual void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0) Die();
    }

    public int GetScore() 
    {
        return scoreValue;
    }

    private void Die()
    {
        // Check win condition on enemy death
        GameController.Instance.StartCoroutine(GameController.Instance.CheckWinCondition());
        // Check wave end condition on enemy death
        GameController.Instance.StartCoroutine(GameController.Instance.CheckWaveEnd());

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
