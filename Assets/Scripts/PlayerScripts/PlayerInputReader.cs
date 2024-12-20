using System;
using UnityEngine;

public class PlayerInputReader : MonoBehaviour
{
    private const string Jump = nameof(Jump);
    private const string Horizontal = nameof(Horizontal);
    private const string AbilityE = nameof(AbilityE);

    public float HorizontalInput { get; private set; }    

    public event Action JumpPressed;
    public event Action AbilityPressed;

    private void Update()
    {        
        HorizontalInput = Input.GetAxisRaw(Horizontal);

        if (Input.GetButtonDown(Jump))
        {
            JumpPressed?.Invoke();
        }

        if (Input.GetButtonDown(AbilityE))
        {
            AbilityPressed?.Invoke();
        }
    }
}
