using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimationController : MonoBehaviour
{
    private const string IsTooFar = nameof(IsTooFar);

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ToggleTeleport(bool isEnabled)
    {
        _animator.SetBool(IsTooFar, isEnabled);
    }
}
