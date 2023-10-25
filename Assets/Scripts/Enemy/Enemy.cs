using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int baseHealth = 1;
    [SerializeField] private int scoreValue = 100;

    [Header("Prefabs")]
    [SerializeField] private ParticleSystem explosionPrefab;

    private int _currentHealth;

    #region Unity Events

    private void Start()
    {
        _currentHealth = baseHealth;

        InvokeRepeating(nameof(FollowPlayer1), 0f, 0.5f);
    }

    #endregion

    private void FollowPlayer1()
    {
        GetComponent<EnemyPathfinding2>().Track(FindObjectsOfType<Player>()[0]?.transform);
    }

    public void TakeDamage(int damage)
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
