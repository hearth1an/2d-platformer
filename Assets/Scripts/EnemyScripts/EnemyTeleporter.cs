using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimationController))]
public class EnemyTeleporter : MonoBehaviour
{
    private int _teleportDelay = 2;
    private WaitForSeconds _wait;
    private EnemyAnimationController _enemyAnimController;

    public bool IsTeleporting { get; private set; } = false;

    private void Awake()
    {        
        _enemyAnimController = GetComponent<EnemyAnimationController>();
        _wait = new WaitForSeconds(_teleportDelay);
    }

    public IEnumerator Teleport(Vector3 position)
    {
        IsTeleporting = true;
        _enemyAnimController.ToggleTeleport(IsTeleporting);

        yield return _wait;

        transform.position = position;        

        IsTeleporting = false;
        _enemyAnimController.ToggleTeleport(IsTeleporting);
    }
}
