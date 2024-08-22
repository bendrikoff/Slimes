using UnityEngine;

namespace Code_Base.Mechanics.DailyRewards
{
    public class WheelReward : MonoBehaviour, IReward
    {
        public int Count;
        public void Get()
        {
            LuckyWheelAnimation.Instance.Count += Count;
        }
    }
}