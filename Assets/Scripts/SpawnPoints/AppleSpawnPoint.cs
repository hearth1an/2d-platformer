using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AppleSpawnPoint : MonoBehaviour
{
    [SerializeField] private Apple _apple;
    [SerializeField] private PlayerMovingController _player;

    private Apple _currentApple;

    private int _spawnRate = 5;
    private WaitForSeconds _sleepTime;

    private Vector2 _position;
    private Quaternion _rotation;

    private void Awake()
    {
        _sleepTime = new WaitForSeconds(_spawnRate);
        _rotation = transform.rotation;
        _position = transform.position;
        SpawnApple();

        _currentApple.AppleDestroyed += HandleAppleDestroyed;
    }

    private void OnDestroy()
    {
        _currentApple.AppleDestroyed -= HandleAppleDestroyed;
    }

    private void HandleAppleDestroyed(Apple destroyedApple)
    {
        if (_currentApple == destroyedApple)
        {
            _currentApple = null;
            StartCoroutine(Respawn());
        }
    }

    private void SpawnApple()
    {
        _currentApple = Instantiate(_apple, _position, _rotation);
    }

    private IEnumerator Respawn()
    {
        yield return _sleepTime;
        SpawnApple();
    }
}
