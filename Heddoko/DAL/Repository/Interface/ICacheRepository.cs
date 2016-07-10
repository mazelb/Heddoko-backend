namespace DAL
{
    public interface ICacheRepository<T> where T : DAL.Models.BaseModel
    {
        string GetCacheKey(string id);

        T GetCached(string id);

        void SetCache(string id, T item);

        void ClearCache(string id);
    }
}
