using UnityEngine;
using System.Collections;

public class PlayerCollisionDetector : MonoBehaviour
{
    [SerializeField] private CoinCounter _coinCounter;
    [SerializeField] private PlayerResources _playerResources;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheck;

    private float _checkRate = 0.1f;
    private WaitForSeconds _wait;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        _wait = new WaitForSeconds(_checkRate);
        StartCoroutine(CheckIfGrounded());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Coin>(out Coin coin))
        {
            _coinCounter.Add();
        }

        if (collision.TryGetComponent<EnemyCollisionDetector>(out var enemy))
        {
            enemy.TakeDamage(enemy.Damage);
        }

        if (collision.TryGetComponent<Apple>(out Apple apple))
        {
            if (_playerResources.TryHeal(apple.HealAmount))
            {
                Destroy(apple.gameObject);
            }            
        }
    }

    private bool IsOnGround()
    {
        float radius = 0.2f;        

        return Physics2D.OverlapCircle(_groundCheck.position, radius, _groundLayer);
    }

    private IEnumerator CheckIfGrounded()
    {
        while (enabled)
        {
            IsGrounded = IsOnGround();
            yield return _wait;            
        }        
    }
}
