using UnityEngine;

[RequireComponent(typeof(PlayerController), typeof(Rigidbody2D), typeof(CapsuleCollider2D))] 
public class PlayerCollisionBehaviour : MonoBehaviour
{    
    [SerializeField] private CoinCounter _coinCounter;
    [SerializeField] private PlayerResources _playerResources;
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Coin>(out var coin))
        {
            _coinCounter.Add();
        }

        if (collision.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.TakeDamage(_playerResources.Damage);
        }
    }
}
