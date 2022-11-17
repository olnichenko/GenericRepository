using System.ComponentModel.DataAnnotations;

namespace GenericRepository
{
    public abstract class BaseEntityAbstract<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
