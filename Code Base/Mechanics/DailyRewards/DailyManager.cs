using System;
using System.Collections.Generic;
using Code_Base.Data;
using TMPro;
using UnityEngine;

namespace Code_Base.Mechanics.DailyRewards
{
    public class DailyManager: Singleton<DailyManager>
    {
        public List<GameObject> CheckInButtons; 
        private void Start()
        {
            ReloadUI();
        }

        public void CheckIn(int day)
        {
            if (day == 7)
            {
                SavingSystem.Instance.Data.LastDailyReward = 0;
            }
            else
            {
                SavingSystem.Instance.Data.LastDailyReward = day;
            }
            SavingSystem.Instance.Data.LastCheckIn = DateTime.Now.ToString();
            SavingSystem.Instance.Save();
            ReloadUI();
        }

        private void ReloadUI()
        {
            foreach (var button in CheckInButtons)
            {
                //button.SetActive(false);
            }
            for (var i = 0; i < CheckInButtons.Count; i++)
            {
                if (i == SavingSystem.Instance.Data.LastDailyReward &&
                    GetSavedDay() != DateTime.Today.Day)
                {
                    CheckInButtons[i].SetActive(true);
                    return;
                }
            }
        }

        private int GetSavedDay()
        {
            if (DateTime.TryParse(SavingSystem.Instance.Data.LastCheckIn, out var date))
            {
                return date.Day;
            }
            
            return 0;
        }
    }
}