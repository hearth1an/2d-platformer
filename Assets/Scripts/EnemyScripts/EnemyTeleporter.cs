using System.Collections;
using UnityEngine;

public class EnemyTeleporter : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private int _teleportDelay = 2;
    private WaitForSeconds _wait;

    private const string IsTooFar = nameof(IsTooFar);

    public bool IsTeleporting { get; private set; } = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _wait = new WaitForSeconds(_teleportDelay);
    }

    public IEnumerator Teleport(Vector3 position)
    {
        IsTeleporting = true;

        _animator.SetBool(IsTooFar, true);

        yield return _wait;

        transform.position = position;
        _animator.SetBool(IsTooFar, false);

        IsTeleporting = false;
    }
}
