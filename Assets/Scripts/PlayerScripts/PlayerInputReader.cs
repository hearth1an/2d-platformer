using UnityEngine;

public class PlayerInputReader : MonoBehaviour
{
    private const string Jump = nameof(Jump);
    private const string Horizontal = nameof(Horizontal);

    public float HorizontalInput { get; private set; }
    public bool JumpInput { get; private set; }

    private void Update()
    {
        HorizontalInput = Input.GetAxisRaw(Horizontal);
        JumpInput = Input.GetButtonDown(Jump);
    }
}
