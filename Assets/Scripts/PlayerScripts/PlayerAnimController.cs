using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string Speed = nameof(Speed);
    private const string IsJumping = nameof(IsJumping);

    public void TriggerJump(bool value)
    {
        _animator.SetBool(IsJumping, value);
    }

    public void TriggerRun(float value)
    {
        _animator.SetFloat(Speed, Mathf.Abs(value));        
    }
}
