using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [Header("Intelligence")]
    [SerializeField] private bool seeThroughWalls = false;

    [Header("Shooting Stats")]
    [SerializeField] private int fireRate = 1;
    [SerializeField] private float fireDelay = 0.25f;
    [SerializeField] private float fireRecoveryTime;
    [SerializeField] private float fireForce;
    public EnemyProjectile projectilePrefab;

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
      
        StartCoroutine(SequentialFireWithDelay(fireDelay));

        // Down time before player can fire again
        _canFire = false;
        StartCoroutine(RecoverFire(fireRecoveryTime));
    }

    private IEnumerator SequentialFireWithDelay(float bulletDelay) 
    {
        for (int i = 0; i < fireRate; i++)
        {
            FindClosestPlayerWithTag();

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

    #region Line Of Sight
    private bool HasLineOfSight() 
    {
        Vector2 direction = transform.position - _closestPlayer.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(_closestPlayer.transform.position, direction, direction.magnitude);

        // Layer mask to filter out objects you want to be considered as obstacles (e.g., walls).
        int layerMask = LayerMask.GetMask("Terrain");

        // Check if the ray hits any obstacles.
        if (hit.collider != null && (layerMask & (1 << hit.collider.gameObject.layer)) != 0)
        {
            // A collision occurred; there is no direct line of sight.
            return false;
        }
        else
        {
            // No collision; there is a direct line of sight.
            return true;
        }
    }
    #endregion
}
