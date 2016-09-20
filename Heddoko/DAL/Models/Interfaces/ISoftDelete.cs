namespace DAL.Models
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; }
    }
}