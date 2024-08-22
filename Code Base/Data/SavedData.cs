using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code_Base.Data
{
    [Serializable]
    public class SavedData
    {
        public List<SkinName> BuyedSkins;

        public SkinName SelectedSkin;

        public string LastCheckIn;

        public int LastDailyReward;

        public int Money;

        public int WheelCount;

        public SavedData()
        {
            BuyedSkins = new List<SkinName>();
            SelectedSkin = SkinName.Default;
        }
    }
}