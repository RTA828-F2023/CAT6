using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float knockBackPerHit;
    [SerializeField] private int damage;

    [Header("References")]
    [SerializeField] private ParticleSystem explosionPrefab;
    [SerializeField] private LineRenderer pathLine;

    private Vector2 _expectedEndPosition;

    private Rigidbody2D _rigidbody;

    #region Unity Events

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        _expectedEndPosition = Physics2D.Raycast(transform.position, _rigidbody.velocity.normalized).point;
        pathLine.SetPosition(0, transform.position);
        pathLine.SetPosition(1, _expectedEndPosition);
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
        if (other.CompareTag("DeathPit")) Explode();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.collider, true);
        }

        if (other.transform.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }

        Explode();
    }
}
