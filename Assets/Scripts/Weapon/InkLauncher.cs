using UnityEngine;

public class InkLauncher : Weapon
{
    public Inkblob inkblobPrefab;
    public float fireForce;

    public override bool Fire()
    {
        if (!base.Fire()) return false;

        var inkblob = Instantiate(inkblobPrefab, firePoint.position, Quaternion.identity);
        inkblob.Init(player, transform.up, fireForce);

        return true;
    }
}
