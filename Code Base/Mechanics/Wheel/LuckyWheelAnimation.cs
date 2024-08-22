using System;
using Code_Base.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code_Base.Mechanics
{
    public class LuckyWheelAnimation : Singleton<LuckyWheelAnimation>
    {
        public int Count
        {
            get => _count;

            set
            {
                if (value > 0)
                {
                    _count = value;
                    ChangeCount();
                }
            }
        }

        [SerializeField]private int _count;
        
        public Action OnAnimationEnded;
        
        public float InitialSpeed = 500f;
        public float Deceleration = 100f;

        private float currentSpeed;

        private void Update()
        {
            Animation();
        }

        private void Animation()
        {
            if (currentSpeed > 0)
            {
                transform.Rotate(new Vector3(0, 0, 1), currentSpeed * Time.deltaTime);   
                var randomDeceleration = Random.Range(50, 150);
                currentSpeed -= randomDeceleration * Time.deltaTime;
                
                if (currentSpeed < 0)
                {
                    currentSpeed = 0;
                    OnAnimationEnded?.Invoke();
                }
            }
        }

        public void StartAnimation()
        {
            if (Count > 0)
            {
                var randomSpeed = Random.Range(450, 550);
                currentSpeed = randomSpeed;
                Count--;
            }
        }

        public void ChangeCount()
        {
            SavingSystem.Instance.Data.WheelCount = Count;
            SavingSystem.Instance.Save();
        }
    }
}