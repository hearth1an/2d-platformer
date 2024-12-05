using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private List<RoutePoint> _routePoints;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _viewRadius = 4f;
    [SerializeField] private int _health = 100;

    private int _minHealth = 0;
    private int _damage = 25;
    private int _teleportDelay = 2;
    private int _speed = 2;
    private int _currentPointIndex = 0;
    private int _direction = 1;

    private WaitForSeconds _wait;
    private Transform _playerTransform;
    private bool _isChasing = false;
    private bool _isTeleporting = false;

    private bool _isDead => _health == _minHealth;    
    private const string IsTooFar = nameof(IsTooFar);

    private void Awake()
    {
        _wait = new WaitForSeconds(_teleportDelay);
        transform.position = _routePoints[_currentPointIndex].Position;
    }

    private void Update()
    {
        if (IsPlayerInSight())
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

        if (TooFar() && !_isTeleporting)
        {
            StartCoroutine(Teleport());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerResources>(out var player))
        {
            player.TakeDamage(_damage);
        }
    }

    public void TakeDamage(int damage)
    {
        _health = Mathf.Max(_health - damage, _minHealth);

        if (_isDead)
        {
            gameObject.SetActive(false);
        }
    }

    private bool IsPlayerInSight()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _viewRadius);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<PlayerResources>(out var player))
            {
                _playerTransform = player.transform;
                return true;
            }
        }

        return false;
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

    private IEnumerator Teleport()
    {
        _isTeleporting = true;

        _animator.SetBool(IsTooFar, true);

        yield return _wait;

        transform.position = _routePoints[_currentPointIndex].Position;
        _animator.SetBool(IsTooFar, false);

        _isTeleporting = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);
    }
}
