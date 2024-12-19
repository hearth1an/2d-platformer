using UnityEngine.UI;
using UnityEngine;

public class UIUpdater : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private CoinCounter _counter;

    private const string CoinText = "Coins: ";

    private void Awake()
    {
        _counter = GetComponent<CoinCounter>();        
        _counter.UpdateCount += UpdateUi;
    }

    private void OnDestroy()
    {
        _counter.UpdateCount -= UpdateUi;
    }

    public void UpdateUi(int text)
    {
        _text.text = CoinText+ text;
    }
}
