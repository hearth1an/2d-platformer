using UnityEngine;
using System;

public class Apple : Collectable
{
    [SerializeField] private int _healAmount = 25;

    public int HealAmount { get; private set; }
    public event Action<Apple> AppleDestroyed;

    private void Awake()
    {
        HealAmount = _healAmount;
    }    

    private void OnDestroy()
    {
        AppleDestroyed?.Invoke(this);
    }
}
