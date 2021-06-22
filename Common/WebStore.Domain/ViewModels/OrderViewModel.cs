using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.ViewModels
{
    public class OrderViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required, MaxLength(20), DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required, MaxLength(500)]
        public string Address { get; set; }
    }
}
