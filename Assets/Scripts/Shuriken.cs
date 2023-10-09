using UnityEngine;

public class Shuriken : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ParticleSystem explosionPrefab;

    private Rigidbody2D _rigidbody;

    #region Unity Events

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    #endregion

    public void Init(Vector2 direction, float force)
    {
        _rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
        _rigidbody.AddTorque(force);
    }

    private void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy if object go too far offscreen
        if (other.CompareTag("DeathPit")) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // If hit an enemy then deal damage to it
        if (other.transform.CompareTag("Enemy"))
        {
            var enemy = other.transform.GetComponent<Enemy>();
            enemy.TakeDamage(1);

            CameraShaker.Instance.Shake(CameraShakeMode.Light);
        }

        Explode();
    }
}
