using UnityEngine;
using Pathfinding;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class FancyEnemyPathfinding : MonoBehaviour
{
    [Header("Pathfinding Stats")]
    public bool inRoom;
    private float distance;
    private Vector2 startTarget; //go down the hallway, enter the room
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
        inRoom = false;
        startTarget = new Vector2(this.transform.position.x, 2.5f);
    }

    private void Update()
    {
        //If in the room, normal behaviour. Else, enter the room
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
            //For the simple level arrangement where enemies all spawn above the door
            Move((startTarget - (Vector2)transform.position).normalized);
            if ((this.transform.position.y) <= 3){
                InvokeRepeating(nameof(TrackNearestPlayer), 0f, 0.5f);
                inRoom = true;
            }

            /*Use this if we use more dynamic levels
            distance = Vector2.Distance(transform.position, startTarget);
            if (distance <= 0.5f)
            {
                // Follow nearest player
                InvokeRepeating(nameof(TrackNearestPlayer), 0f, 0.5f);
                inRoom = true;
            }
            */

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
