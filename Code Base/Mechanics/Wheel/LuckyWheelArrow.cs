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
        Debug.Log(Reward);
        Reward.Get();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IReward>(out var reward))
        {
            Reward = reward;
        }
    }
}