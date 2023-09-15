using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] private float fireRecoveryTime;
    [SerializeField] private float fireForce;
    [SerializeField] private EnemyProjectile projectilePrefab;

    private GameObject _closestPlayer;
    private bool _canFire = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_canFire)
        {
            Fire();
        }
    }

    #region Fire Methods

    private void Fire()
    {
        if (!_canFire) return;

        FindClosestPlayerWithTag();
        Vector2 directionToClosestPlayer = (_closestPlayer.transform.position - transform.position).normalized;

        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.Init(directionToClosestPlayer, fireForce);

        // Down time before player can fire again
        _canFire = false;
        StartCoroutine(RecoverFire(fireRecoveryTime));
    }

    private IEnumerator RecoverFire(float time)
    {
        yield return new WaitForSeconds(time);
        _canFire = true;
    }

    #endregion

    #region Locate Nearest Player

    private void FindClosestPlayerWithTag()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject player in players)
        {
            Vector3 directionToPlayer = player.transform.position - currentPosition;
            float distanceToPlayer = directionToPlayer.sqrMagnitude;

            if (distanceToPlayer < closestDistance)
            {
                closestDistance = distanceToPlayer;
                _closestPlayer = player;
            }
        }
    }

    private IEnumerator FindClosestPlayerCoroutine()
    {
        yield return new WaitForSeconds(1.0f); // Adjust the interval as needed
        FindClosestPlayerWithTag();
    }

    #endregion
}
