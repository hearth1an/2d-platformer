using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private CharacterResources _characterResources;

    [SerializeField] private bool _isDamage;
    [SerializeField] private bool _isHeal;

    private Button _button;

    private int _damage = 25;
    private int _heal = 25;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Simulate);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Simulate);
    }

    private void Simulate()
    {
        if (_isDamage)
        {
            _characterResources.TakeDamage(_damage);
        }
        
        if (_isHeal)
        {
            _characterResources.TryHeal(_heal);
        }
    }

}
