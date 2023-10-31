using UnityEngine;

public class Bomb : Projectile
{
    public int damage;
    public float radius;
    public float blastForce;

    [Header("References")]
    [SerializeField] private ParticleSystem explosionPrefab;

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
            var enemiesHits = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("Enemies"));
            foreach (var enemyHit in enemiesHits)
            {
                var direction = (enemyHit.transform.position - transform.position).normalized;
                var enemy = enemyHit.GetComponent<Enemy>();

                enemy.TakeDamage(damage);
                enemyHit.GetComponent<Rigidbody2D>().AddForce(direction * blastForce, ForceMode2D.Impulse);

                playerOwner.UpdateScore(enemy.GetScore());
            }

            //PointSystemController.UpdatePlayerScore(player.type, enemy.GetScore());
            CameraShaker.Instance.Shake(CameraShakeMode.Light);
        }

        Explode();
    }
}
