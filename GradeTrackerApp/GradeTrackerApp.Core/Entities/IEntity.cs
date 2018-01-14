namespace GradeTrackerApp.Core.Entities
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
        string Name { get; set; }
    }
}