using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private Text _text;

    private int _coinCount;
    private string _coinText;

    private void Awake()
    {
        _coinText = "Монеток: ";
        _coinCount = 0;
        UpdateUI();
    }

    public void Add()
    {
        _coinCount++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _text.text = _coinText + _coinCount.ToString();
    }
}
