using UnityEngine;
using Zenject;
using UniRx;

public class CurrenyManager : MonoBehaviour
{
    public static CurrenyManager Instance { get; private set; }

    private ReactiveProperty<int> _currentGold = new ReactiveProperty<int>(20);
    public IReadOnlyReactiveProperty<int> CurrenntGold => _currentGold;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }   
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGold(int amount)
    {
        _currentGold.Value += amount;
    }

    public void UseGold(int amount)
    {
        if(_currentGold.Value >= amount)
        {
            _currentGold.Value -= amount;
        }
    }


}
