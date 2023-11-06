using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    protected Player player;

    [SerializeField] private float fireRecoveryTime;
    private bool _canFire = true;

    #region Unity Events

    public virtual void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    #endregion

    public virtual bool Fire()
    {
        if (!_canFire) return false;

        // Down time before weapon can be fired again
        _canFire = false;
        StartCoroutine(RecoverFire(fireRecoveryTime));

        return true;
    }

    private IEnumerator RecoverFire(float time)
    {
        yield return new WaitForSeconds(time);
        _canFire = true;
    }

}
