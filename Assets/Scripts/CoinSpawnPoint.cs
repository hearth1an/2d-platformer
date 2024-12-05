using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CoinSpawnPoint : MonoBehaviour
{
    [SerializeField] private Coin _coin;

    private Coin _currentCoin;

    private int _spawnRate = 5;
    private WaitForSeconds _sleepTime;

    private Vector2 _position;
    private Quaternion _rotation;

    private void Awake()
    {
        _sleepTime = new WaitForSeconds(_spawnRate);
        _rotation = transform.rotation;
        _position = transform.position;

        SpawnCoin();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out var player) && _currentCoin != null)
        {
            Destroy(_currentCoin.gameObject);

            StartCoroutine(Respawn());
        }
    }

    private void SpawnCoin()
    {
        _currentCoin = Instantiate(_coin, _position, _rotation);
    }

    private IEnumerator Respawn()
    {
        yield return _sleepTime;
        SpawnCoin();
    }
}
