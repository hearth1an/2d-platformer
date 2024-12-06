using UnityEngine;
using System;

public class Apple : MonoBehaviour
{
    [SerializeField] private int _healAmount = 25;

    public static event Action<Apple> AppleDestroyed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerResources>(out var player))
        {
            if (player.TryHeal(_healAmount))
            {
                AppleDestroyed?.Invoke(this);
                Destroy(gameObject);
            }
        }
    }
}
