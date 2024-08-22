using System;
using Code_Base.Mechanics.DailyRewards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code_Base.Mechanics.TimerReward
{
    public class TimerReward : MonoBehaviour
    {
        public int StartSeconds;

        public TextMeshProUGUI Text;

        public Button Button;
        
        public Image ButtonImage;

        private Timer _timer;

        private IReward _reward;

        private bool _isTimerStopped;

        private void Start()
        {
            _timer = new Timer(StartSeconds);
            _timer.OnTimerEnd += SetButtonClickable;
            _reward = gameObject.GetComponentInChildren<IReward>();
            Button = gameObject.GetComponentInChildren<Button>();
        }

        private void SetButtonClickable()
        {
            //todo: хуйня не работает
            _isTimerStopped = true;
            Text.text = "Получить";
            Button.gameObject.GetComponent<Image>().color = new Color(0, 188, 0);
            Button.onClick.AddListener(_reward.Get);
        }

        private void Update()
        {
            if (_isTimerStopped == false)
            {
                SetButtonText();
            }
        }

        private void SetButtonText()
        {
            Text.text = GetTimerText();
        }
        private string GetTimerText()
        {
            var hours = _timer.Hours == 0
                ? ""
                : _timer.Hours + ":";
            var minutes = _timer.Minutes == 0 
                ? ""
                : _timer.Minutes + ":";
            var seconds = _timer.Seconds == 0 
                ? ""
                : _timer.Seconds.ToString();
            return $"{hours}{minutes}{seconds}";
        }
        
        
    }
}