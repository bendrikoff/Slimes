using System;
using UnityEngine;

namespace Code_Base.Interface.Money
{
    public class Money : Singleton<Money>
    {
        [SerializeField] private MoneyUI _moneyUI;
        public int Count
        {
            get => _count;

            set
            {
                if (value > 0)
                {
                    _count = value;
                    Change();
                }
            }
        }

        private int _count;

        public void Change()
        {
            _moneyUI.MoneyText.text = Count.ToString();
        }
        
        public bool TryToBuy(int price)
        {
            if (Count - price >= 0)
            {
                Count -= price;
                return true;
            }

            return false;
        }
    }
}