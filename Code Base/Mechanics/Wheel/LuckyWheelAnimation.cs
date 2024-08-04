using System;
using UnityEngine;

namespace Code_Base.Mechanics
{
    public class LuckyWheelAnimation : MonoBehaviour
    {
        public Action OnAnimationEnded;
        
        public float InitialSpeed = 500f;
        public float Deceleration = 100f;

        private float currentSpeed;

        private void Start()
        {
            currentSpeed = InitialSpeed; 
        }

        private void Update()
        {
            StartAnimation();
        }

        private void StartAnimation()
        {
            if (currentSpeed > 0)
            {
                transform.Rotate(new Vector3(0, 0, 1), currentSpeed * Time.deltaTime);                 currentSpeed -= Deceleration * Time.deltaTime;
                if (currentSpeed < 0)
                {
                    currentSpeed = 0;
                }
            }
        }
    }
}