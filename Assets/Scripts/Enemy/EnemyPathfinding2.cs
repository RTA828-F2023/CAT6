using UnityEngine;
using Pathfinding;

public class EnemyPathfinding2 : MonoBehaviour
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

    private Rigidbody2D _rigidbody;

    #region Unity Events

    private void Awake()
    {
        _seeker = GetComponent<Seeker>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_path == null || !_isTracking) return;

        // Reached pathfinding destination, stopping...
        if (_currentWaypoint >= _path.vectorPath.Count || Vector2.Distance(transform.position, _targetPosition) <= stoppingDistance)
        {
            StopTracking();
            return;
        }

        // Travel to current waypoint
        Direction = (_path.vectorPath[_currentWaypoint] - transform.position).normalized;
        Walk(Direction);

        // If waypoint reached then proceed to the next waypoint
        if (Vector2.Distance(transform.position, _path.vectorPath[_currentWaypoint]) < NextWaypointDistance) _currentWaypoint++;
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
        if (!target) return false;
        return Track(target.position);
    }

    private void StopTracking()
    {
        _isTracking = false;

        _path = null;
        _currentWaypoint = 0;

        StopWalking();
    }

    #endregion

    private void Walk(Vector2 direction)
    {
        _currentDirection = direction;
        _isWalking = true;
    }

    private void StopWalking()
    {
        _isWalking = false;
    }
}
