using UnityEngine;
using System;

public class Apple : MonoBehaviour
{
    [SerializeField] private int _healAmount = 25;

    public int HealAmount { get; private set; }
    public static event Action<Apple> AppleDestroyed;

    private void Awake()
    {
        HealAmount = _healAmount;
    }    

    private void OnDestroy()
    {
        AppleDestroyed?.Invoke(this);
    }
}
