using System.Collections;
using UnityEngine;
using TMPro;
using System;

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

    public event Action<bool> AbilityActive;

    public int Duration { get; private set; } = 6;
    public float Cooldown { get; private set; } = 4;

    private void Awake()
    {
        _radiusSprite.enabled = false;
        UpdateRadiusSprite();
    }

    private void Update()
    {
        if (_inputReader.AbilityInput && !_isAbilityActive && !_isCooldown)
        {
            StartCoroutine(ActivateAbility());
        }
    }

    private IEnumerator ActivateAbility()
    {
        float elapsedTime = 0f;
        float nextDamageTime = 0f;

        _isAbilityActive = true;
        AbilityActive?.Invoke(true);

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

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<EnemyCollisionDetector>(out var enemy) && !enemy.IsDead)
            {
                enemy.TakeDamage(_vamprirismValue);
                _characterResources.TryHeal(_vamprirismValue);
            }
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
