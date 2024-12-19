using UnityEngine;

public class EnemyCollisionDetector : HealthHandler
{    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<DeathHandler>(out var player))
        {
            player.TakeDamage(Damage);
        }
    }
}
