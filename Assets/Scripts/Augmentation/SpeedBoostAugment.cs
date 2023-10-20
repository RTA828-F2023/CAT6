using UnityEngine;

public class SpeedBoostAugment : Augment
{
    [Header("Speed Boost Stats")]
    [SerializeField] private float boostFactor;

    public override void Apply(Player player)
    {
        player.walkForce *= boostFactor;
        base.Apply(player);
    }

    public override void Revert(Player player)
    {
        player.walkForce /= boostFactor;
        base.Revert(player);
    }
}
