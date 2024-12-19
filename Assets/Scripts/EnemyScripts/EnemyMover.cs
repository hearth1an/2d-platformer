using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyTeleporter), typeof(EnemyPlayerTracker))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<RoutePoint> _routePoints;
    [SerializeField] private EnemyTeleporter _teleporter;
    [SerializeField] private float _viewRadius = 4f;
    
    private int _speed = 2;
    private int _currentPointIndex = 0;
    private int _direction = 1;

    private Transform _playerTransform;
    private EnemyPlayerTracker _playerTracker;
    private bool _isChasing = false;

    private void Awake()
    {
        _playerTracker = GetComponent<EnemyPlayerTracker>();
        _teleporter = GetComponent<EnemyTeleporter>();
        transform.position = _routePoints[_currentPointIndex].Position;
    }

    private void Update()
    {     
        if (_playerTracker.IsPlayerNear)
        {
            StartChasing();
        }
        else
        {
            StopChasing();
        }

        if (_isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        if (TooFar() && _teleporter.IsTeleporting == false)
        {
            _teleporter.StartCoroutine(_teleporter.Teleport(_routePoints[_currentPointIndex].Position));
        }
    }

    public void GetPlayerTransform(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    private void StartChasing()
    {
        _isChasing = true;
    }

    private void StopChasing()
    {
        _isChasing = false;
        _playerTransform = null;
    }

    private void Patrol()
    {
        float minDistance = 0.5f;
        int reverseDirection = -1;

        if (Vector2.Distance(transform.position, _routePoints[_currentPointIndex].Position) < minDistance)
        {
            _currentPointIndex += _direction;

            if (_currentPointIndex >= _routePoints.Count || _currentPointIndex < 0)
            {
                _direction *= reverseDirection;
                _currentPointIndex += _direction;
            }
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            _routePoints[_currentPointIndex].Position,
            _speed * Time.deltaTime
        );
    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            _playerTransform.position,
            _speed * Time.deltaTime
        );
    }

    private bool TooFar()
    {
        float maxDistance = 6f;

        if (Vector2.Distance(transform.position, _routePoints[_currentPointIndex].Position) > maxDistance)
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);
    }
}
