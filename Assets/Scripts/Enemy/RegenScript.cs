using UnityEngine;

public class RegenScript : Enemy
{
    private float _healRate = 3.0f;
    private float _nextHeal = 0.0f;

    private Animator _animator;
    private static readonly int LegCountAnimationInt = Animator.StringToHash("legCount");

    #region Unity Events

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //heal at a regular 3s interval
        if (Time.time > _nextHeal)
        {
            if (_currentHealth < baseHealth)
            {
                _currentHealth++;
                UpdateAnimationWithHealth();
            }
            _nextHeal = Time.time + _healRate;
        }
    }

    #endregion

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        UpdateAnimationWithHealth();
    }

    private void UpdateAnimationWithHealth()
    {
        switch (_currentHealth)
        {
            case 3:
                _animator.SetInteger(LegCountAnimationInt, 5);
                break;

            case 2:
                _animator.SetInteger(LegCountAnimationInt, 3);
                break;

            case 1:
                _animator.SetInteger(LegCountAnimationInt, 1);
                break;

            default:
                break;
        }
    }
}
