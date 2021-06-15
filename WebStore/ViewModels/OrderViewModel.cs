using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
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
