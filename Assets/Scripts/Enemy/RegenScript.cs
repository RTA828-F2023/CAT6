using UnityEngine;

public class RegenScript : Enemy
{
    private float _healRate = 2.0f;
    private float _nextHeal = 0.0f;

    #region Unity Events

    private void Update()
    {
        //heal at a regular 2s interval
        if (Time.time > _nextHeal)
        {
            if (_currentHealth < baseHealth)
            {
                _currentHealth++;
            }
            _nextHeal = Time.time + _healRate;
        }
    }

    #endregion
}
