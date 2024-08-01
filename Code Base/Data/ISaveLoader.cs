namespace Code_Base.Data
{
    public interface ISaveLoader
    {
        public void Save(SavedData data);

        public SavedData Load();
    }
}