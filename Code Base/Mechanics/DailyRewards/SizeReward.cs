using System;
using UnityEngine;

namespace Code_Base.Mechanics.DailyRewards
{
    public class SizeReward: MonoBehaviour, IReward
    {
        private Player _player;
        public int Count;


        private void Start()
        {
            _player = FindObjectOfType<Player>();
        }
        

        public void Get()
        {
            _player.SizeMultiplier(Count);
        }
    }
}