using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private List<RoutePoint> _routePoints;
    [SerializeField] private Animator _animator;

    private int _speed = 2;
    private int _currentPointIndex = 0;
    private int _firstRouteIndex = 0;
    private int _direction = 1;

    public Vector3 Position => transform.position;

    private void Awake()
    {
        transform.position = _routePoints[_currentPointIndex].Position;
    }

    private void Update()
    {
        Move();

        if (IsTooFar())
        {
            Teleport();
        }
        else
        {
            _animator.SetBool("IsTooFar", false);
        }
    }

    private void Move()
    {
        float minDistance = 0.5f;
        int reverseDirection = -1;

        if (Vector2.Distance(transform.position, _routePoints[_currentPointIndex].Position) < minDistance)
        {
            _currentPointIndex += _direction;

            if (_currentPointIndex >= _routePoints.Count || _currentPointIndex < _firstRouteIndex)
            {
                _direction *= reverseDirection;
                _currentPointIndex += _direction;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, _routePoints[_currentPointIndex].Position, _speed * Time.deltaTime);
    }

    private void Teleport()
    {
        transform.position = _routePoints[_currentPointIndex].Position;
    }

    private bool IsTooFar()
    {
        float maxDistance = 6f;        

        if (Vector2.Distance(transform.position, _routePoints[_currentPointIndex].Position) > maxDistance)
        {
            _animator.SetBool("IsTooFar", true);
            return true;
        }

        return false;
    }
}