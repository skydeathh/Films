namespace Films.Services
{
    public interface ISharedDataService
    {
        int GetLastFilmId();
        void SetLastFilmId(int id);
        List<int> GetSharedIds();
        void AddSharedId(int id);
        void RemoveSharedId(int id);
    }

    public class SharedDataService : ISharedDataService
    {
        private int _lastFilmId;
        private readonly List<int> _favoriteFilmsIds;
        public SharedDataService()
        {
            _favoriteFilmsIds = new List<int>();
        }

        public void SetLastFilmId(int id)
        {
            _lastFilmId = id;
        }

        public int GetLastFilmId()
        {
            return _lastFilmId;
        }

        public List<int> GetSharedIds()
        {
            return _favoriteFilmsIds;
        }

        public void AddSharedId(int id)
        {
            if (!_favoriteFilmsIds.Contains(id))
            {
                _favoriteFilmsIds.Add(id);
            }
        }

        public void RemoveSharedId(int id)
        {
            _favoriteFilmsIds.Remove(id);
        }
    }
}
