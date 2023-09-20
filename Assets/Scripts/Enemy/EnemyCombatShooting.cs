using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatShooting : MonoBehaviour
{
    [Header("Intelligence")]
    [SerializeField] private bool seeThroughWalls = false;
    [SerializeField] private LayerMask terrainLayer;

    [Header("Shooting Stats")]
    [SerializeField] private int fireRate = 1;
    [SerializeField] private float fireDelayBetweenBullet = 0.25f;
    [SerializeField] private float fireRecoveryTime;
    [SerializeField] private float fireForce;
    public EnemyProjectile projectilePrefab;

    private GameObject _closestPlayer;
    private bool _canFire = true;
    private GameObject[] _players;

    void Start()
    {
        _players = GameObject.FindGameObjectsWithTag("Player");
    }

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
      
        StartCoroutine(SequentialFireWithDelay(fireDelayBetweenBullet));

        // Down time between bursts of bullets can fire again
        _canFire = false;
        StartCoroutine(RecoverFire(fireRecoveryTime));
    }

    private IEnumerator SequentialFireWithDelay(float bulletDelay) 
    {
        for (int i = 0; i < fireRate; i++)
        {
            //Aim for nearest Player
            FindClosestPlayerWithTag();

            //Check if Enemy can See Player
            if (HasLineOfSight() || seeThroughWalls)
            {
                Vector2 directionToClosestPlayer = (_closestPlayer.transform.position - transform.position).normalized;

                var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                projectile.Init(directionToClosestPlayer, fireForce);
            }

            yield return new WaitForSeconds(bulletDelay);

        }
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
        
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        //For each player, check whos currently the closest
        foreach (GameObject player in _players)
        {
            Vector3 directionToPlayer = player.transform.position - currentPosition;
            //Gets distance of enemy to player
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
        yield return new WaitForSeconds(1.0f);
        FindClosestPlayerWithTag();
    }

    #endregion

    #region Line Of Sight

    private bool HasLineOfSight() 
    {
        Vector2 direction = transform.position - _closestPlayer.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(_closestPlayer.transform.position, direction, direction.magnitude, terrainLayer);

        // Check if the ray hits any obstacles.
        if (hit.collider != null)
        {
            return false;
        }
        
        return true;
    }

    #endregion
}
