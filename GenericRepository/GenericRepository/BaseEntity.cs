using System.ComponentModel.DataAnnotations;

namespace GenericRepository
{
    public abstract class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
