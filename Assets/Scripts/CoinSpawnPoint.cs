using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CoinSpawnPoint : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private CoinCounter _coinManager;

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
        if (collision.gameObject.CompareTag("Player") && _currentCoin != null)
        {
            Destroy(_currentCoin.gameObject);
            _currentCoin = null;

            _coinManager.Add();

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
