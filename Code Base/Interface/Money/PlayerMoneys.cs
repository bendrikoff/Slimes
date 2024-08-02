using System.Collections.Generic;
using Code_Base.Interface.Money;
using UnityEngine;

public class PlayerMoneys : MonoBehaviour
{
    [SerializeField] private Money _money;

    private Queue<MovedMoney> _moneys;

    private void Start()
    {
        _moneys = new Queue<MovedMoney>();
        
        foreach (var money in FindObjectsOfType<MovedMoney>())
        {
            _moneys.Enqueue(money);
            
            money.OnMoneyPoint += () =>
            {
                _moneys.Enqueue(money);
                _money.Count++;
            };
        }
    }

    public void GetMoney()
    {
        if (_moneys.TryDequeue(out var money))
        {
            money.Move();
        }
    }
}