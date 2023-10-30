using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    protected Player player;

    #region Unity Events

    public virtual void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    #endregion

    public virtual void Fire()
    {

    }
}
