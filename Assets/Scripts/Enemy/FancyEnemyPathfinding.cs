using UnityEngine;
using Pathfinding;

public class FancyEnemyPathfinding : MonoBehaviour
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

    private Animator _animator;
    private static readonly int WalkFrontAnimationBool = Animator.StringToHash("isWalkingFront");
    private static readonly int WalkBackAnimationBool = Animator.StringToHash("isWalkingBack");
    private static readonly int WalkSideAnimationBool = Animator.StringToHash("isWalkingSide");

    private Player _target;

    #region Unity Events

    private void Awake()
    {
        _seeker = GetComponent<Seeker>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Follow nearest player
        InvokeRepeating(nameof(TrackNearestPlayer), 0f, 0.5f);
    }

    private void Update()
    {
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

        // Play walk animation
        _animator.SetBool(WalkFrontAnimationBool, false);
        _animator.SetBool(WalkBackAnimationBool, false);
        _animator.SetBool(WalkSideAnimationBool, false);
        if (direction.y > 0f) _animator.SetBool(WalkBackAnimationBool, true);
        else if (direction.y < 0f) _animator.SetBool(WalkFrontAnimationBool, true);
        else _animator.SetBool(WalkSideAnimationBool, true);
    }

    private void StopMoving()
    {
        _isWalking = false;

        // Stop walk animation
        _animator.SetBool(WalkFrontAnimationBool, false);
        _animator.SetBool(WalkBackAnimationBool, false);
        _animator.SetBool(WalkSideAnimationBool, false);
    }

    #endregion

    private void TrackNearestPlayer()
    {
        Track(NearestPlayer()?.transform);
    }

    private Player NearestPlayer()
    {
        var players = FindObjectsOfType<Player>();
        if (players.Length == 0) return null;

        var distance = Vector2.Distance(transform.position, players[0].transform.position);
        var nearest = players[0];

        for (int i = 1; i < players.Length; i++)
        {
            var newDistance = Vector2.Distance(transform.position, players[i].transform.position);
            if (newDistance < distance)
            {
                distance = newDistance;
                nearest = players[i];
            }
        }
        return nearest;
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
