using System;
using UnityEngine;

namespace Code_Base.Mechanics.DailyRewards
{
    public class SizeReward: MonoBehaviour, IDailyReward
    {
        private Player _player;

        private void Start()
        {
            _player = FindObjectOfType<Player>();
        }

        public void Get(int count)
        {
            _player.SizeMultiplier(count);
        }
    }
}