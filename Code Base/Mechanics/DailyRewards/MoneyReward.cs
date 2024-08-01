using Code_Base.Interface.Money;
using UnityEngine;

namespace Code_Base.Mechanics.DailyRewards
{
    public class MoneyReward: MonoBehaviour, IDailyReward
    {
        public void Get(int count)
        {
            Money.Instance.Count += count;
        }
    }
}