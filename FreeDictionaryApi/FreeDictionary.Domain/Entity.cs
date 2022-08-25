
namespace FreeDictionary.Domain
{
    public class Entity
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime CreatedIn { get; set; }
        public virtual DateTime? UpdatedIn { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
