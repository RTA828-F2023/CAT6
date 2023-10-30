using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    protected Player playerOwner;

    #region Unity Events

    public virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    #endregion

    public virtual void Init(Player player, Vector2 direction, float force)
    {
        playerOwner = player;
        _rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
    }

    public Player GetOwner() 
    {
        return playerOwner;
    }

    public virtual void Explode()
    {
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy if object go too far offscreen
        if (other.CompareTag("DeathPit")) Destroy(gameObject);
    }
}
