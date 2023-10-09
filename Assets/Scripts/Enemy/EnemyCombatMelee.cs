using UnityEngine;

public class EnemyCombatMelee : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float knockBackForce;

    private void OnCollisionEnter2D(Collision2D other)
    {
        // If collides with a player
        if (other.collider.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            if (player)
            {
                player.TakeDamage(damage);
                player.KnockBack((player.transform.position - transform.position).normalized, knockBackForce);
            }
        }
    }
}
