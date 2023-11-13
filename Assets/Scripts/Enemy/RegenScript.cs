using UnityEngine;

public class RegenScript : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public int baseHealth = 3;
    [SerializeField] private int scoreValue = 100;

    [Header("Prefabs")]
    [SerializeField] private ParticleSystem explosionPrefab;

    private int _currentHealth;
    private float _healRate = 2.0f;
    private float _nextHeal = 0.0f;

    #region Unity Events

    private void Start()
    {
        _currentHealth = baseHealth;
    }

    private void Update()
    {
        //heal at a regular 2s interval
        if (Time.time > _nextHeal)
        {
            if (_currentHealth < baseHealth)
            {
                _currentHealth++;
            }
            _nextHeal = Time.time + _healRate;
        }
    }

    #endregion

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
