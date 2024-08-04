using System;
using Code_Base.Mechanics;
using Code_Base.Mechanics.DailyRewards;
using UnityEngine;

public class LuckyWheelArrow:MonoBehaviour
{
    public LuckyWheelAnimation Animation;
    public IReward Reward;

    private void OnEnable()
    {
        Animation.OnAnimationEnded += GetReward;
    }
    
    private void OnDisable()
    {
        Animation.OnAnimationEnded -= GetReward;
    }

    private void GetReward()
    {
        Reward.Get();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IReward>(out var reward))
        {
            Reward = reward;
        }
    }
}