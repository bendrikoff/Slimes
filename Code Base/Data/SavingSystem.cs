using UnityEngine;
using YG;

namespace Code_Base.Data
{
    public class SavingSystem : Singleton<SavingSystem>
    {
        public SavedData Data;

        private ISaveLoader _saveLoader;
        
        private void OnEnable()
        {
            YandexGame.GetDataEvent += GetLoad;
        }
        private void OnDisable()
        {
            YandexGame.GetDataEvent -= GetLoad;
        }

        protected override void Awake()
        {
            base.Awake();
            _saveLoader = new YandexSaveLoader();
        }

        protected void Start()
        {
            if (YandexGame.SDKEnabled == true)
            {
                Data = _saveLoader.Load();
            }
        }

        public void Save()
        {
            _saveLoader.Save(Data);
        }

        private void GetLoad()
        {
            Data = _saveLoader.Load();
        }
    }
}