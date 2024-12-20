using System.Collections;
using UnityEngine;
using TMPro;
using System;
using Unity.Burst.CompilerServices;

public class VampirismAbility : MonoBehaviour
{
    [SerializeField] private HealthHandler _characterResources;
    [SerializeField] private PlayerInputReader _inputReader;
    [SerializeField] private SpriteRenderer _radiusSprite;

    private int _radius = 5;    
    private int _vamprirismValue = 1;
    private float _checkRate = 0.5f;

    private bool _isCooldown = false;
    private bool _isAbilityActive = false;

    public event Action AbilityActive;

    public int Duration { get; private set; } = 6;
    public float Cooldown { get; private set; } = 4;

    private void Awake()
    {
        _radiusSprite.enabled = false;
        UpdateRadiusSprite();

        _inputReader.AbilityPressed += Activate;
    }

    private void OnDestroy()
    {
        _inputReader.AbilityPressed -= Activate;
    }

    private void Activate()
    {
        if (_isAbilityActive == false && _isCooldown == false)
        {
            StartCoroutine(ActivateAbility());
        }
    }

    private IEnumerator ActivateAbility()
    {
        float elapsedTime = 0f;
        float nextDamageTime = 0f;

        _isAbilityActive = true;
        AbilityActive?.Invoke();

        while (elapsedTime < Duration)
        {
            elapsedTime += Time.deltaTime;            

            if (elapsedTime >= nextDamageTime)
            {
                ApplyVampirismToEnemiesInRadius();
                nextDamageTime += _checkRate;
            }

            yield return null;
        }

        _isAbilityActive = false;

        StartCoroutine(StartCooldown());
    }

    private void ApplyVampirismToEnemiesInRadius()
    {
        if (_isAbilityActive == false) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius);
        _radiusSprite.enabled = true;

        EnemyCollisionDetector nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<EnemyCollisionDetector>(out var enemy) && enemy.IsDead == false )
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }

        if (nearestEnemy != null)
        {
            int limitedDamage = _vamprirismValue;

            if (nearestEnemy.Health < _vamprirismValue)
            {
                limitedDamage = nearestEnemy.Health;
            }

            nearestEnemy.TakeDamage(limitedDamage);
            _characterResources.TryHeal(limitedDamage);
        }
    }

    private IEnumerator StartCooldown()
    {
        _isCooldown = true;

        float elapsedTime = 0f;

        _radiusSprite.enabled = false;

        while (elapsedTime < Cooldown)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _isCooldown = false;
    }

    private void UpdateRadiusSprite()
    {
        Vector3 spriteVector = new Vector3(_radius * 2, _radius * 2, 1);

        _radiusSprite.transform.localScale = spriteVector;
    }
}
