using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerTracker : MonoBehaviour
{
    private float _viewRadius = 4f;
    private WaitForSeconds _rate;
    private float _checkRate = 0.1f;    
    private EnemyBehaviour _enemyBehaviour;

    public bool IsPlayerNear { get; private set; } = false;

    private void Awake()
    {
        _enemyBehaviour = GetComponent<EnemyBehaviour>();
        _rate = new WaitForSeconds(_checkRate);
    }

    private void Update()
    {
        StartCoroutine(nameof(CheckPlayerNear));
    }

    private bool IsPlayerInSight()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _viewRadius);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<PlayerResources>(out var player))
            {
                _enemyBehaviour.GetPlayerTransform(player.transform);

                return true;
            }
        }

        return false;
    }

    private IEnumerator CheckPlayerNear()
    {
        while (enabled)
        {
            IsPlayerNear = IsPlayerInSight();

            yield return _rate;
        }
    }
}
