using UnityEngine;

public class Bomber : Weapon
{
    public Bomb bombPrefab;
    public float fireForce;

    public override void Fire()
    {
        base.Fire();

        var bomb = Instantiate(bombPrefab, firePoint.position, Quaternion.identity);
        bomb.Init(player, transform.up, fireForce);
    }
}
