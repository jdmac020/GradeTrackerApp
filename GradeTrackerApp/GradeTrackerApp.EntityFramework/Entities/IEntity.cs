namespace GradeTrackerApp.EntityFramework.Entities
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
        string Name { get; set; }
    }
}