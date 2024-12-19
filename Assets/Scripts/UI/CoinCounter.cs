using System;

public class CoinCounter : Collectable
{    
    private int _value = 0;

    public event Action<int> UpdateCount;

    private void Awake()
    {       
        UpdateCount?.Invoke(_value);        
    }

    public void Add()
    {
        _value++;
        UpdateCount?.Invoke(_value);
    }
}
