using UnityEngine;
using YG;

namespace Code_Base.Data
{
    public class YandexSaveLoader : ISaveLoader
    {
        public void Save(SavedData data)
        {
            YandexGame.savesData.Data = data;
            YandexGame.SaveProgress();
        }

        public SavedData Load()
        {
            return YandexGame.savesData?.Data;
        }
    }
}