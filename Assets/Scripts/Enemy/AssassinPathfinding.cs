using UnityEngine;
using Pathfinding;
using System;
using Unity.VisualScripting;

public class AssassinPathfinding : MonoBehaviour
{
    [Header("Pathfinding Stats")]
    public float stoppingDistance;
    private Vector2 _targetPosition;
    private bool _isTracking;
    private int _currentWaypoint;
    public Vector2 Direction { get; set; }
    private const float NextWaypointDistance = 0.1f;

    private Path _path;
    private Seeker _seeker;

    public float walkForce;
    private bool _isWalking;
    private Vector2 _currentDirection = Vector2.up;
    private Vector2 startTarget;
    private Boolean inRoom = false;
    private Rigidbody2D _rigidbody;

    private Player _target;

    #region Unity Events

    private void Awake()
    {
        _seeker = GetComponent<Seeker>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Follow lowest health player
        InvokeRepeating(nameof(TrackWeakestPlayer), 0f, 0.5f);
        //Enemy spawns outside game area, so should first enter game area
        if (this.transform.position.x < -9.5f)
        {
            startTarget = new Vector2(UnityEngine.Random.Range(-9.5f, 9.5f), this.transform.position.y);
        }
        if (this.transform.position.x > 9.5f)
        {
            startTarget = new Vector2(UnityEngine.Random.Range(-9.5f, 9.5f), this.transform.position.y);
        }
        if (this.transform.position.y > 2.3f)
        {
            startTarget = new Vector2(this.transform.position.x, UnityEngine.Random.Range(-5.3f, 2.3f));
        }
    }

    private void Update()
    {
        if (inRoom){
            if (_path != null || _isTracking)
            {
                // Reached pathfinding destination, stopping...
                if (_currentWaypoint >= _path.vectorPath.Count || Vector2.Distance(transform.position, _targetPosition) <= stoppingDistance)
                {
                    StopTracking();
                    return;
                }

                // Travel to current waypoint
                Direction = (_path.vectorPath[_currentWaypoint] - transform.position).normalized;
                Move(Direction);

                // If waypoint reached then proceed to the next waypoint
                if (Vector2.Distance(transform.position, _path.vectorPath[_currentWaypoint]) < NextWaypointDistance) _currentWaypoint++;
            }
        }
        else
        {
            float distance = Vector2.Distance(transform.position, startTarget);
            if (distance <= 1f)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, startTarget, 2 * Time.deltaTime);
            }
            else
            {
                inRoom = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isWalking) _rigidbody.AddForce(_currentDirection * walkForce, ForceMode2D.Force);
    }

    #endregion

    #region Tracking Methods

    public bool Track(Vector2 position)
    {
        _targetPosition = position;
        _isTracking = false;

        _seeker.StartPath(transform.position, _targetPosition, path =>
        {
            if (path.error)
            {
                Debug.LogError(path.errorLog);
                StopTracking();
                return;
            }

            _isTracking = true;
            _path = path;
            _currentWaypoint = 0;
        });

        return _isTracking;
    }

    public bool Track(Transform target)
    {
        if (!target)
        {
            StopMoving();
            return false;
        }

        return Track(target.position);
    }

    private void StopTracking()
    {
        _isTracking = false;

        _path = null;
        _currentWaypoint = 0;

        StopMoving();
    }

    #endregion

    #region Movement Methods

    private void Move(Vector2 direction)
    {
        _currentDirection = direction;
        _isWalking = true;
    }

    private void StopMoving()
    {
        _isWalking = false;
    }

    #endregion

    private void TrackWeakestPlayer()
    {
        Track(WeakestPlayer().transform);
    }

    private Player WeakestPlayer()
    {
        var players = FindObjectsOfType<Player>();
        if (players.Length == 0) return null;

        var weakest = players[0]._currentHealth;
        var weakestPlayer = players[0];

        for (int i = 1; i < players.Length; i++)
        {
            if (players[i]._currentHealth < weakest)
            {
                weakest = players[i]._currentHealth;
                weakestPlayer = players[i];
            }
        }
        return weakestPlayer;
    }

    private int NearestPathPoint()
    {
        var patrolRoute = Level.Instance.patrolRoute;
        var distance = Vector2.Distance(transform.position, patrolRoute[0]);
        var nearest = 0;

        for (int i = 1; i < patrolRoute.Length; i++)
        {
            var newDistance = Vector2.Distance(transform.position, patrolRoute[i]);
            if (newDistance < distance)
            {
                distance = newDistance;
                nearest = i;
            }
        }
        return nearest;
    }
}
