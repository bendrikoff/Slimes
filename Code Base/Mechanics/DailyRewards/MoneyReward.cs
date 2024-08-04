using Code_Base.Interface.Money;
using UnityEngine;

namespace Code_Base.Mechanics.DailyRewards
{
    public class MoneyReward: MonoBehaviour, IReward
    {
        public int Count;
        public void Get()
        {
            Money.Instance.Count += Count;
        }
    }
}