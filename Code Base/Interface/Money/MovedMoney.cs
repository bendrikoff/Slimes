using System;
using UnityEngine;

public class MovedMoney : MonoBehaviour
{
    public Action OnMoneyPoint;
    
    [SerializeField] private Animation _moneyAnimation;
    private Animation _moveAnimation;

    private void Start()
    {
        _moveAnimation = GetComponent<Animation>();
    }

    public void Move()
    {
        _moveAnimation.Play();
    }
    public void PlayAnim()
    {
        _moneyAnimation.Play();
        OnMoneyPoint?.Invoke();
    }
}