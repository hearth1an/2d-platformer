using UnityEngine;

public class CoinCounter : MonoBehaviour
{    
    [SerializeField] private UIUpdater _uIUpdater;

    private int _coinCount = 0;

    private void Awake()
    {
        _uIUpdater.UpdateUi(_coinCount.ToString());
    }

    public void Add()
    {
        _coinCount++;
        _uIUpdater.UpdateUi(_coinCount.ToString());
    }
}
