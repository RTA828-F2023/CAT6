using UnityEngine;

public class Shuriken : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float knockBackPerHit;
    [SerializeField] private int maxBounces;

    [Header("References")]
    [SerializeField] private ParticleSystem explosionPrefab;
    [SerializeField] private LineRenderer pathLine;

    private Vector2 _expectedEndPosition;

    private int _bouncesLeft;

    private Rigidbody2D _rigidbody;

    #region Unity Events

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _bouncesLeft = maxBounces;
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
        if (other.transform.CompareTag("Player"))
        {
            var player = other.transform.GetComponent<Player>();
            player.KnockBack(_rigidbody.velocity.normalized, knockBackPerHit);

            Explode();
            CameraShaker.Instance.Shake(CameraShakeMode.Light);
            return;
        }

        if (_bouncesLeft <= 0) Explode();
        _bouncesLeft--;
    }
}
