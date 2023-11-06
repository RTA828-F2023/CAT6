using UnityEngine;

public class Inkblob : Projectile
{
    [Header("Stats")]
    public int damage;
    public float blastForce;
    public float range;
    public int pierce;

    [Header("References")]
    [SerializeField] private ParticleSystem explosionPrefab;

    private Vector2 _initPosition;
    private int _pierceCount;

    #region Unity Events

    private void Start()
    {
        _initPosition = transform.position;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _initPosition) >= range) Explode();
    }

    #endregion

    public override void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        base.Explode();
    }

    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        // If hit an enemy then deal damage to it
        if (other.transform.CompareTag("Enemy"))
        {
            var enemy = other.transform.GetComponent<Enemy>();
            enemy.TakeDamage(damage);

            var direction = (enemy.transform.position - transform.position).normalized;
            enemy.GetComponent<Rigidbody2D>().AddForce(direction * blastForce, ForceMode2D.Impulse);
            playerOwner.UpdateScore(enemy.GetScore());

            // TODO: Implement blast radius
            // var enemiesHits = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("Enemies"));
            // foreach (var enemyHit in enemiesHits)
            // {
            //     var direction = (enemyHit.transform.position - transform.position).normalized;
            //     var enemy = enemyHit.GetComponent<Enemy>();

            //     enemy.TakeDamage(damage);
            //     enemyHit.GetComponent<Rigidbody2D>().AddForce(direction * blastForce, ForceMode2D.Impulse);

            //     playerOwner.UpdateScore(enemy.GetScore());
            // }

            //PointSystemController.UpdatePlayerScore(player.type, enemy.GetScore());
            CameraShaker.Instance.Shake(CameraShakeMode.Light);

            _pierceCount++;
            if (_pierceCount >= pierce) Explode();
        }
        else
        {
            Explode();
        }
    }
}
