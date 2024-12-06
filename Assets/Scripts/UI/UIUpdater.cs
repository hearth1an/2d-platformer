using UnityEngine.UI;
using UnityEngine;

public class UIUpdater : MonoBehaviour
{
    [SerializeField] private Text _text;

    private const string CoinText = "Монеток: ";
    
    public void UpdateUi(string text)
    {
        _text.text = CoinText+ text;
    }
}
