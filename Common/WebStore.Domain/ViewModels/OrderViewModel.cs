using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.ViewModels
{
    public class OrderViewModel
    {
        /// <summary>Название</summary>
        [Required]
        public string Name { get; set; }

        /// <summary>Телефон для связи</summary>
        [Required, MaxLength(20), DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        /// <summary>Адрес доставки</summary>
        [Required, MaxLength(500)]
        public string Address { get; set; }
    }
}
