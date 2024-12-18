using UnityEngine;

public class EnemyCollisionDetector : CharacterResources
{    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerResources>(out var player))
        {
            player.TakeDamage(Damage);
        }
    }
}
