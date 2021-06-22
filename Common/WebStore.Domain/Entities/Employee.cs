using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities
{
    public class Employee : Entity
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string Patronymic { get; set; }

        public int Age { get; set; }
    }
}
