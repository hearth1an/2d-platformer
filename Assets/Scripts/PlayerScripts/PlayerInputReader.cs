using UnityEngine;

public class PlayerInputReader : MonoBehaviour
{
    private const string Jump = nameof(Jump);
    private const string Horizontal = nameof(Horizontal);
    private const string AbilityE = nameof(AbilityE);

    public float HorizontalInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool AbilityInput { get; private set; }


    private void Update()
    {
        AbilityInput = Input.GetButtonDown(AbilityE);
        HorizontalInput = Input.GetAxisRaw(Horizontal);
        JumpInput = Input.GetButtonDown(Jump);
    }
}
