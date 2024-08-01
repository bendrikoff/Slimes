using System;
using System.Collections;
using System.Collections.Generic;
using Code_Base.Interface.Money;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMoneys : MonoBehaviour
{
    [SerializeField] private Money _money;
    
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _moneyTransform
        ;
    [SerializeField] private Animation _moneyAnimation;

    private Queue<MovedMoney> _moneys;

    private void Start()
    {
        _moneys = new Queue<MovedMoney>();
        
        foreach (var money in FindObjectsOfType<MovedMoney>())
        {
            _moneys.Enqueue(money);
            
            money.OnMoneyPoint+= () =>
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